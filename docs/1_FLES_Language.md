# ✨ FLES — Ferneon Logic Engine Scripts  
### The Official Language Specification

FLES is the scripting language used by creators to define gameplay logic inside Ferneon.  
It is designed to be:

- Simple  
- Safe  
- Deterministic  
- Easy to compile  
- Easy to visualize  
- Sandbox‑restricted  

FLES is not a general‑purpose programming language.  
It is a **domain‑specific logic language** that compiles into FLEB bytecode and runs inside the FLER virtual machine.

---

# 🧩 Language Philosophy

FLES is built around these principles:

### **1. Creator‑friendly**
Non‑programmers should be able to understand it.

### **2. Visual-first**
Every FLES feature must map cleanly to FLEN visual nodes.

### **3. Deterministic**
Scripts must behave identically on all clients and servers.

### **4. Sandbox‑safe**
No access to:

- Unity  
- C#  
- Filesystem  
- Networking  
- Reflection  
- Threads  

### **5. Event‑driven**
Scripts respond to events rather than running continuously.

---

# 🧠 FLES Script Structure

A FLES script is composed of:

- **Events**
- **Variables**
- **Statements**
- **API calls**
- **Control flow blocks**

Example:

```
OnStart:
    SetVariable("Health", 100)

OnInteract:
    PlaySound("DoorOpen")
    SetVariable("IsOpen", true)
```

---

# 🔔 Events

Events are entry points triggered by the engine.

Common events include:

| Event Name | Description |
|------------|-------------|
| `OnStart` | Called when the script instance is created |
| `OnUpdate` | Called every frame |
| `OnInteract` | Called when a player interacts |
| `OnTriggerEnter` | Called when a collider enters |
| `OnTriggerExit` | Called when a collider exits |
| `OnDamage` | Called when the entity takes damage |

Events are defined like this:

```
OnStart:
    <statements>
```

---

# 📦 Variables

Variables are stored per‑instance and can be:

- Numbers  
- Booleans  
- Strings  
- Vectors  
- Entity references  
- Lists (optional future feature)  

### Setting a variable:

```
SetVariable("Health", 50)
```

### Getting a variable:

```
If GetVariable("Health") <= 0:
    DestroySelf()
```

---

# 🧱 Statements

FLES supports a small set of simple statements:

### **Assignment**
```
SetVariable("Speed", 10)
```

### **Conditionals**
```
If GetVariable("IsOpen") == false:
    PlaySound("DoorOpen")
```

### **Blocks**
```
If <condition>:
    <statements>
Else:
    <statements>
```

### **Loops (optional, limited)**
FLES does not support general loops, but may support:

```
Repeat 5:
    PlaySound("Click")
```

(Loops compile into safe, bounded bytecode.)

---

# 🧩 Expressions

Expressions can include:

- Literals  
- Variable reads  
- Comparisons  
- Boolean logic  
- Math operations  

Examples:

```
If GetVariable("Health") <= 0:
If DistanceToPlayer() < 3:
If GetVariable("IsOpen") == true:
If (GetVariable("Coins") + 1) > 10:
```

---

# 🛠️ API Calls

API calls are the bridge between FLES and the engine.

Examples:

```
PlaySound("Explosion")
MoveTo(Vector3(0, 5, 0))
SpawnObject("Crate")
SendEvent("Open")
```

Every API call maps to a FLEA function.

---

# 🔄 Control Flow

Supported control flow:

### **If / Else**
```
If <condition>:
    <statements>
Else:
    <statements>
```

### **Repeat**
```
Repeat 3:
    PlaySound("Beep")
```

### **Return**
```
Return
```

---

# 🧬 Types

FLES supports these basic types:

| Type | Example |
|------|---------|
| Number | `10`, `3.14` |
| Boolean | `true`, `false` |
| String | `"Hello"` |
| Vector | `Vector3(1, 2, 3)` |
| Entity | `Self`, `Player` |
| Null | `null` |

---

# 🧱 Example Script

```
OnStart:
    SetVariable("Health", 100)
    PlaySound("Spawn")

OnDamage:
    SetVariable("Health", GetVariable("Health") - DamageAmount)
    If GetVariable("Health") <= 0:
        PlaySound("Death")
        DestroySelf()

OnInteract:
    If GetVariable("IsOpen") == false:
        PlaySound("DoorOpen")
        SetVariable("IsOpen", true)
    Else:
        PlaySound("DoorClose")
        SetVariable("IsOpen", false)
```

---

# 🔧 Compilation Pipeline

FLES → FLEB → FLER

1. **FLES** is parsed into an AST  
2. AST is compiled into **FLEB bytecode**  
3. Bytecode is executed by **FLERInterpreter**  

---

# 📌 Notes for Engine Developers

- FLES is intentionally minimal  
- All logic must map cleanly to bytecode  
- No recursion  
- No unbounded loops  
- No dynamic code execution  
- No direct engine access  

---

# 📚 Next Steps

Continue to:

- **2_FLEB_Bytecode.md** — Bytecode format  
- **3_FLER_Runtime.md** — Runtime architecture  
- **4_FLEA_API.md** — API reference  
- **6_Unity_Integration.md** — Using FLE in Unity  

This document defines the language creators write.  
The next documents explain how it executes.
