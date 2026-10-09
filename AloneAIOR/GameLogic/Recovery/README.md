# Recovery Policies

## Purpose

Classify stalled or divergent automation state and select a bounded corrective policy.

## Owns

- recovery eligibility and escalation;
- attempt, time, and progress limits;
- recovery result classification;
- transition to controlled shutdown.

## Depends On

Progress history, current state evidence, logical action capability, cancellation, and diagnostics.

## Does Not Own

Input transport, visual acquisition, route storage, or user-interface failure presentation.

## Public Contracts

The public boundary describes policy ownership only. Production heuristics, values, and tactical action sequences remain private.
