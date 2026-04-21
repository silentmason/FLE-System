# 📘 Ferneon Logic Engine (FLE)

The **Ferneon Logic Engine (FLE)** is the scripting system that powers all interactive content inside **Ferneon**.  
It is designed to be:

- Safe  
- Deterministic  
- Multiplayer‑friendly  
- Creator‑accessible  
- Extensible  

FLE is divided into six major components:

| Acronym | Name | Purpose |
|--------|------|---------|
| **FLE** | Ferneon Logic Engine | The entire scripting system |
| **FLES** | Ferneon Logic Engine Scripts | The scripting language creators write |
| **FLEB** | Ferneon Logic Engine Bytecode | The compiled output of FLES |
| **FLER** | Ferneon Logic Engine Runtime | The VM that executes bytecode |
| **FLEA** | Ferneon Logic Engine API | Safe engine functions exposed to scripts |
| **FLEN** | Ferneon Logic Engine Nodes | Visual scripting nodes used in the editor |

---

## 🧠 FLES — The Scripting Language

Creators write gameplay logic using **FLES**, a safe, sandboxed scripting language.

FLES can be:

- Visual (FLEN nodes)  
- Text‑based (optional)  
- Hybrid  

FLES scripts **cannot** access Unity, C#, files, networking, or unsafe operations.  
They can only call **FLEA API functions**.

### Example (conceptual)

```
OnInteract:
    PlaySound("DoorOpen")
    SetVariable("IsOpen", true)
```

---

## ⚙️ FLEB — Bytecode Format

When a creator saves a FLES script, it is compiled into **FLEB bytecode**.

FLEB contains:

- Instruction stream  
- Constants table  
- Variable table  
- Event entry points  

### Example (conceptual)

```
PUSH_CONST 0        ; "DoorOpen"
CALL_API 1, 1       ; PlaySound
PUSH_CONST 1        ; true
STORE_VAR 0         ; IsOpen
RETURN
```

---

## 🖥️ FLER — Runtime Execution

The **FLER Runtime** is the virtual machine that executes FLEB bytecode.

FLER is:

- Deterministic  
- Sandbox‑safe  
- Server‑authoritative  
- Event‑driven  
- Lightweight  

FLER consists of:

- `FLESProgramInstance` — runtime state  
- `FLERContext` — manages all instances  
- `FLERInterpreter` — executes bytecode  

---

## 🧱 FLEA — Safe API Functions

Scripts interact with the world through **FLEA**, the safe API registry.

Examples:

- `PlaySound(soundId)`  
- `SpawnObject(prefabId)`  
- `MoveTo(position)`  
- `SendEvent(eventName)`  
- `GetPlayerPosition(playerId)`  

Creators never call Unity or C# directly — only FLEA.

---

## 🧩 FLEN — Visual Scripting Nodes

FLEN is the visual scripting layer.

Each FLEA API function becomes a node:

```
[Play Sound]
Inputs: soundId
```

Each event becomes a node:

```
[On Interact]
```

Each variable becomes a node:

```
[Get Variable]
[Set Variable]
```

FLEN compiles into FLES → FLEB → FLER.

---

# 📦 Runtime Architecture

Below is a clear explanation of each runtime script included in the engine.

---

## 📄 FLESProgram.cs

Stores compiled script data.

Contains:

- Bytecode  
- Constants  
- Variable table  
- Event table  
- Program ID  
- Debug info  

This is a **data container**, not logic.

---

## 📄 FLESProgramInstance.cs

Represents a running script attached to an entity.

Contains:

- Reference to FLESProgram  
- Runtime variable array  
- Stack  
- Instruction pointer  
- Entity ID  
- Enabled flag  

This is the **runtime state** of a script.

---

## 📄 FLERContext.cs

Manages all running scripts in a world.

Responsibilities:

- Add/remove script instances  
- Fire events (OnStart, OnUpdate, etc.)  
- Run scripts through FLERInterpreter  

This is the **script manager**.

---

## 📄 FLEAApiRegistry.cs

Stores safe API functions.

Responsibilities:

- Register API handlers  
- Look up API handlers by ID  
- Enforce sandboxing  

This is the **bridge** between scripts and the engine.

---

## 📄 FLERInterpreter.cs

Executes bytecode.

Responsibilities:

- Stack operations  
- Variable operations  
- Control flow  
- API calls  
- Event execution  

This is the **virtual CPU** of FLE.

---

## 📄 FLESEventType.cs

Defines all script events.

Examples:

- OnStart  
- OnUpdate  
- OnInteract  
- OnTriggerEnter  
- OnDamage  

This is the **event system**.

---

# 🧪 Script Lifecycle

1. Creator builds logic in FLEN  
2. FLEN compiles into FLES  
3. FLES compiles into FLEB  
4. FLEB loads into a FLESProgram  
5. FLERContext creates a FLESProgramInstance  
6. Unity triggers an event  
7. FLERInterpreter executes the script  
8. Script calls FLEA API functions  
9. Engine performs the action safely  

---

# 📚 Creator Summary

Creators only need to learn:

- Events  
- Variables  
- API functions  
- Visual nodes  

They never touch bytecode or the runtime.

---

# 🛠️ Developer Summary

Engine developers work with:

- `FLESProgram`  
- `FLESProgramInstance`  
- `FLERInterpreter`  
- `FLERContext`  
- `FLEAApiRegistry`  
- `Instruction.cs`  

This is the full runtime architecture.

