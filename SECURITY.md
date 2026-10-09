# Security Policy

## Repository Security Model

This repository contains a protected runtime distribution and a redacted architecture backbone. It does not contain the private implementation used to build the executable.

The public boundary includes architecture documentation, responsibility guides, public-safe interfaces, redacted configuration examples, and release assets. It excludes live credentials, authorization material, native transport details, protection procedures, and operational automation constants.

## Reporting A Security Issue

Do not publish credentials, tokens, device identifiers, private logs, or exploit details in a public GitHub issue.

Contact the maintainer through the repository owner's GitHub profile or the project [Discord server](https://discord.gg/tvjJdznEeT). Include:

- the affected file or release hash;
- the observed behavior and impact;
- minimal reproduction steps that contain no private data;
- whether the issue affects the public repository, released runtime, or update path.

## Local Configuration

`AloneAIOR/bin/Debug/Alone.ini` is tracked as the default configuration. Its `Account` and `Password` fields must remain blank in the repository. Users may configure those values locally, but they must restore or remove personal values before contributing changes.

The following runtime file is local-only and must never be committed:

```text
AloneAIOR/bin/Debug/Password.ini
```

Account, password, token, device, and room-secret values must never be added to tracked files.

If a credential has ever been committed to a public history, changing the Git history is not sufficient. Treat the value as exposed and rotate it at its authority.

## Released Binary Trust Boundary

`AloneAIOR/bin/Debug/AloneAIOR.exe` is a protected binary. It cannot be reproduced from this public backbone and may require administrator rights for platform integration.

Users should:

- keep the complete release directory together;
- verify the repository and release source before execution;
- avoid third-party repackaged binaries;
- avoid uploading local configuration or logs containing private values;
- review changes to runtime asset hashes before replacing an installation.

## Public Documentation Boundary

Security reports and documentation changes must not add:

- live API endpoints or authorization payloads;
- signing material, session values, or device identifiers;
- native memory or message layouts;
- device report formats or private driver procedures;
- protection, process-modification, or evasion procedures;
- fixed automation coordinates, timings, or recovery constants.

The public repository gate in [`tools/Verify-PublicRepository.ps1`](tools/Verify-PublicRepository.ps1) enforces the trackable portion of this policy.
