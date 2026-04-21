# 🚀 Advanced FLE Examples  
### Real‑World Scripts, Patterns, and Best Practices

This document contains advanced examples demonstrating how to build real gameplay logic using the Ferneon Logic Engine (FLE).  
These examples assume you already understand:

- FLES syntax  
- FLEB bytecode  
- FLER runtime  
- FLEA API usage  
- Unity integration  

These examples are designed to show **practical, production‑ready patterns**.

---

# 🧱 Example 1 — Door With Cooldown

A door that:

- Opens when interacted with  
- Closes after 3 seconds  
- Cannot be spammed  

### FLES Script

```
OnStart:
    SetVariable("IsOpen", false)
    SetVariable("Cooldown", 0)

OnInteract:
    If GetVariable("Cooldown") > 0:
        Return

    If GetVariable("IsOpen") == false:
        PlaySound("DoorOpen")
        SetVariable("IsOpen", true)
        SetVariable("Cooldown", 3)
    Else:
        PlaySound("DoorClose")
        SetVariable("IsOpen", false)
        SetVariable("Cooldown", 3)

OnUpdate:
    If GetVariable("Cooldown") > 0:
        SetVariable("Cooldown", GetVariable("Cooldown") - DeltaTime())
```

### Concepts Demonstrated

- Cooldowns  
- State toggling  
- Time‑based logic  
- Preventing spam interactions  

---

# 🤖 Example 2 — Simple AI Patrol Behavior

An NPC that:

- Walks between two points  
- Waits 1 second at each point  
- Loops forever  

### FLES Script

```
OnStart:
    SetVariable("TargetIndex", 0)
    SetVariable("WaitTime", 0)

OnUpdate:
    If GetVariable("WaitTime") > 0:
        SetVariable("WaitTime", GetVariable("WaitTime") - DeltaTime())
        Return

    If GetVariable("TargetIndex") == 0:
        MoveTo(Vector3(0,0,0))
        If DistanceTo(Vector3(0,0,0)) < 0.1:
            SetVariable("TargetIndex", 1)
            SetVariable("WaitTime", 1)
    Else:
        MoveTo(Vector3(10,0,0))
        If DistanceTo(Vector3(10,0,0)) < 0.1:
            SetVariable("TargetIndex", 0)
            SetVariable("WaitTime", 1)
```

### Concepts Demonstrated

- AI state machine  
- Movement  
- Distance checks  
- Timers  
- Looping behavior  

---

# 🔄 Example 3 — Multi‑Script Communication

Two scripts communicate using events.

### **Button Script**

```
OnInteract:
    SendEvent("Toggle")
```

### **Light Script**

```
OnStart:
    SetVariable("IsOn", false)

OnEvent("Toggle"):
    If GetVariable("IsOn") == false:
        SetVariable("IsOn", true)
        PlaySound("LightOn")
        SetLight(true)
    Else:
        SetVariable("IsOn", false)
        PlaySound("LightOff")
        SetLight(false)
```

### Concepts Demonstrated

- Cross‑script communication  
- Custom events  
- Multi‑entity logic  

---

# 🧨 Example 4 — Health, Damage, and Death Logic

A complete health system.

### FLES Script

```
OnStart:
    SetVariable("Health", 100)

OnDamage:
    SetVariable("Health", GetVariable("Health") - DamageAmount)

    If GetVariable("Health") <= 0:
        PlaySound("Death")
        SpawnObject("Explosion")
        DestroySelf()
```

### Concepts Demonstrated

- Damage handling  
- Death logic  
- Spawning effects  
- Cleanup  

---

# 🎯 Example 5 — Projectile Behavior

A projectile that:

- Moves forward  
- Deals damage  
- Destroys itself on impact  
- Times out after 5 seconds  

### FLES Script

```
OnStart:
    SetVariable("Lifetime", 5)

OnUpdate:
    MoveForward(10 * DeltaTime())
    SetVariable("Lifetime", GetVariable("Lifetime") - DeltaTime())

    If GetVariable("Lifetime") <= 0:
        DestroySelf()

OnTriggerEnter:
    DealDamage(25)
    SpawnObject("HitEffect")
    DestroySelf()
```

### Concepts Demonstrated

- Movement  
- Lifetime timers  
- Collision handling  
- Damage application  

---

# 🔐 Example 6 — Locked Door With Key Check

```
OnInteract:
    If PlayerHasItem("Key") == false:
        PlaySound("Locked")
        Return

    PlaySound("Unlock")
    SetVariable("IsOpen", true)
    OpenDoor()
```

### Concepts Demonstrated

- Inventory checks  
- Conditional access  
- Interaction gating  

---

# 🧩 Example 7 — Animation Controller

```
OnStart:
    PlayAnimation("Idle")

OnUpdate:
    If GetVariable("Speed") > 0.1:
        PlayAnimation("Run")
    Else:
        PlayAnimation("Idle")
```

### Concepts Demonstrated

- Animation switching  
- State‑based animation logic  

---

# 🧱 Example 8 — Area Trigger With Player Count

```
OnStart:
    SetVariable("PlayersInside", 0)

OnTriggerEnter:
    SetVariable("PlayersInside", GetVariable("PlayersInside") + 1)

OnTriggerExit:
    SetVariable("PlayersInside", GetVariable("PlayersInside") - 1)

OnUpdate:
    If GetVariable("PlayersInside") > 0:
        SetLight(true)
    Else:
        SetLight(false)
```

### Concepts Demonstrated

- Counting players  
- Area logic  
- Dynamic lighting  

---

# 🧠 Example 9 — Randomized Behavior (Deterministic)

FLE supports deterministic random via seeded API:

```
OnStart:
    SetVariable("Seed", 12345)

OnUpdate:
    If Random(GetVariable("Seed")) < 0.01:
        PlaySound("RandomBeep")
```

### Concepts Demonstrated

- Deterministic randomness  
- Seeded behavior  

---

# 🧱 Example 10 — Complex State Machine (Boss AI)

```
OnStart:
    SetVariable("State", "Idle")
    SetVariable("Timer", 2)

OnUpdate:
    SetVariable("Timer", GetVariable("Timer") - DeltaTime())

    If GetVariable("State") == "Idle":
        If GetVariable("Timer") <= 0:
            SetVariable("State", "Attack")
            SetVariable("Timer", 3)

    ElseIf GetVariable("State") == "Attack":
        ShootProjectile()
        If GetVariable("Timer") <= 0:
            SetVariable("State", "Idle")
            SetVariable("Timer", 2)
```

### Concepts Demonstrated

- Multi‑state AI  
- Timers  
- Attack patterns  
- Behavior loops  

---

# 🎉 You Now Have Real Gameplay Patterns

These examples demonstrate:

- State machines  
- Timers  
- AI logic  
- Interaction systems  
- Multi‑script communication  
- Projectiles  
- Health systems  
- Animation control  
- Deterministic randomness  

These patterns are the foundation of real gameplay systems in Ferneon.

---

# 📚 Next Steps

You can now:

- Build your own API functions  
- Create custom nodes  
- Implement complex behaviors  
- Build reusable gameplay modules  

FLE is powerful, safe, and flexible — and now you know how to use it at an advanced level.
