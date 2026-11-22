# Cloned SuperLabs — Project status and next steps

**Assessment:** Unfinished multiplayer prototype. Source review only; no Unity build or multiplayer playtest has been performed.

## Prioritized issues to verify

| Priority | Area | Evidence from source | Completion check |
|---|---|---|---|
| P0 | Computer task logic | `ComputerTask.CmdSubmit` initializes `correct = false` | Submit real answers and see distinct correct/incorrect responses |
| P0 | Computer task input | `ComputerTaskUI.OnButtonSubmit` sends hard-coded `[10,20,30]` | UI reads player input and submits it to the server |
| P0 | Procedural world generation | `WorldGenerator.GenerateLobby` removes from `branchPositions` inside a `foreach` | Generation runs repeatedly with no collection-modification exceptions |
| P1 | Multiplayer win states | `GameController.OnWinner` uses winner connection for the losing-screen RPC | Each client receives its own correct victory/defeat screen |
| P1 | Power gameplay | `GameController` assigns placeholder power names | Confirm whether powers are identifiers or intended real mechanics; implement and test as appropriate |
| P1 | Player count and startup | `GameController.Update` starts at `maxConnections == numPlayers` | Test host/client joins, disconnections and start conditions |
| P2 | UI/gameplay polish | Several UI and gameplay scripts have placeholders | Verify all controls, instructions and interaction feedback |

## Portfolio checklist

- [x] Concise root README describing the actual prototype
- [x] Engine version and scene entry points documented
- [x] Known limitations listed without suggesting they are solved
- [ ] Confirm precise personal contribution and collaborators
- [ ] Audit redistributable rights for Mirror and imported assets
- [ ] Provide 2–3 original screenshots or an authorized walkthrough video
- [ ] Capture and verify a playable build, if technically viable
- [ ] Add an accessible public demo link (required by the original portfolio checklist)
- [ ] Only then consider switching visibility to public and pinning the repository

**Important:** A static project README or a video is useful evidence but does **not** meet a requirement for a game that runs directly in a browser.

## Git housekeeping

The existing `.gitignore` already excludes common Unity-generated directories. Existing tracked vendor packages remain present; no assets or history have been removed. If a later cleanup is desired, review ownership and dependencies before rewriting or deleting them.

Commit future fixes separately with accurate messages, e.g. `fix: validate computer task answers on server` *only after implementing and testing that behavior*.
