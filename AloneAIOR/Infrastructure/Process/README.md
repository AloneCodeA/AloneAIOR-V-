# Process And Window Coordination

## Purpose

Track the target process and expose stable lifecycle and window-state evidence to higher layers.

## Owns

- target discovery and lifecycle tracking;
- window availability and geometry;
- foreground and interaction eligibility projections;
- process failure classification.

## Depends On

Windows platform adapters, timing, configuration, and diagnostics.

## Does Not Own

Automation policy, action selection, UI promotion, or visual state classification.

## Public Contracts

This module publishes responsibility documentation only; private process-control procedures are omitted.
