# Game Workflows

## Purpose

Coordinate high-level game-state transitions and delegate specialized behavior to focused modules.

## Owns

- login and navigation intent;
- task sequencing;
- launch and disconnect recovery requests;
- action cleanup when the target lifecycle changes.

## Depends On

Game-client state, vision evidence, logical input, configuration, and notifications.

## Does Not Own

Credential authority, process implementation, image analysis algorithms, or platform input delivery.

## Public Contracts

This module consumes public capability boundaries but intentionally publishes no concrete workflow implementation.
