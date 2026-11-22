# Cloned SuperLabs — Multiplayer Unity Prototype

**A four-player multiplayer game prototype built in Unity using C# and Mirror networking.**

Players explore a shared environment, receive hidden assignments, interact with tasks and doors, and use a notebook to work out player assignments. **This is an unfinished prototype, not a released or verified playable game.**

| | |
|---|---|
| **Engine** | Unity 2019.4.17f1 |
| **Technology** | C#, Mirror Networking, TextMesh Pro |
| **Type** | Multiplayer gameplay systems / prototype |
| **Status** | Unfinished; code review only, gameplay not verified |
| **Browser demo** | Not available |

## What the repository demonstrates

- **Multiplayer game flow:** `Assets/GameController.cs` manages player spawning, assigned labels, starting money, and timed day/night phases.
- **Networked interaction:** `Assets/PlayerRaycast.cs`, `Assets/PlayerDoor.cs` and `Assets/TaskDoor.cs` provide proximity interaction and door behavior.
- **Notebook deduction:** `Assets/NoteConsole.cs` compares submitted player assignments and invokes a victory handler.
- **Computer-task prototype:** `Assets/ComputerTask.cs` and `Assets/ComputerTaskUI.cs` illustrate the task UI and request path but **do not implement working answer validation**.
- **Level generation experiment:** `Assets/WorldGenerator.cs` spawns modular floor tiles and attempts branching corridors; the current branch-removal logic may throw a runtime exception.

### Project structure

```text
Assets/
  Scenes/            Unity scenes (including SnowLab and a generator prototype)
  GameController.cs   Networked round management
  PlayerGameState.cs  Player state and labels
  NoteConsole.cs      Notebook submission / win check
  ComputerTask*.cs    Task interaction prototype
  WorldGenerator.cs   Procedural layout experiment
  Mirror/             Third-party networking framework
Packages/
ProjectSettings/
```

## How to inspect the project

1. Install **Unity 2019.4.17f1** using Unity Hub.
2. Clone the repository and open its root folder as a Unity project.
3. Open a scene such as `Assets/Scenes/MainScene.unity` or `Assets/Scenes/SnowLab.unity`.
4. Inspect the scene setup and scripts before attempting multiplayer play.

**Note:** This documentation is not a claim that the project compiles or plays without errors. Four-player testing requires a properly configured host/client setup.

## Development status / known issues

- The computer task always evaluates `correct = false`; its UI currently submits placeholder values `[10, 20, 30]`.
- Power names are assigned but distinct gameplay effects have not been verified.
- The procedural generator modifies a list during a `foreach`, which may throw at runtime.
- The loser-screen target in the victory handler needs multiplayer verification.
- The game has no verified live web build, video walkthrough, or downloadable release.

See [PROJECT_STATUS.md](PROJECT_STATUS.md) for a prioritized validation checklist.

## Ownership and attribution

This project includes **third-party materials**, including the Mirror framework and imported model/environment asset folders. The repository alone does not establish redistribution permission for all assets. **Do not make it public until licenses, provenance, and any team ownership requirements are checked.**

**Individual contribution notes:** Add a confirmed description of your specific work and collaborators here before using this as an application portfolio piece. File presence alone is not proof of authorship.

## Portfolio media

A gameplay screenshot or short capture can be added here once supplied and cleared for sharing. No screenshot or playable demo has been verified from this repository.

---

*Prototype documentation prepared to make the existing work understandable without overstating its completion.*
