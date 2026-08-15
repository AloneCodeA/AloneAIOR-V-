# Specialized Visual Workflows

## Purpose

Contain automation modes that combine focused image evidence, OCR, matching, and short-lived action sequences.

## Owns

- mode-specific evidence interpretation;
- confidence and timeout policy;
- mode-local action sequencing;
- cleanup when evidence is insufficient.

## Depends On

Vision services, logical input, localized text processing, timing, and diagnostics.

## Does Not Own

OCR engine lifecycle, generic capture, application orchestration, or global route policy.

## Public Contracts

No production question data, matching thresholds, image regions, or action timing is included.
