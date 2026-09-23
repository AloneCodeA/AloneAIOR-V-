# Route Scripting

## Purpose

Execute validated route documents as phases, triggers, logical actions, and bounded recovery transitions.

## Owns

- route execution context;
- phase and trigger state;
- pause, resume, cancellation, and completion;
- terminal action release.

## Depends On

Route models, progress evidence, logical input, timing, recovery policy, and diagnostics.

## Does Not Own

Protected document keys, production routes, input transport, frame capture, or application startup.

## Public Contracts

- [`Contracts/IRouteExecutor.cs`](Contracts/IRouteExecutor.cs)
