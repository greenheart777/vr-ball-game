# VR Ball Arena Prototype

A simple VR prototype built for **Meta Quest / PCVR** using **Unity** and the **XR Interaction Toolkit**.  
The player can move freely around a **10×10 meter platform**, collect physical balls into an inventory (“pocket”), and throw balls to knock objects off the arena.  
Includes bonus mechanics, counters, UI elements, and an extensible gameplay structure.

---

![Gameplay Preview](demo.gif)

## 📌 Features

### 🔹 1. VR Locomotion
### 🔹 2. Arena (10×10 m)
### 🔹 3. Ball Pickup
### 🔹 4. Pocket Inventory
### 🔹 5. Throwing
### 🔹 6. Fall Detection
### 🔹 7. Bonus Mechanic

---
The prototype places the player on a 10×10 m VR arena where they can move freely, collect physical balls, and interact with objects using natural hand motions. Balls scattered across the platform can be picked up by touching or grabbing them, filling a simple “pocket” inventory displayed on a wrist-mounted UI.

Collected balls can be spawned into the player’s hand with the trigger and thrown using real physics. These throws can knock edge objects off the platform, contributing to a fall counter that tracks everything pushed beyond the arena boundaries.

A bonus mechanic adds variety: hitting a lever with a thrown ball opens a hatch that releases extra balls, allowing the player to restock their pocket and continue interacting with the environment.

---

## 🧩 Technologies & Tools
- Unity (2021+ recommended)  
- XR Interaction Toolkit  
- OpenXR / Oculus Integration  
- C# OOP architecture  
- ScriptableObject-based inventory  
- Physics-based interaction system

