# 🗺️ Emberstone: Summoner — Roadmap

This document outlines the development roadmap for *Emberstone Summoner*, structured by phase. Each phase builds modularly on top of previous work and aims to establish a clean architecture for future expansion.

---

## ✅ Phase 1 — Foundation

### Goals
- Establish code architecture
- Prepare modular, scalable structure
- Implement core engine wrappers

### Tasks
- [x] Project structure: split into `GameClient` (GUI & input) and `GameCore` (data, simulation, logic)
- [x] Rendering setup using OpenTK
- [x] Virtual resolution and aspect ratio system (initially 16:9, later user-configurable)
- [ ] UI Tab system (sidebar to switch game views)
- [ ] Tick system (central game loop for simulation updates)
- [ ] Sprite renderer with simple frame-based animation

---

## 🧩 Phase 2 — Modular Game Systems

### Goals
- Implement core systems as reusable modules
- No specific content focus

### Tasks
- [ ] Emberstone system
  - Types, levels, experience, quality
  - Start-of-run type selection (limited pool at start)
- [ ] Resource funnel
  - Resources (e.g. Wood, Mana) can be converted into Essence
  - Essence: capped resource, fuel for summoning
- [ ] Summoning system
  - Spend Essence to create Mobs
  - Mob quality depends on Emberstone quality
  - Enforce Mob cap
- [ ] Mob sheet & storage UI
- [ ] Task assignment module
  - Tasks: Train, Work, Adventure
  - Run per tick, output stats/resources

---

## 🔥 Phase 3 — Progression Mechanics

### Goals
- Implement loop closures and character progression

### Tasks
- [ ] Mob enhancement system
  - Passive and active modifiers, equipment hooks
- [ ] Sacrifice system
  - Feed Mob into Emberstone → gain XP based on Mastery
- [ ] Emberstone unlocks
  - Unlock systems (e.g. storage, new tabs, buildings) as quality increases

---

## ♻️ Phase 4 — Run & Prestige System

### Goals
- Add long-term meta loop

### Tasks
- [ ] Retirement & run reset
  - Carry forward unlocked Emberstone types
  - Calculate multipliers from Adventure Rank + Emberstone Level
- [ ] Progression multipliers
  - Boost Emberstone XP gain and Mob stat growth per run

---

## ⚔️ Phase 5 — Battle & Automation

### Goals
- Add basic combat and user-driven automation logic

### Tasks
- [ ] Battle simulation (mock interface)
  - Mobs use Skills with cooldowns
  - User assigns a **priority value** to each Skill (lower = higher priority)
  - Simple thresholds for states (e.g. Heal → use if HP < 30%)
  - Skills are triggered cyclically based on priority
- [ ] Adventure task updates (with combat step hooks)

---

## 🧱 Phase 6 — World Expansion Systems

### Goals
- Introduce content variety and modular growth

### Tasks
- [ ] Building system
  - Unlock via Emberstone level
  - Adds new jobs and storage
- [ ] Equipment crafting
  - Assign mobs to create equipment
  - Attach gear to mobs for bonuses

---

## 🧪 Phase 7 — Debug & Balancing Tools

### Goals
- Improve testing and feedback during development

### Tasks
- [ ] Debug console (dev only)
- [ ] Fast-forward ticks for testing
- [ ] Visualizers for balance metrics (XP flow, resource throughput)

---

## Future Notes

- 🖥️ Aspect ratio is fixed to 16:9 during alpha, but future updates will allow automatic resolution detection and user-defined aspect settings.

---

*This roadmap evolves as the project grows. Contributions welcome — see `CONTRIBUTING.md` for guidelines.*