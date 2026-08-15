# Startup And Lifecycle

## Purpose

Validate the runtime environment and promote the application through one controlled startup path.

## Owns

- preflight and single-session policy;
- maintenance and compatibility decisions;
- initialization ordering;
- controlled exit before main-application promotion.

## Depends On

Authorization, configuration, localization, capability initialization, diagnostics, and presentation construction.

## Does Not Own

Automation behavior, route execution, visual state classification, or backend input dispatch.

## Public Contracts

The public startup flow is documented in [`../../../../ARCHITECTURE.md`](../../../../ARCHITECTURE.md). Private promotion and protection procedures are omitted.
