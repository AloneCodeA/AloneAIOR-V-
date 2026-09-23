# Documentation

This repository is an architecture showcase and protected runtime distribution. Documentation is organized by authority rather than by file count.

## Start Here

- [`../README.md`](../README.md) - English portfolio and download entry.
- [`../README.zh-TW.md`](../README.zh-TW.md) - Traditional Chinese portfolio and download entry.
- [`../ARCHITECTURE.md`](../ARCHITECTURE.md) - redacted system architecture, runtime flows, and ownership boundaries.
- [`../SECURITY.md`](../SECURITY.md) - reporting and sensitive-data policy.

## Engineering Notes

- [`AI-Pathfinding.md`](AI-Pathfinding.md) - research direction for adaptive route selection and recovery.

## Responsibility Guides

The authoritative module guides are located beside the public backbone:

- [`../AloneAIOR/GameLogic/`](../AloneAIOR/GameLogic/) - automation decisions and route behavior.
- [`../AloneAIOR/GameLogic/Functions/Run/`](../AloneAIOR/GameLogic/Functions/Run/) - progress, movement, and recovery requests.
- [`../AloneAIOR/GameLogic/Functions/Map/Scripting/`](../AloneAIOR/GameLogic/Functions/Map/Scripting/) - route document execution.
- [`../AloneAIOR/Infrastructure/Application/`](../AloneAIOR/Infrastructure/Application/) - lifecycle and application policy.
- [`../AloneAIOR/Infrastructure/GameIntegration/Input/`](../AloneAIOR/Infrastructure/GameIntegration/Input/) - logical input and backend ownership.
- [`../AloneAIOR/Infrastructure/Vision/`](../AloneAIOR/Infrastructure/Vision/) - frame evidence, OCR, matching, and state classification.

Leaf README files preserve the redacted folder layout. They are navigation markers, not separate architecture authorities.

## Distribution

The protected executable and required runtime assets are under [`../AloneAIOR/bin/Debug/`](../AloneAIOR/bin/Debug/). The readable backbone is not used to build that executable.
