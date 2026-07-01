# Infrastructure

## Purpose

Provide application, integration, platform, process, vision, diagnostic, and system capabilities to game logic.

## Owns

- application lifecycle and policy;
- game-client and platform integration;
- vision and input gateways;
- process, diagnostics, and system services.

## Depends On

Windows, native runtime assets, OCR data, local configuration, and remote authorization decisions.

## Does Not Own

Map strategy, movement decisions, game-mode policy, or route-specific recovery.

## Public Contracts

- [`Application/`](Application/)
- [`GameIntegration/`](GameIntegration/)
- [`Vision/`](Vision/)
- [`Platform/`](Platform/)
- [`Process/`](Process/)
- [`Diagnostics/`](Diagnostics/)
- [`SystemServices/`](SystemServices/)
