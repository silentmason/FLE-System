# 🧩 FLEA — Ferneon Logic Engine API  
### The Safe Interface Between Scripts and the Engine

The **Ferneon Logic Engine API (FLEA)** is the only way FLES scripts can interact with the game world.  
Scripts cannot access Unity, C#, or engine internals directly — all interaction must go through **registered API functions**.

FLEA ensures:

- Safety  
- Determinism  
- Sandboxing  
- Engine‑agnostic behavior  
- Clear separation between scripting and engine code  

---

# 🧱 What Is a FLEA API Function?

A FLEA API function is a **C# method registered with an integer ID**.

Scripts call APIs using bytecode:

```
CallApi apiId, argCount
```

The interpreter:

1. Pops `argCount` values from the stack  
2. Calls the registered API handler  
3. The handler may push a return value  

---

# 🧩 API Registration

APIs are registered in the `FLEAApiRegistry`.

### Example

```csharp
ApiRegistry.Register(0, (instance, argCount) =>
{
    var value = instance.Stack.Pop();
    Debug.Log("[FLE] " + value);
});
```

### API Signature

```
void ApiHandler(FLESProgramInstance instance, int argCount)
```

Arguments are popped from the instance’s stack.

Return values are pushed onto the stack.

---

# 🧠 API Call Flow

Example FLES:

```
Print("Hello")
```

Compiles to:

```
PushConst 0
CallApi 0, 1
```

Runtime flow:

1. Push `"Hello"`  
2. Call API ID 0 with 1 argument  
3. API pops `"Hello"`  
4. API prints it  
5. (Optional) API pushes a return value  

---

# 🧱 API Safety Rules

FLEA enforces strict safety:

### ❌ APIs cannot:
- Access the filesystem  
- Access the network  
- Execute arbitrary C#  
- Modify Unity objects directly (unless wrapped safely)  
- Allocate large memory blocks  
- Block the main thread  
- Perform nondeterministic operations (unless explicitly allowed)  

### ✔ APIs can:
- Play sounds  
- Move objects  
- Spawn entities  
- Modify script variables  
- Send events  
- Query world state  
- Interact with Unity through safe wrappers  

---

# 🧩 Built‑In API Categories

FLEA includes several categories of built‑in APIs.

---

## 🔊 Audio APIs

### **PlaySound(soundId)**  
Plays a sound at the entity’s position.

Arguments:

1. `string soundId`

Example:

```
PlaySound("DoorOpen")
```

---

## 🎮 Entity APIs

### **MoveTo(position)**  
Moves the entity to a world position.

Arguments:

1. `Vector3 position`

---

### **DestroySelf()**  
Destroys the entity this script is attached to.

Arguments: none

---

### **SetVariable(name, value)**  
Sets a script variable.

---

### **GetVariable(name)**  
Returns a script variable.

---

## 🧍 Player APIs

### **GetPlayerPosition(playerId)**  
Returns the world position of a player.

---

### **SendEvent(eventName)**  
Triggers another event on the same script instance.

---

## 🌍 World APIs

### **SpawnObject(prefabId)**  
Spawns an object in the world.

---

### **Raycast(origin, direction)**  
Performs a safe raycast.

---

# 🧱 API Return Values

APIs may push a return value onto the stack.

Example:

```csharp
ApiRegistry.Register(5, (inst, args) =>
{
    var pos = inst.Entity.transform.position;
    inst.Stack.Push(pos);
});
```

FLES usage:

```
SetVariable("MyPos", GetPosition())
```

Bytecode:

```
CallApi 5, 0
StoreVar 0
```

---

# 🧩 API ID Assignment

API IDs are integers:

```
0 → Print
1 → PlaySound
2 → MoveTo
3 → SetVariable
4 → GetVariable
5 → GetPosition
...
```

You may:

- Assign IDs manually  
- Auto‑assign IDs  
- Generate IDs at build time  

IDs must remain stable across builds.

---

# 🧪 Example: Full API Implementation

### C# API

```csharp
ApiRegistry.Register(10, (inst, args) =>
{
    var amount = (float)inst.Stack.Pop();
    var health = (float)inst.GetVariable("Health");
    inst.SetVariable("Health", health - amount);
});
```

### FLES Script

```
OnDamage:
    DamageSelf(10)
```

### Bytecode

```
PushConst 10
CallApi 10, 1
Return
```

---

# 🧱 Error Handling

APIs should validate:

- Argument count  
- Argument types  
- Null values  
- Out‑of‑range values  

Example:

```csharp
if (argCount != 1)
    throw new FLEApiException("PlaySound expects 1 argument");
```

---

# 🧩 Determinism Considerations

APIs must avoid:

- Random numbers (unless seeded)  
- Time‑based operations  
- Physics queries with nondeterministic results  
- Floating‑point drift  

If nondeterminism is required, it must be explicitly documented.

---

# 📚 Next Steps

Continue to:

- **5_FLEN_Visual_Nodes.md** — Visual scripting system  
- **6_Unity_Integration.md** — Using FLE inside Unity  
- **7_Hello_World_Tutorial.md** — First script tutorial  

This document defines how scripts interact with the engine.  
The next document explains how visual scripting maps to FLES.
