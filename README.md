# 🧭 Ferneon Logic Engine (FLE) — Overview

The **Ferneon Logic Engine (FLE)** is the core scripting system that powers all interactive content inside **Ferneon**.  
It is designed to be:

- Safe  
- Deterministic  
- Multiplayer‑friendly  
- Creator‑accessible  
- Extensible  
- Engine‑agnostic (but currently implemented in Unity)  

FLE allows creators to build gameplay logic, interactions, tools, and systems without writing unsafe or engine‑level code.  
It is the foundation for Ferneon’s creator ecosystem.

---

# 🧩 The FLE Ecosystem

FLE is composed of six major subsystems, each with a clear purpose:

| Acronym | Name | Purpose |
|--------|------|---------|
| **FLE** | Ferneon Logic Engine | The entire scripting system |
| **FLES** | Ferneon Logic Engine Scripts | The scripting language creators write |
| **FLEB** | Ferneon Logic Engine Bytecode | The compiled output of FLES |
| **FLER** | Ferneon Logic Engine Runtime | The VM that executes bytecode |
| **FLEA** | Ferneon Logic Engine API | Safe engine functions exposed to scripts |
| **FLEN** | Ferneon Logic Engine Nodes | Visual scripting nodes used in the editor |

Each layer builds on the one below it, forming a complete pipeline from creator‑authored logic to runtime execution.

---

# 🧠 FLES — The Scripting Language

FLES is the language creators use to define gameplay logic.  
It can be authored through:

- Visual scripting (FLEN)  
- Text scripting (optional)  
- Hybrid workflows  

FLES is intentionally:

- Simple  
- Safe  
- Deterministic  
- Sandbox‑restricted  

Creators cannot access Unity, C#, the filesystem, networking, or unsafe operations.  
They can only call approved **FLEA API functions**.

---

# ⚙️ FLEB — Bytecode Format

FLES scripts compile into **FLEB**, a compact bytecode format optimized for:

- Fast execution  
- Deterministic behavior  
- Network replication  
- Security  
- Low memory usage  

FLEB contains:

- Instruction stream  
- Constants table  
- Variable table  
- Event entry points  
- Optional debug metadata  

This is the format executed by the runtime.

---

# 🖥️ FLER — Runtime Execution

The **FLER Runtime** is a lightweight virtual machine that executes FLEB bytecode.  
It is responsible for:

- Stack operations  
- Variable access  
- Control flow  
- Event dispatch  
- API calls  
- Script lifecycle management  

FLER is designed to be:

- Fast  
- Predictable  
- Server‑authoritative  
- Easy to embed in Unity  

---

# 🧱 FLEA — Safe API Layer

FLEA exposes safe engine functions to scripts, such as:

- Playing sounds  
- Moving objects  
- Spawning entities  
- Sending events  
- Reading player data  
- Accessing world state  

FLEA ensures:

- No unsafe operations  
- No direct Unity access  
- No arbitrary C# execution  
- Full sandboxing  

Developers can register new APIs to extend the engine.

---

# 🧩 FLEN — Visual Scripting Nodes

FLEN is the visual scripting layer used by creators inside Ferneon.  
Each node corresponds to:

- A FLES instruction  
- A FLEA API call  
- A variable operation  
- A control flow structure  
- An event entry point  

FLEN compiles into FLES → FLEB → FLER.

---

# 🔄 Script Lifecycle

A script goes through the following pipeline:

1. Creator builds logic in FLEN (visual nodes)  
2. FLEN compiles into FLES (script format)  
3. FLES compiles into FLEB (bytecode)  
4. FLEB is loaded into a `FLESProgram`  
5. A `FLESProgramInstance` is created for an entity  
6. Unity triggers events (OnStart, OnUpdate, etc.)  
7. FLERInterpreter executes the script  
8. Script calls FLEA API functions  
9. Engine performs the action safely  

This pipeline ensures safety, determinism, and performance.

---

# 🎯 Design Goals

FLE is built around several core principles:

### **1. Safety**
Scripts cannot crash the engine or access unsafe systems.

### **2. Determinism**
Scripts behave the same on all clients and servers.

### **3. Extensibility**
Developers can add new APIs, nodes, and systems.

### **4. Creator Accessibility**
Non‑programmers can build complex logic visually.

### **5. Engine Independence**
The runtime is portable and not tied to Unity internals.

---

# 📦 What’s Next?

The following documents provide deeper detail:

- **1_FLES_Language.md** — How the scripting language works  
- **2_FLEB_Bytecode.md** — Bytecode format and opcodes  
- **3_FLER_Runtime.md** — Runtime architecture  
- **4_FLEA_API.md** — API reference  
- **5_FLEN_Visual_Nodes.md** — Visual scripting system  
- **6_Unity_Integration.md** — How to use FLE inside Unity  
- **7_Hello_World_Tutorial.md** — First script tutorial  
- **8_Advanced_Examples.md** — More complex examples  

This overview provides the high‑level understanding needed before diving into the details.
