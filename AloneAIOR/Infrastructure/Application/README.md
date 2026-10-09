# Application Orchestration

## Purpose

Coordinate process lifecycle, authorization, configuration, localization, route documents, notifications, and presentation promotion.

## Owns

- startup and shutdown policy;
- application session state;
- authorization decision handling;
- local settings and route-document lifecycle.

## Depends On

Integration capability health, remote signed decisions, local files, presentation callbacks, and diagnostics.

## Does Not Own

Movement policy, route tactics, image recognition algorithms, or native transport implementation.

## Public Contracts

- [`Startup/`](Startup/) - process lifecycle.
- [`Authentication/`](Authentication/) - authorization boundary.
- [`Configuration/`](Configuration/) - local settings boundary.
- [`MapScripts/`](MapScripts/) - route document boundary.
