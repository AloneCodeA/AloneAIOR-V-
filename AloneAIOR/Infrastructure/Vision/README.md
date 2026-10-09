# Vision

## Purpose

Convert suitable frames into bounded visual evidence for state coordination and specialized workflows.

## Owns

- frame acquisition coordination;
- region, pixel, and template analysis;
- OCR resource coordination;
- state evidence and capture-health reporting.

## Depends On

Platform capture adapters, OCR runtime assets, configuration, image processing, and diagnostics.

## Does Not Own

Automation decisions, route policy, native transport layout, or user-interface state transitions.

## Public Contracts

- [`Services/IVisionService.cs`](Services/IVisionService.cs)
- [`OCR/`](OCR/) - text evidence marker.
- [`PatternMatching/`](PatternMatching/) - visual matching marker.
- [`State/`](State/) - state classification marker.
