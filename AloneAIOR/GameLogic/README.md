# Game Logic

## Purpose

Convert observed game state and route intent into bounded automation decisions.

## Owns

- state-oriented gameplay workflows;
- movement and progress interpretation;
- route and mode selection;
- recovery policy and terminal cleanup requests.

## Depends On

Vision evidence, logical input actions, game-client lifecycle state, timing, configuration projections, and diagnostics.

## Does Not Own

Windows Forms lifecycle, authorization authority, native resource management, input report formatting, or frame transport.

## Public Contracts

- [`Contracts/IAutomationController.cs`](Contracts/IAutomationController.cs)
- [`Functions/`](Functions/) - core automation responsibilities.
- [`Recovery/`](Recovery/) - recovery policy boundary.
