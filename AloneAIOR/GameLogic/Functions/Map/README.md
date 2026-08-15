# Map And Route Behavior

## Purpose

Select route behavior for the active map context and bridge route documents into the game-logic runtime.

## Owns

- map-category responsibility boundaries;
- route selection and lifecycle;
- route execution entry and cleanup;
- event-route categorization.

## Depends On

Validated map context, route documents, movement behavior, logical input, and diagnostics.

## Does Not Own

Map-file transport, protected storage keys, production route implementations, or native integration.

## Public Contracts

- [`Scripting/`](Scripting/) - phase-based route execution.
- [`Maps/`](Maps/) - route category marker.
- [`EventMap/`](EventMap/) - event-route marker.
