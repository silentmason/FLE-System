# 🧩 FLEN — Ferneon Logic Engine Nodes  
### The Visual Scripting Layer for FLES

**FLEN** is the visual scripting system used by creators inside Ferneon.  
It provides a node‑based interface that compiles directly into **FLES** (the scripting language), which then compiles into **FLEB** bytecode.

FLEN is designed to be:

- Creator‑friendly  
- Easy to understand  
- Safe and deterministic  
- Fully compatible with FLES  
- One‑to‑one with bytecode instructions  
- Extensible with custom nodes  

This document explains how FLEN nodes work, how they map to FLES, and how the compiler transforms them into bytecode.

---

# 🧱 Visual Scripting Philosophy

FLEN is built around these principles:

### **1. Every node must map cleanly to FLES**
No node may generate behavior that FLES cannot represent.

### **2. No hidden logic**
Nodes must be transparent and predictable.

### **3. Deterministic execution**
Node graphs must compile into deterministic bytecode.

### **4. Creator‑friendly**
Non‑programmers should be able to build logic visually.

### **5. Extensible**
Developers can add new nodes that map to new API functions.

---

# 🧩 Node Categories

FLEN includes several categories of nodes:

| Category | Purpose |
|----------|---------|
| **Event Nodes** | Entry points (OnStart, OnUpdate, etc.) |
| **Flow Nodes** | Control flow (If, Else, Repeat, Return) |
| **Variable Nodes** | Get/Set variables |
| **Math Nodes** | Add, Subtract, Multiply, Divide |
| **Boolean Nodes** | And, Or, Not |
| **Comparison Nodes** | ==, !=, <, >, <=, >= |
| **API Nodes** | Call engine functions |
| **Utility Nodes** | Constants, vectors, conversions |

Each category maps directly to FLES constructs.

---

# 🔔 Event Nodes

Event nodes represent script entry points.

Examples:

- **OnStart**
- **OnUpdate**
- **OnInteract**
- **OnTriggerEnter**
- **OnTriggerExit**
- **OnDamage**

### Visual Node Example

```
[ OnStart ]
      |
      v
[ PlaySound("Spawn") ]
```

### Compiles to FLES

```
OnStart:
    PlaySound("Spawn")
```

### Compiles to Bytecode

```
PushConst 0
CallApi 1, 1
Return
```

---

# 🔀 Flow Nodes

Flow nodes control execution order.

---

## **If Node**

### Visual

```
[ Condition ] → True → [ Block A ]
                 False → [ Block B ]
```

### FLES

```
If <condition>:
    <block A>
Else:
    <block B>
```

### Bytecode

```
<condition>
JumpIfFalse B
<A block>
Jump End
B:
<B block>
End:
```

---

## **Repeat Node**

A safe, bounded loop.

### Visual

```
[ Repeat 5 ]
      |
      v
[ PlaySound("Click") ]
```

### FLES

```
Repeat 5:
    PlaySound("Click")
```

### Bytecode

```
PushConst 5
StoreVar loopCounter
LoopStart:
PushVar loopCounter
PushConst 0
GreaterEqual
JumpIfTrue LoopEnd
<block>
PushVar loopCounter
PushConst 1
Sub
StoreVar loopCounter
Jump LoopStart
LoopEnd:
```

---

## **Return Node**

Ends execution of the current event.

### Bytecode

```
Return
```

---

# 📦 Variable Nodes

---

## **Set Variable**

### Visual

```
[ Value ] → [ SetVariable("Health") ]
```

### FLES

```
SetVariable("Health", value)
```

### Bytecode

```
<value>
StoreVar index
```

---

## **Get Variable**

### Visual

```
[ GetVariable("Health") ]
```

### Bytecode

```
PushVar index
```

---

# 🔢 Math Nodes

Math nodes map directly to bytecode operations.

| Node | Bytecode |
|------|----------|
| Add | `Add` |
| Subtract | `Sub` |
| Multiply | `Mul` |
| Divide | `Div` |
| Negate | `Neg` |

Example:

```
[ A ] + [ B ]
```

Compiles to:

```
<A>
<B>
Add
```

---

# 🔍 Comparison Nodes

| Node | Bytecode |
|------|----------|
| == | Equal |
| != | NotEqual |
| < | Less |
| <= | LessEqual |
| > | Greater |
| >= | GreaterEqual |

---

# 🔘 Boolean Nodes

| Node | Bytecode |
|------|----------|
| And | And |
| Or | Or |
| Not | Not |

---

# 🧩 API Nodes

API nodes call engine functions.

### Visual

```
[ PlaySound ]
    |
    v
[ "DoorOpen" ]
```

### FLES

```
PlaySound("DoorOpen")
```

### Bytecode

```
PushConst 0
CallApi 1, 1
```

---

# 🧱 Constant Nodes

Constant nodes push literal values.

Supported types:

- Number  
- Boolean  
- String  
- Vector3  
- Null  

### Bytecode

```
PushConst index
```

---

# 🧬 Node Graph Compilation

FLEN compiles into FLES using a depth‑first traversal:

1. Start at an event node  
2. Traverse connected nodes  
3. Emit FLES statements  
4. Compile FLES into FLEB bytecode  

### Example Graph

```
[ OnStart ]
      |
      v
[ SetVariable("Health", 100) ]
      |
      v
[ PlaySound("Spawn") ]
```

### Compiled FLES

```
OnStart:
    SetVariable("Health", 100)
    PlaySound("Spawn")
```

### Compiled Bytecode

```
PushConst 0
StoreVar 0
PushConst 1
CallApi 1, 1
Return
```

---

# 🛡️ Safety Rules

FLEN enforces:

- No recursion  
- No unbounded loops  
- No dynamic node creation  
- No hidden state  
- No nondeterministic behavior  
- No direct Unity access  

All unsafe operations must be wrapped in FLEA APIs.

---

# 🧩 Extending FLEN

Developers can add new nodes by:

1. Creating a new FLEA API function  
2. Creating a visual node that maps to that API  
3. Adding metadata (name, color, category)  
4. Adding compiler rules  

Example:

```
API: Teleport(position)
Node: Teleport Node
FLES: Teleport(Vector3)
Bytecode: PushConst, CallApi
```

---

# 📚 Next Steps

Continue to:

- **6_Unity_Integration.md** — Using FLE inside Unity  
- **7_Hello_World_Tutorial.md** — First script tutorial  
- **8_Advanced_Examples.md** — More complex examples  

This document explains how visual scripting maps to FLES.  
The next document explains how to embed FLE into Unity.
