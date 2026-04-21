# ⚙️ FLEB — Ferneon Logic Engine Bytecode  
### The Official Bytecode Specification

FLEB is the compiled bytecode format used by the Ferneon Logic Engine (FLE).  
It is designed to be:

- Compact  
- Deterministic  
- Fast to interpret  
- Safe for sandboxed execution  
- Easy to serialize and load  
- Engine‑agnostic  

FLEB is produced by the FLES compiler and executed by the FLER virtual machine.

---

# 🧩 Bytecode Structure

A compiled FLEB program contains:

- **Instruction Stream** — the actual bytecode  
- **Constants Table** — strings, numbers, vectors, etc.  
- **Variable Table** — names and initial values  
- **Event Table** — entry points for events  
- **Debug Metadata** (optional)

This is represented in code as a `FLESProgram`.

---

# 📦 FLESProgram Layout

A FLEB program is structured like this:

```
FLESProgram
│
├── Instruction[] Instructions
├── object[] Constants
├── VariableInfo[] Variables
├── Dictionary<EventType, int> EventEntryPoints
└── DebugInfo (optional)
```

### **Instructions**
The bytecode executed by the interpreter.

### **Constants**
Literal values referenced by index.

### **Variables**
Per‑instance storage slots.

### **Event Entry Points**
Mapping of event → instruction index.

---

# 🧱 Instruction Format

Each instruction has:

- **OpCode** — the operation to perform  
- **Operand A** — optional  
- **Operand B** — optional  

Example:

```
new Instruction(OpCode.PushConst, 0)
```

This pushes constant index `0` onto the stack.

---

# 🧠 Stack-Based Execution

FLEB uses a **stack machine** architecture:

- Most operations push or pop values  
- API calls pop arguments  
- Conditionals pop booleans  
- Math pops operands and pushes results  

This keeps the interpreter simple and fast.

---

# 🧩 Supported OpCodes

Below is the core instruction set.

---

## 📥 Stack Operations

| OpCode | Description |
|--------|-------------|
| `PushConst index` | Pushes a constant onto the stack |
| `PushVar index` | Pushes a variable value |
| `StoreVar index` | Pops and stores into a variable |
| `Pop` | Discards the top of the stack |

---

## 🔢 Math Operations

All math ops pop operands and push results.

| OpCode | Description |
|--------|-------------|
| `Add` | a + b |
| `Sub` | a - b |
| `Mul` | a * b |
| `Div` | a / b |
| `Mod` | a % b |
| `Neg` | -a |

---

## 🔍 Comparison Operations

| OpCode | Description |
|--------|-------------|
| `Equal` | a == b |
| `NotEqual` | a != b |
| `Less` | a < b |
| `LessEqual` | a <= b |
| `Greater` | a > b |
| `GreaterEqual` | a >= b |

---

## 🔀 Boolean Operations

| OpCode | Description |
|--------|-------------|
| `And` | a && b |
| `Or` | a \|\| b |
| `Not` | !a |

---

## 🔁 Control Flow

| OpCode | Description |
|--------|-------------|
| `Jump index` | Unconditional jump |
| `JumpIfTrue index` | Jump if top of stack is true |
| `JumpIfFalse index` | Jump if top of stack is false |
| `Return` | Exit the current event |

---

## 🧩 API Calls

| OpCode | Description |
|--------|-------------|
| `CallApi apiId, argCount` | Calls a FLEA API function |

Example:

```
CallApi 3, 2
```

Calls API function ID `3` with `2` arguments popped from the stack.

---

## 🔔 Event Handling

Events are not opcodes — they are **entry points**.

Example event table:

```
OnStart → instruction index 0
OnUpdate → instruction index 12
OnInteract → instruction index 40
```

When Unity fires an event, FLER jumps to the corresponding instruction index.

---

# 🧬 Constants Table

Constants are stored in a simple array:

```
Constants[0] = "DoorOpen"
Constants[1] = true
Constants[2] = 3.5
Constants[3] = Vector3(0, 1, 0)
```

Instructions reference constants by index.

---

# 📦 Variable Table

Variables are stored per‑instance, not per‑program.

Example:

```
Variables:
0 → "Health"
1 → "IsOpen"
2 → "Speed"
```

At runtime, each script instance has:

```
object[] VariableValues
```

---

# 🧪 Example Bytecode Program

Here is a conceptual example of a simple script:

### FLES Script
```
OnStart:
    PlaySound("Spawn")
    SetVariable("Health", 100)
```

### Compiled FLEB
```
Constants:
0 → "Spawn"
1 → 100

Instructions:
0: PushConst 0
1: CallApi 1, 1      ; PlaySound
2: PushConst 1
3: StoreVar 0        ; Health
4: Return

EventEntryPoints:
OnStart → 0
```

---

# 🧱 Safety Guarantees

FLEB is designed to be safe:

- No arbitrary memory access  
- No dynamic code execution  
- No recursion  
- No unbounded loops  
- No direct engine access  
- No reflection  
- No unsafe operations  

The interpreter enforces strict sandboxing.

---

# 🔧 Serialization

FLEB can be serialized as:

- Binary  
- JSON  
- ScriptableObject  
- Custom asset format  

The engine does not enforce a specific format.

---

# 📚 Next Steps

Continue to:

- **3_FLER_Runtime.md** — How the VM executes bytecode  
- **4_FLEA_API.md** — API reference  
- **6_Unity_Integration.md** — Using FLE inside Unity  

This document defines the bytecode format.  
The next document explains how it is executed.
