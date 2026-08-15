# Movement And Progress

## Purpose

Interpret route progress, request directional actions, and report when bounded recovery is required.

## Owns

- progress evidence interpretation;
- movement intent and action ownership;
- stall and divergence classification;
- recovery requests and result handling.

## Depends On

Vision state, logical input, route context, timing, and recovery policy.

## Does Not Own

Screen capture transport, key mapping storage, backend dispatch, or private route constants.

## Public Contracts

The public architecture documents the observe-decide-act-recover loop. Production scoring, thresholds, and tactical sequences are intentionally omitted.
