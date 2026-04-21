# 🖥️ FLER — Ferneon Logic Engine Runtime  
### The Virtual Machine That Executes FLEB Bytecode

The **Ferneon Logic Engine Runtime (FLER)** is the virtual machine responsible for executing FLEB bytecode.  
It is designed to be:

- Fast  
- Deterministic  
- Sandbox‑safe  
- Lightweight  
- Easy to embed in Unity  
- Easy to extend with new APIs  

FLER is the heart of the scripting system — it runs every script instance in the world.

---

# 🧩 Runtime Architecture Overview

FLER is composed of four major components:

| Component | Purpose |
|----------|----------|
| **FLERInterpreter** | Executes bytecode instructions |
| **FLERContext** | Manages all running script instances |
| **FLESProgramInstance** | Holds the runtime state of a single script |
| **FLEAApiRegistry** | Stores safe API functions callable from scripts |

These components work together to execute FLEB programs safely and efficiently.

---

# 🧠 FLESProgramInstance  
### The Runtime State of a Script

A `FLESProgramInstance` represents a single running script attached to an entity.

It contains:

- Reference to the `FLESProgram` (bytecode + metadata)  
- Instruction pointer  
- Operand stack  
- Variable storage  
- Entity ID  
- Enabled flag  
- Optional debug info  

### Structure

```
FLESProgramInstance
│
├── FLESProgram Program
├── object[] Variables
├── Stack<object> Stack
├── int InstructionPointer
├── int EntityId
└── bool Enabled
```

Each entity in the world may have multiple script instances.

---

# 🧱 FLERContext  
### The Script Manager

`FLERContext` manages all running script instances in a scene or world.

Responsibilities:

- Add/remove script instances  
- Fire events (OnStart, OnUpdate, etc.)  
- Route events to the correct entry points  
- Execute scripts through the interpreter  
- Maintain deterministic ordering  

### Event Dispatch

When Unity triggers an event:

```
Context.FireEvent(FLESEventType.OnUpdate, entityId)
```

The context:

1. Finds all script instances for that entity  
2. Looks up the event entry point  
3. Sets the instruction pointer  
4. Executes the script via the interpreter  

---

# 🧩 FLERInterpreter  
### The Virtual CPU

The interpreter executes FLEB bytecode instructions.

It is a **stack‑based virtual machine**, meaning:

- Most instructions push or pop values  
- API calls pop arguments  
- Math pops operands and pushes results  
- Conditionals pop booleans  

### Execution Loop

A simplified version:

```
while (true)
{
    var instruction = program.Instructions[ip];

    switch (instruction.OpCode)
    {
        case PushConst:
            stack.Push(constants[instruction.A]);
            break;

        case Add:
            var b = stack.Pop();
            var a = stack.Pop();
            stack.Push(a + b);
            break;

        case CallApi:
            apiRegistry.Invoke(instruction.A, instance, instruction.B);
            break;

        case Return:
            return;
    }

    ip++;
}
```

The real implementation includes:

- Safety checks  
- Type validation  
- Bounds checking  
- Error handling  
- Debug hooks  

---

# 🧱 FLEAApiRegistry  
### Safe API Function Storage

Scripts cannot call Unity or C# directly.  
Instead, they call **FLEA API functions**, which are registered in the `FLEAApiRegistry`.

### Registering an API

```
ApiRegistry.Register(0, (inst, args) =>
{
    var value = inst.Stack.Pop();
    Debug.Log("[FLE] " + value);
});
```

### Calling an API from bytecode

```
CallApi 0, 1
```

This pops 1 argument and calls API ID 0.

---

# 🔄 Event Execution Flow

Here is the full lifecycle of an event:

```
Unity Event → FLERContext → FLESProgramInstance → FLERInterpreter → FLEA API → Engine
```

Example:

1. Unity calls `OnTriggerEnter`  
2. FLERContext receives the event  
3. It finds all script instances for that entity  
4. It jumps to the event’s entry point  
5. Interpreter executes bytecode  
6. Script calls API functions  
7. Engine performs actions (sound, movement, etc.)  

---

# 🧬 Determinism

FLER is deterministic by design:

- No random numbers unless provided by API  
- No time‑based operations unless provided by API  
- No floating‑point nondeterminism (optional fixed‑point mode)  
- No concurrency  
- No race conditions  
- No dynamic code execution  

This ensures consistent behavior across:

- Clients  
- Servers  
- Replays  
- Simulations  

---

# 🛡️ Safety Model

FLER enforces strict sandboxing:

- No direct access to Unity  
- No filesystem access  
- No networking  
- No reflection  
- No unsafe memory access  
- No recursion  
- No unbounded loops  
- No dynamic code generation  

All engine interaction must go through **FLEA API functions**.

---

# 🧪 Example: Full Execution of a Simple Script

### FLES Script
```
OnStart:
    PlaySound("Spawn")
    SetVariable("Health", 100)
```

### FLEB Bytecode
```
0: PushConst 0
1: CallApi 1, 1
2: PushConst 1
3: StoreVar 0
4: Return
```

### Runtime Flow

1. Unity triggers `OnStart`  
2. FLERContext jumps to instruction 0  
3. Interpreter pushes `"Spawn"`  
4. Interpreter calls API `PlaySound`  
5. Interpreter pushes `100`  
6. Interpreter stores into variable `Health`  
7. Interpreter returns  

---

# 🧱 Performance Characteristics

FLER is optimized for:

- Low memory usage  
- Fast stack operations  
- Minimal allocations  
- Predictable execution time  
- Large numbers of script instances  

A typical game can run:

- Hundreds of scripts per entity  
- Thousands of entities  
- Tens of thousands of script instances  

---

# 📚 Next Steps

Continue to:

- **4_FLEA_API.md** — API reference  
- **5_FLEN_Visual_Nodes.md** — Visual scripting system  
- **6_Unity_Integration.md** — Using FLE inside Unity  

This document explains how bytecode is executed.  
The next document explains how scripts interact with the engine.
