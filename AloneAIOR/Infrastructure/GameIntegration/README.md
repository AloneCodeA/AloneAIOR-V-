# Game Integration

## Purpose

Expose stable game-client and input capabilities without leaking platform implementation into automation policy.

## Owns

- game-client coordination;
- target lifecycle and availability projections;
- logical input gateway composition;
- capability health reporting.

## Depends On

Process and window services, platform adapters, local configuration, and diagnostics.

## Does Not Own

Route decisions, movement strategy, authorization, or user-interface lifecycle.

## Public Contracts

- [`Clients/`](Clients/) - client coordination marker.
- [`Input/`](Input/) - logical input and backend contracts.
