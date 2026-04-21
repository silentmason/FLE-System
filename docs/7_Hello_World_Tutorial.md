# 🌟 FLE Tutorial — Your First Script  
### A Beginner‑Friendly Guide to Running Your First FLE Script in Unity

This tutorial walks you through creating and running your first script using the **Ferneon Logic Engine (FLE)**.

By the end, you will:

- Create a simple FLES script  
- Compile it into bytecode  
- Load it into Unity  
- Attach it to a GameObject  
- Trigger events  
- See the script run in the Unity console  

This is the perfect starting point for new developers.

---

# 🧱 Step 1 — Create a Simple FLES Script

Let’s start with the simplest possible script:

```
OnStart:
    Print("Hello from FLE!")
```

This script prints a message when the entity spawns.

---

# 🧩 Step 2 — Compile the Script into FLEB Bytecode

Your FLES compiler will convert the script into:

- Constants  
- Bytecode instructions  
- Variable table  
- Event entry points  

Conceptual output:

```
Constants:
0 → "Hello from FLE!"

Bytecode:
0: PushConst 0
1: CallApi 0, 1
2: Return

EventEntryPoints:
OnStart → 0
```

This becomes a `FLESProgram`.

---

# 📦 Step 3 — Create a FLESProgram Asset

In Unity, you can load the program however you prefer:

- ScriptableObject  
- JSON  
- Binary file  
- Resources folder  
- AssetBundle  

Example (C#):

```csharp
var program = new FLESProgram(
    "HelloWorld",
    bytecodeArray,
    constantsArray,
    variableTable,
    eventTable
);
```

---

# 🎮 Step 4 — Add the FLE Runtime to Your Scene

Make sure your scene has a GameObject with the **FLEManager** component:

```
FLE_Runtime
└── FLEManager.cs
```

This initializes:

- FLERContext  
- FLERInterpreter  
- FLEAApiRegistry  

And registers default APIs like `Print`.

---

# 🧱 Step 5 — Attach the Script to a GameObject

Create a component called **FLEScriptComponent**:

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
}
```

Add it to any GameObject:

```
Cube
└── FLEScriptComponent (Program = HelloWorld)
```

Set:

- **Program** → your compiled script  
- **EntityId** → any unique integer  

---

# 🚀 Step 6 — Press Play

When the scene starts, Unity triggers:

```
OnStart
```

FLE receives the event and executes:

```
PushConst 0
CallApi 0, 1
Return
```

Which calls the `Print` API.

---

# 🎉 Step 7 — See the Output

Open the Unity Console.

You should see:

```
[FLE] Hello from FLE!
```

Congratulations — you just ran your first FLE script!

---

# 🧪 Bonus: Add More Logic

Try expanding your script:

```
OnStart:
    Print("Script started!")
    SetVariable("Health", 100)

OnUpdate:
    Print("Tick")
```

Or add interaction:

```
OnInteract:
    Print("You interacted with me!")
```

---

# 🧱 What You Learned

You now know how to:

- Write a FLES script  
- Compile it into FLEB bytecode  
- Load it into Unity  
- Attach it to a GameObject  
- Fire events  
- Execute logic through the runtime  

This is the foundation for all future FLE development.

---

# 📚 Next Steps

Continue to:

- **8_Advanced_Examples.md** — More complex scripts  
- **4_FLEA_API.md** — Learn how to expose engine features  
- **5_FLEN_Visual_Nodes.md** — Visual scripting system  
- **6_Unity_Integration.md** — Full Unity integration guide  

You’re now ready to build real gameplay logic with FLE!
