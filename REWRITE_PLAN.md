# Lexomancy Rewrite Task List

This document outlines the ordered tasks for the Lexomancy rewrite, following the "strangler-fig" pattern. We will build a new authoritative core alongside the legacy code, prove parity, and then migrate systems in slices.

## Phase 0 — Repo Prep & Blast-Radius Freeze
**Definition of Done:** Assemblies compile, no cross-references from Sim to Unity, and the legacy game still runs.

- [ ] **0.1 Create greenfield structure & assemblies:**
    - `/Assets/Lexomancy/Sim` — pure C# authoritative core (no UnityEngine types).
    - `/Assets/Lexomancy/Data` — ScriptableObjects (definitions only).
    - `/Assets/Lexomancy/Presentation` — MonoBehaviours, VFX, UI, cameras.
    - `/Assets/Lexomancy/Tools` — Editor windows, importers, validators.
    - `/Assets/Lexomancy/Legacy` — existing implementation, frozen.
    - `/Assets/Lexomancy/Bootstrap` — entry scene + wiring.
    - Create `asmdefs`: `Lex.Sim`, `Lex.Data`, `Lex.Pres`, `Lex.Tools`, `Lex.Legacy`, `Lex.Tests` (editor + playmode).
- [ ] **0.2 Adopt coding & testing policy for Sim:**
    - Enable nullable refs, treat warnings as errors in `Lex.Sim`.
    - Add NUnit + a basic test runner.
- [ ] **0.3 Freeze risky work:**
    - Feature freeze on new abilities/complex statuses until Sim kernel lands.
    - Create a `CONTRIBUTING.md` note about the freeze.

## Phase 1 — Shared Simulation Kernel (authoritative core)
**DoD:** Kernel can run a tiny scripted scenario fully in headless tests.

- [ ] **1.1 Core types (POCOs):** `EntityId`, `TagSet`, `StatBlock`, `Status`, `Deck`, `Hand`, `Pile`. Types must be serializable and have tests.
- [ ] **1.2 Deterministic RNG:** `RngStream` (PCG/XorShift) with seed + substreams. Must be repeatable across platforms.
- [ ] **1.3 Action Queue + Command interface:** `ICommand { void Execute(SimContext ctx); }` and `ActionQueue` (FIFO).
- [ ] **1.4 Calculators & Log:** `Calculators` service and `BattleLog`/`RunLog` with a stable running hash.
- [ ] **1.5 SimContext:** Holds state, queue, calculators, RNG, and event dispatcher.

## Phase 2 — Battle Core MVP (data-driven)
**DoD:** An end-to-end headless test, given a seed and scripted actions, produces the expected final state and hash.

- [ ] **2.1 Effect definitions (SO) + planning interfaces:** `IEffectDefinition` → `IPlannedEffect`, `PlanContext`, `SimContext`. Concrete SOs for Damage, Heal, Shield, Sequence, Conditional.
- [ ] **2.2 Targeting definitions (SO):** `Self`, `SingleEnemy`, `AllEnemies`.
- [ ] **2.3 Costs/requirements (SO):** `CostDefinition`, `RequirementDefinition`.
- [ ] **2.4 Minimal battle loop:** Turn structure (start, commit, resolve, end).

## Phase 3 — Dual-Run Harness & Parity
**DoD:** All baseline micro-battles pass; hashes match or explainable known deltas are recorded.

- [ ] **3.1 Mirror runner:** Launch Legacy and Sim battles with same seed/inputs, then diff the final states and logs.
- [ ] **3.2 Parity baselines:** Author at least 5 micro-battles covering core mechanics.

## Phase 4 — Presentation Decoupling
**DoD:** UI/FX react to events; no presentation code mutates authoritative state.

- [ ] **4.1 Event bridge:** Presentation subscribes to Sim events (`EffectResolved`, `DamageTaken`, etc.).
- [ ] **4.2 Input intents:** UI produces intents, not direct state changes. Intents are fed to the planner/queue.

## Phase 5 — Tooling MVP
**DoD:** Designers can locate a card and simulate it without pressing Play.

- [ ] **5.1 Lexicon Editor (MVP):** EditorWindow to list/filter `CardTemplates`, view fields, and "Test Cast" into a headless Sim.
- [ ] **5.2 Content Validator:** Editor script to scan all SOs for invariant violations (e.g., cost ranges). Fails CI if errors are found.
- [ ] **5.3 Importer pipeline:** Watch a data folder, import/normalize data, and build a cache/index asset.

## Phase 6 — Status System & Targeting Expansion
**DoD:** Unit tests cover stacking rules, timing hooks, and targeting edge cases.

- [ ] **6.1 Status framework:** Stacks, durations, tick timing, on-hit/on-cast hooks. `ApplyStatus`, `RemoveStatus`, `TickStatus` commands.
- [ ] **6.2 Targeting patterns:** Add `RandomEnemy`, `AllAllies`, and pattern abstractions (AoE/line).

## Phase 7 — Save/Load & Replay
**DoD:** A "golden" replay executes identically on CI; its final hash is verified.

- [ ] **7.1 Snapshot serializer:** Binary snapshot of `SimContext` (or a compact DTO) and a JSON debug path.
- [ ] **7.2 Replay file format:** `{ seed, initialSnapshot, actionList, periodicSnapshots? }`.

## Phase 8 — Overworld Sim MVP (Scrabble/grid)
**DoD:** A headless test can place a word, trigger an encounter event, and correctly handshake a battle seed.

- [ ] **8.1 OverworldContext + queue:** Separate context, queue, log, and RNG for the overworld.
- [ ] **8.2 Board representation:** Flat array or axial hex for tiles: `Tile { Letter, Flags, Bonus }`.
- [ ] **8.3 Word placement & scoring:** `PlaceWord` command validates, scores, and applies multipliers.
- [ ] **8.4 Triggers & effects:** Hook system to emit events (reveal, spawn, grant resource) from word placement.

## Phase 9 — Battle ↔ Overworld Handshake
**DoD:** An end-to-end test can run: place word → battle sim → back to overworld with applied rewards/costs.

- [ ] **9.1 Encounter bridge:** Derive `BattleSeed`, gather party state, launch battle, and apply results back as `OverworldCommands`.
- [ ] **9.2 Lifecycle policy:** Battle Sim is instanced and disposed cleanly; overworld persists.

## Phase 10 — CI & Automation
**DoD:** CI badge is green; failing tests or validation block merges.

- [ ] **10.1 Headless test & build:** Add a `-batchmode` job to run all tests, then build Addressables and the player.
- [ ] **10.2 Data-driven smoke tests:** Parameterized tests that iterate and "dry-run" all `CardTemplates`.

## Phase 11 — Content Porting Cadence
**DoD:** Slices pass the dual-run harness; legacy code for ported effects is disabled.

- [ ] **11.1 Weekly ability slices:** Port N effect types from Legacy to Sim each week.
- [ ] **11.2 Migration scripts:** Create editor tools (`Tools/Lexomancy/Migrate vX→vY`) to reshape SOs as schemas evolve.

## Phase 12 — Flip & Delete Legacy
**DoD:** The game is fully playable on the new Sim; the legacy assembly is removed.

- [ ] **12.1 Flip the switch:** Presentation layer only reads from Sim events.
- [ ] **12.2 Remove legacy:** Delete `Lex.Legacy` assembly and all related code.
