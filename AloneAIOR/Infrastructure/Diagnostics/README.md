# Diagnostics

## Purpose

Record structured runtime transitions, failures, and capability health without changing automation behavior.

## Owns

- severity and module categories;
- bounded file-log lifecycle;
- capture and native capability health reporting;
- safe fallback when normal logging is unavailable.

## Depends On

File-system, threading, timing, and operating-system diagnostic services.

## Does Not Own

Recovery policy, user credentials, authorization payloads, or route decisions.

## Public Contracts

Logs must not contain secrets or private configuration. High-frequency diagnostics must remain independently controllable.
