<div align="center">

# AloneAIO Framework

**Production-grade Windows automation runtime**

Computer vision, adaptive recovery systems, encrypted scripting, and low-level Windows platform integration.

https://discord.gg/tvjJdznEeT

</div>

---

## Overview

AloneAIO Framework is a large-scale automation platform built as a runtime architecture, not a traditional macro recorder.

The framework continuously observes live game state, interprets runtime conditions, executes structured route logic, detects drift or failed movement states, and applies adaptive recovery strategies while maintaining controlled orchestration.

**Built with:**

| Layer | Technology |
| --- | --- |
| Runtime | .NET Framework |
| UI | Windows Forms |
| Platform | Kernel Driver |
| Vision | Computer vision, template matching, OCR |
| Scripting | Encrypted `.Map` runtime documents |
| Recovery | Adaptive telemetry-driven correction logic |

---

## Core Technical Areas

| Area | Description |
| --- | --- |
| Adaptive Recovery | Runtime correction system using movement telemetry, failed-path memory, and recovery heuristics. |
| Encrypted Scripting | Structured AES-encrypted `.Map` route system with validation and in-memory execution. |
| Vision Runtime | OCR, template matching, pixel search, region probes, and image analysis. |
| Native Input | Low-level scan-code dispatch, configured key resolution, and Kernel Driver input control. |
| Runtime Orchestration | State-driven automation execution with phases, triggers, recovery zones, and cleanup guarantees. |
| Startup Pipeline | Splash-first startup, authentication gates, validation, update flow, and controlled promotion. |
| Packaging | Embedded resources, protected builds, deterministic output, and x64 deployment. |

---

## Adaptive Recovery Runtime

Unlike replay-based automation, AloneAIO continuously evaluates movement state during execution.

### Recovery Features

- Rolling runpoint sampling.
- Adaptive jam detection.
- Movement delta analysis.
- Opposite-direction micro-correction.
- Side-probe recovery attempts.
- Dash/jump burst recovery.
- Cooldown bans for failed paths.
- Recovery escalation windows.
- Runtime-safe key cleanup.

### Designed To Recover From

- Movement drift.
- Collision desync.
- Partial movement failure.
- Repeated stuck states.
- Unstable movement timing.

This is the difference between a macro and a runtime: a macro repeats, while AloneAIO observes, decides, and corrects.

---

## Encrypted `.Map` Scripting System

The `.Map` system is one of the framework's core runtime components.

Instead of plaintext macro files, routes are stored as structured encrypted runtime documents.

### Runtime Format

```text
AIORMAP1
|-- Version header
|-- AES-CBC encrypted payload
|-- JSON runtime document
|-- Validation metadata
```

### Features

- Versioned contracts.
- AES-CBC encryption.
- Structured JSON serialization.
- Runtime validation.
- In-memory execution.
- State-based execution phases.
- Trigger phases.
- Recovery phases.
- Stage transitions.
- Combo timing definitions.
- Script-only logging.

The result is a route system that is reviewable, tunable, portable, and safer than parsing ad-hoc text while the runtime is active.

---

## Vision System

The vision layer converts screen data into runtime decisions.

### Capabilities

- Region-based screen capture.
- Pixel matching.
- Template matching.
- OCR through Tesseract.
- Thread-safe OCR engine pooling.
- Embedded image resources.
- DPI-aware search coordination.
- Runtime diagnostics toggles.

### Design Goals

- Small-region hot-path captures.
- Minimal unnecessary allocations.
- Stable OCR coordination.
- Runtime performance consistency.
- Centralized assumptions around DPI, scaling, color format, and search regions.

---

## Native Input Layer

AloneAIO owns its own low-level Windows input pipeline.

### Features

- Native `SendInput` batching.
- Scan-code dispatch.
- Configured key resolution.
- Hardware-input backend support.
- Foreground-window validation.
- Ordered combo execution.
- Input blocking states.
- Runtime cleanup guarantees.

Scripts operate on logical bindings instead of hard-coded keyboard keys:

```text
JumpKey
DashKey
ItemKey
ShuKey
```

This allows runtime rebinding without rewriting route logic.

---

## Runtime Architecture

### Main Runtime

```text
AloneAIOR/
```

Contains the automation runtime, vision systems, input systems, route execution, recovery logic, and application infrastructure.

### Major Areas

| Path | Responsibility |
| --- | --- |
| `GameLogic/Functions` | Automation behaviors and route execution. |
| `GameLogic/Functions/Map` | Map orchestration and scripting runtime. |
| `Infrastructure/Vision` | OCR, template matching, capture, and image processing. |
| `Infrastructure/GameIntegration` | Native input and game coordination. |
| `Infrastructure/Application` | Startup, authentication, configuration, localization, and UI. |
| `Infrastructure/Process` | Window/process management and runtime recovery. |
| `Infrastructure/Platform` | Focused Kernel Driver interop boundaries. |

---

## Startup Pipeline

The startup sequence is treated as a dedicated engineering surface.

### Startup Flow

```text
Splash screen
    |
    v
Environment validation
    |
    v
Authentication gates
    |
    v
Update / maintenance handling
    |
    v
Main runtime promotion
```

### Runtime Controls

- Duplicate-session protection.
- Controlled shutdown.
- Update-safe mutex handling.
- Optional scaling validation.
- Startup diagnostics.

---

## Packaging And Distribution

### Build Pipeline

- Costura.Fody embedding.
- Obfuscar integration.
- Deterministic builds.
- Embedded native resources.
- x64-focused deployment.
- Runtime diagnostics control.

---

## Technology Stack

| Category | Technology |
| --- | --- |
| Language | C# |
| Runtime | .NET Framework |
| UI | Windows Forms |
| OCR | Tesseract |
| Serialization | Newtonsoft.Json |
| Vision | System.Drawing |
| Database | MySqlConnector |
| Packaging | Costura.Fody |
| Protection | Obfuscar |
| Platform | Kernel Driver |

---

## Solution Layout

```text
AloneAIO_Framework/
|-- AloneAIOR/
|   |-- GameLogic/
|   |-- Infrastructure/
|   |-- Resources/
|
|-- docs/
|-- packages/
|-- Directory.Build.targets
|-- AloneAIO_Framework.sln
```

---

## Engineering Philosophy

The framework is intentionally separated into distinct runtime layers:

- Behavior orchestration.
- Vision and detection.
- Native input dispatch.
- Script representation.
- Startup and lifecycle management.
- Diagnostics and packaging.

This separation allows the automation runtime to scale without collapsing into a monolithic script system.

---

## Documentation

The `docs/` tree mirrors the source structure and records runtime contracts, module boundaries, architecture decisions, engineering notes, historical implementation mistakes, and correct implementation approaches.

### Suggested Starting Points

```text
docs/Readme.md
docs/AloneAIOR/Readme.md
docs/AloneAIOR/GameLogic/Functions/Map/Readme.md
docs/AloneAIOR/Infrastructure/Vision/Readme.md
docs/Alone.Startup/Readme.md
docs/AloneMarco/Readme.md
```

---

## Repository Scope

This repository focuses on public-facing architecture and engineering design.

Certain operational details, runtime heuristics, protection systems, and internal implementation behavior are intentionally omitted.

The primary purpose of this repository is to showcase:

- Runtime architecture.
- Automation engineering.
- Adaptive recovery systems.
- Computer vision integration.
- Native Windows platform work.
- Packaging and startup orchestration.
- Maintainable large-scale automation design.
