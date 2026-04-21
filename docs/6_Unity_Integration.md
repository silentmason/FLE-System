# 🎮 Unity Integration Guide  
### How to Use the Ferneon Logic Engine (FLE) Inside Unity

This document explains how to embed the **Ferneon Logic Engine (FLE)** into a Unity project, create a runtime, load scripts, fire events, and expose safe API functions.

This guide is for **Unity developers**, not creators.  
Creators never see this — they use FLEN visual scripting.

---

# 🧱 Overview

To use FLE inside Unity, you must:

1. Create a **runtime manager** (`FLEManager`)
2. Register **FLEA API functions**
3. Load compiled **FLESProgram** assets
4. Create **FLESProgramInstance** objects for entities
5. Fire events from Unity (OnStart, Update, triggers, etc.)
6. Optionally create components that attach scripts to GameObjects

This document walks through each step.

---

# 🧩 1. Creating the FLE Runtime

Create a script called **FLEManager.cs** and place it in your Unity project.

```csharp
using UnityEngine;
using Ferneon.FLE.FLER;
using Ferneon.FLE.FLEA;

public class FLEManager : MonoBehaviour
{
    public static FLEManager Instance { get; private set; }

    public FLERContext Context { get; private set; }
    public FLEAApiRegistry ApiRegistry { get; private set; }
    public FLERInterpreter Interpreter { get; private set; }

    private void Awake()
    {
        Instance = this;

        ApiRegistry = new FLEAApiRegistry();
        Interpreter = new FLERInterpreter(ApiRegistry);
        Context = new FLERContext(Interpreter);

        RegisterDefaultAPIs();
    }

    private void RegisterDefaultAPIs()
    {
        // Example API: Print to console
        ApiRegistry.Register(0, (inst, args) =>
        {
            var value = inst.Stack.Pop();
            Debug.Log("[FLE] " + value);
        });
    }
}
```

This creates the full runtime:

- API registry  
- Interpreter  
- Context  
- Default APIs  

Place this on a GameObject in your scene (e.g., `FLE_Runtime`).

---

# 📦 2. Loading a FLES Program

A compiled script is represented as a `FLESProgram`.

You can load it from:

- ScriptableObject  
- JSON  
- Binary file  
- Resources folder  
- AssetBundle  

Example:

```csharp
var program = new FLESProgram(
    "Example",
    bytecodeArray,
    constantsArray,
    variableTable,
    eventTable
);
```

---

# 🧬 3. Creating a Script Instance

Each entity that uses a script needs a **FLESProgramInstance**.

Example:

```csharp
var instance = FLEManager.Instance.Context.AddInstance(program, entityId);
```

Where:

- `program` = the compiled script  
- `entityId` = unique ID for the GameObject or entity  

You can store this instance in a Unity component.

---

# 🎮 4. Firing Events From Unity

Unity events must be forwarded to FLE.

Example component:

```csharp
public class FLEEntity : MonoBehaviour
{
    public int EntityId;
    public FLESProgram Program;
    private FLESProgramInstance instance;

    void Start()
    {
        instance = FLEManager.Instance.Context.AddInstance(Program, EntityId);
        FLEManager.Instance.Context.FireEvent(FLESEventType.OnStart, EntityId);
    }

    void Update()
    {
        FLEManager.Instance.Context.FireEvent(FLESEventType.OnUpdate, EntityId);
    }

    void OnTriggerEnter(Collider other)
    {
        FLEManager.Instance.Context.FireEvent(FLESEventType.OnTriggerEnter, EntityId);
    }
}
```

This connects Unity → FLE.

---

# 🧩 5. Registering Custom API Functions

To expose engine functionality to scripts, register APIs:

```csharp
ApiRegistry.Register(1, (inst, args) =>
{
    var message = inst.Stack.Pop();
    Debug.Log("FLE says: " + message);
});
```

Scripts can now call:

```
CALL_API 1, 1
```

Or in FLES:

```
Print(message)
```

---

# 🔊 Example: PlaySound API

```csharp
ApiRegistry.Register(2, (inst, args) =>
{
    var soundId = inst.Stack.Pop() as string;
    var entity = inst.GetUnityEntity();
    AudioManager.PlaySound(soundId, entity.transform.position);
});
```

---

# 🧱 6. Attaching Scripts to GameObjects

Create a component:

```csharp
public class FLEScriptComponent : MonoBehaviour
{
    public FLESProgram Program;
    public int EntityId;

    private FLESProgramInstance instance;

    void Start()
    {
        instance = FLEManager.Instance.Context.AddInstance(Program, EntityId);
        FLEManager.Instance.Context.FireEvent(FLESEventType.OnStart, EntityId);
    }

    void Update()
    {
        FLEManager.Instance.Context.FireEvent(FLESEventType.OnUpdate, EntityId);
    }
}
```

This is the recommended way to attach scripts to objects.

---

# 🧪 7. Minimal “Hello World” Example

### FLES Script

```
OnStart:
    Print("Hello from FLE!")
```

### Bytecode (conceptual)

```
PushConst 0
CallApi 0, 1
Return
```

### Unity Output

```
[FLE] Hello from FLE!
```

---

# 🛡️ 8. Safety Notes

FLE scripts cannot:

- Access Unity directly  
- Modify GameObjects without an API  
- Use threads  
- Access the filesystem  
- Access networking  
- Allocate large memory blocks  

All engine interaction must go through **FLEA API functions**.

---

# 📚 Next Steps

Continue to:

- **7_Hello_World_Tutorial.md** — First script tutorial  
- **8_Advanced_Examples.md** — More complex examples  

This document explains how to embed FLE into Unity.  
The next document walks through creating your first script.
