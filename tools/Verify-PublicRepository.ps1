<#
.SYNOPSIS
    Enforces the public/private boundary documented in SECURITY.md and
    docs\superpowers\specs\2026-06-20-public-backbone-design.md.

.DESCRIPTION
    Read-only audit. Never mutates the repository. Referenced by
    .github\workflows\public-repository-audit.yml as the "public repository gate".

.PARAMETER IncludeAllRefs
    Also scan the full reachable history (not just the current tree) for paths that
    should never have been committed, per "History Sanitization" (the branch is a
    flattened snapshot; nothing private should be reachable through it).
#>
[CmdletBinding()]
param(
    [switch]$IncludeAllRefs
)

$ErrorActionPreference = 'Stop'
$repoRoot = Split-Path -Parent $PSScriptRoot
Set-Location $repoRoot

$results = New-Object System.Collections.Generic.List[object]
function Record([string]$name, [bool]$ok, [string]$detail = '') {
    $results.Add([PSCustomObject]@{ Name = $name; Ok = $ok; Detail = $detail })
    $tag = if ($ok) { 'PASS' } else { 'FAIL' }
    Write-Host ("[{0}] {1} {2}" -f $tag, $name, $detail)
}

# Path patterns that must never be tracked, in the current tree or anywhere in history:
# build-system files. GameLogic/Infrastructure are a docs-only backbone (README.md per
# module) plus the "no more than five" hand-written contracts named in the design spec -
# those live at whatever path their owning module uses (Contracts/, Backends/,
# Providers/, Services/, ...), so they are allow-listed by filename below instead of by
# folder shape.
$forbiddenTrackedPatterns = @(
    '\.sln$', '\.csproj$', '\.vcxproj(\.filters)?$', '\.vcxitems$',
    '(^|/)packages\.config$', '(^|/)packages/', '(^|/)obj/', '(^|/)bin/x64/', '(^|/)bin/x86/'
)
$allowedContractFiles = @(
    'IAutomationController.cs', 'IRouteExecutor.cs', 'IInputBackend.cs',
    'IKeyMappingProvider.cs', 'IVisionService.cs'
)
function Get-DisallowedBackboneCs {
    param([string[]]$Paths)
    $Paths | Where-Object {
        $_ -match '^AloneAIOR/(GameLogic|Infrastructure)/.*\.cs$' -and
        ($allowedContractFiles -notcontains (Split-Path -Leaf $_))
    }
}
# Implementation fingerprints that must not appear in tracked public text (SECURITY.md
# "Public Documentation Boundary" + the standing rule to never name the injection DLL).
# The actual sensitive values (real domains/IP/DLL name) never get committed to this
# public repo: they live in a local, gitignored fingerprints file (same idea as
# Alone.ini/Password.ini shipping blank placeholders publicly). CI supplies them via the
# VERIFY_PUBLIC_REPO_FINGERPRINTS secret instead.
$forbiddenFingerprints = @(
    '-----BEGIN (RSA|EC|OPENSSH|PGP) PRIVATE KEY-----'
)
$fingerprintsLocalPath = Join-Path $PSScriptRoot 'Verify-PublicRepository.fingerprints.ps1'
if (Test-Path -LiteralPath $fingerprintsLocalPath) {
    $forbiddenFingerprints += @(& $fingerprintsLocalPath)
} elseif ($env:VERIFY_PUBLIC_REPO_FINGERPRINTS) {
    $forbiddenFingerprints += @($env:VERIFY_PUBLIC_REPO_FINGERPRINTS -split ';' | Where-Object { $_ })
} else {
    Write-Host '[WARN] No fingerprints file or VERIFY_PUBLIC_REPO_FINGERPRINTS set - checking generic patterns only' -ForegroundColor Yellow
}
$textExtensions = @('.md', '.cs', '.yml', '.yaml', '.txt', '.ps1', '.ini')

function Get-TrackedFiles { git ls-files }

# 1. Required portfolio documents and runtime assets are present. Password.ini ships
#    tracked as a public default/placeholder file (same idea as Alone.ini's blank
#    Account/Password) so the runtime layout is complete out of the box.
$required = @(
    'README.md', 'README.zh-TW.md', 'ARCHITECTURE.md', 'SECURITY.md',
    'AloneAIOR/bin/Debug/AloneAIOR.exe', 'AloneAIOR/bin/Debug/Alone.ini',
    'AloneAIOR/bin/Debug/Password.ini'
)
$missing = @($required | Where-Object { -not (Test-Path -LiteralPath $_) })
Record 'Required documents/assets present' ($missing.Count -eq 0) ($missing -join ', ')

# 2. Alone.ini ships with blank Account/Password (Password.ini's placeholder content is
#    covered by the forbidden-fingerprint scan below like any other tracked text file).
$tracked = @(Get-TrackedFiles)
$aloneIniOk = $true
if (Test-Path 'AloneAIOR/bin/Debug/Alone.ini') {
    $iniLines = Get-Content -LiteralPath 'AloneAIOR/bin/Debug/Alone.ini'
    $badLine = $iniLines | Where-Object { $_ -match '^(Account|Password)=.+' }
    $aloneIniOk = -not $badLine
}
Record 'Alone.ini Account/Password blank' $aloneIniOk

# 3. No build-system files or private implementation source tracked in the current tree.
$forbiddenTracked = @($tracked | Where-Object { $p = $_; $forbiddenTrackedPatterns | Where-Object { $p -match $_ } })
$forbiddenTracked += @(Get-DisallowedBackboneCs -Paths $tracked)
Record 'No build-system/private-source files tracked' ($forbiddenTracked.Count -eq 0) ($forbiddenTracked -join ', ')

# 4. Any redacted examples (examples/**, *.ini, *.example) contain no non-empty sensitive values.
$exampleFiles = @($tracked | Where-Object { $_ -match '^examples/' -or $_ -match '\.example$' })
$sensitiveExampleHits = New-Object System.Collections.Generic.List[string]
foreach ($f in $exampleFiles) {
    if (-not (Test-Path -LiteralPath $f)) { continue }
    $hit = Select-String -LiteralPath $f -Pattern '^(Account|Password|Token|Device|Room.?Secret)\s*=\s*\S+' -CaseSensitive:$false
    if ($hit) { $sensitiveExampleHits.Add($f) }
}
Record 'Example configs contain no live values' ($sensitiveExampleHits.Count -eq 0) ($sensitiveExampleHits -join ', ')

# 5. Forbidden implementation fingerprints do not appear in tracked public text.
$fingerprintHits = New-Object System.Collections.Generic.List[string]
foreach ($f in $tracked) {
    if ($textExtensions -notcontains [IO.Path]::GetExtension($f)) { continue }
    if (-not (Test-Path -LiteralPath $f)) { continue }
    $content = Get-Content -LiteralPath $f -Raw -ErrorAction SilentlyContinue
    if (-not $content) { continue }
    foreach ($pattern in $forbiddenFingerprints) {
        if ($content -match $pattern) { $fingerprintHits.Add("$f (~$pattern~)") }
    }
}
Record 'No forbidden implementation fingerprints in tracked text' ($fingerprintHits.Count -eq 0) ($fingerprintHits -join '; ')

# 6. Agent brainstorming / implementation-plan files are not tracked (they are local-only
#    working notes; .gitignore excludes .superpowers/ and docs/superpowers/).
$agentFilesTracked = @($tracked | Where-Object { $_ -match '^\.superpowers/' -or $_ -match '^docs/superpowers/' })
Record 'No agent brainstorm/plan files tracked' ($agentFilesTracked.Count -eq 0) ($agentFilesTracked -join ', ')

# 7. Full reachable history never added anything matching the forbidden patterns above
#    (the branch is a flattened public snapshot; nothing private should be reachable
#    through it even if later deleted from the tip).
if ($IncludeAllRefs) {
    $addedPaths = git log --all --diff-filter=A --name-only --pretty=format: 2>$null |
        Where-Object { $_ -and $_.Trim() }
    $historyHits = @($addedPaths | Where-Object { $p = $_; $forbiddenTrackedPatterns | Where-Object { $p -match $_ } } | Select-Object -Unique)
    $historyHits += @(Get-DisallowedBackboneCs -Paths (@($addedPaths) | Select-Object -Unique))
    Record 'History never added build-system/private-source paths' ($historyHits.Count -eq 0) ($historyHits -join ', ')
} else {
    Record 'History never added build-system/private-source paths (skipped, pass -IncludeAllRefs)' $true
}

# 8. README.md / README.zh-TW.md stay in section-parity (same heading structure).
function Get-HeadingShape([string]$path) {
    @(Get-Content -LiteralPath $path | Where-Object { $_ -match '^#{1,6}\s' } | ForEach-Object { ($_ -replace '^(#{1,6})\s.*', '$1') })
}
$enShape = Get-HeadingShape 'README.md'
$zhShape = Get-HeadingShape 'README.zh-TW.md'
$shapeOk = ($enShape.Count -eq $zhShape.Count) -and (-not (Compare-Object $enShape $zhShape -SyncWindow 0))
Record 'README EN/zh-TW section parity' $shapeOk ("en=$($enShape.Count) zh-TW=$($zhShape.Count) headings")

# 9. Relative Markdown links resolve to real files.
$brokenLinks = New-Object System.Collections.Generic.List[string]
foreach ($f in @($tracked | Where-Object { $_ -match '\.md$' })) {
    if (-not (Test-Path -LiteralPath $f)) { continue }
    $dir = Split-Path -Parent $f
    $content = Get-Content -LiteralPath $f -Raw
    foreach ($m in [regex]::Matches($content, '\[[^\]]*\]\(([^)]+)\)')) {
        $target = $m.Groups[1].Value.Split('#')[0]
        if (-not $target -or $target -match '^(https?:|mailto:)') { continue }
        $resolved = if ($dir) { Join-Path $dir $target } else { $target }
        if (-not (Test-Path -LiteralPath $resolved)) { $brokenLinks.Add("$f -> $target") }
    }
}
Record 'Markdown links resolve' ($brokenLinks.Count -eq 0) ($brokenLinks -join '; ')

# 10. Public contracts (GameLogic/Contracts/*.cs) document every public member.
$docGaps = New-Object System.Collections.Generic.List[string]
$contractFiles = @(Get-ChildItem -LiteralPath 'AloneAIOR/GameLogic/Contracts' -Filter '*.cs' -ErrorAction SilentlyContinue)
foreach ($cf in $contractFiles) {
    $lines = Get-Content -LiteralPath $cf.FullName
    for ($i = 0; $i -lt $lines.Count; $i++) {
        if ($lines[$i] -match '^\s*public\s+(interface|[\w<>\[\],\s]+\s+\w+\s*\()') {
            $prev = if ($i -gt 0) { $lines[$i - 1].Trim() } else { '' }
            if ($prev -notmatch '^///') { $docGaps.Add("$($cf.Name):$($i + 1)") }
        }
    }
}
Record 'Public contract members have XML docs' ($docGaps.Count -eq 0) ($docGaps -join ', ')

# 11. Working tree is clean (the audit must not run against unintended local drift).
$dirty = git status --porcelain
Record 'Git status clean' ([string]::IsNullOrWhiteSpace($dirty)) ($dirty -join '; ')

# 12. Runtime assets are non-empty (a placeholder/corrupt binary must not ship silently).
$runtimeSizeOk = $true
$sizeDetail = ''
$exePath = 'AloneAIOR/bin/Debug/AloneAIOR.exe'
if (Test-Path -LiteralPath $exePath) {
    $len = (Get-Item -LiteralPath $exePath).Length
    if ($len -lt 100KB) { $runtimeSizeOk = $false; $sizeDetail = "AloneAIOR.exe is only $len bytes" }
}
Record 'Runtime assets are non-empty' $runtimeSizeOk $sizeDetail

# 13. origin points at the expected public repository (guards against a silent remote swap).
$remoteUrl = (git remote get-url origin 2>$null)
$remoteOk = $remoteUrl -match 'github\.com[:/]AloneCodeA/AloneAIOR-V-(\.git)?$'
Record 'origin remote matches the public repository' $remoteOk $remoteUrl

Write-Host ''
Write-Host '===== Verify-PublicRepository summary ====='
foreach ($r in $results) {
    $tag = if ($r.Ok) { 'PASS' } else { 'FAIL' }
    Write-Host ("  {0}  {1}" -f $tag, $r.Name)
}
$failed = @($results | Where-Object { -not $_.Ok })
if ($failed.Count -gt 0) {
    Write-Host ("FAILED checks: " + $failed.Count) -ForegroundColor Red
    exit 1
}
Write-Host 'All checks passed.' -ForegroundColor Green
exit 0
