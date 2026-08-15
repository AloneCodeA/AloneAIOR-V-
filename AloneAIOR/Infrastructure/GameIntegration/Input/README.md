# Input Gateway

## Purpose

Resolve logical actions, select an available backend, validate dispatch eligibility, and guarantee action release.

## Owns

- logical action mapping;
- backend selection and health;
- dispatch result classification;
- release of owned action state.

## Depends On

Local configuration, target lifecycle state, platform adapters, timing, and diagnostics.

## Does Not Own

Movement decisions, route phases, device report structures, native transport details, or driver installation procedures.

## Public Contracts

- [`Backends/IInputBackend.cs`](Backends/IInputBackend.cs)
- [`Providers/IKeyMappingProvider.cs`](Providers/IKeyMappingProvider.cs)
