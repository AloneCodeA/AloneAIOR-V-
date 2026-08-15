# Automation Functions

## Purpose

Group the stable behavior responsibilities used by the runtime automation loop.

## Owns

- game-state workflows;
- movement and progress behavior;
- room-state behavior;
- route execution coordination.

## Depends On

Game logic contracts, integration capabilities, route documents, timing, and diagnostics.

## Does Not Own

Platform adapters, native resources, application startup, authorization, or user-interface controls.

## Public Contracts

- [`Game/`](Game/) - game-state workflow boundary.
- [`Run/`](Run/) - movement and progress boundary.
- [`Rooms/`](Rooms/) - room-state boundary.
- [`Map/`](Map/) - map and route boundary.
