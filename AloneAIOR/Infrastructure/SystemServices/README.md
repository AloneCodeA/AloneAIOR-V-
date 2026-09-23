# System Services

## Purpose

Provide small, cross-cutting timing, threading, text, file, environment, and randomization capabilities.

## Owns

- bounded delays and time access;
- thread-safe counters and flags;
- focused text and encoding operations;
- file and environment access helpers.

## Depends On

Only the .NET Framework and operating-system services required by each focused utility.

## Does Not Own

Application policy, game behavior, platform integration, or feature-specific state.

## Public Contracts

System services remain small and responsibility-specific. This area must not become a general utility dumping ground.
