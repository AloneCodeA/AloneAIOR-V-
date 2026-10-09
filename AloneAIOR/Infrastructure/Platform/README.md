# Platform Adapters

## Purpose

Isolate Windows and native runtime operations behind focused infrastructure boundaries.

## Owns

- Windows API adaptation;
- native resource lifecycle;
- conversion between capability requests and platform operations;
- low-level error translation.

## Depends On

Windows x64, shipped runtime assets, configuration, and diagnostics.

## Does Not Own

Game workflow, route behavior, authorization policy, or presentation decisions.

## Public Contracts

The platform tree is a responsibility map. Private device, transport, protection, and target-process procedures are not public contracts.
