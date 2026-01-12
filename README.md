# 🧠 Intricate Memories

**Intricate Memories** is an immersive VR installation exploring the relationship between memory, disinformation, and bodily perception. Built using Unity and TouchDesigner, this project blends real-time interaction, somatic storytelling, and speculative interfaces.

**Publication** https://dl.acm.org/doi/10.1145/3714394.3756248

## 👥 Authors
**Hui-Ting HONG**  
**Marta SHILOVA**

---

## 🌀 Concept Overview

In *Intricate Memories*, the user—called the *Intriquant*—enters a symbolic digital world via a VR headset. There, they meet their virtual double: the *Intriqué*. This body is made of iridescent scales—metaphors of embodied memories—roaming on a glowing digital tapestry representing **collective artificial memory**.

🧬 **Key Interactions:**
- Scattered memory fragments appear and respond to gestures.
- Able to play with **full-body** in VR **without any wearable tracking device**.
- Glowing glyphs **trigger scene transitions when looked at** (with zero neccesity of using the VR controller).
- A camera above captures the real body, projecting a "mental image" onto a celestial vault.

---

## 🗺 Keywords

`VR` `interactive installation` `fake news` `collective memory` `embodied interaction` `eye tracking` `Unity` `TouchDesigner` `DALL·E` `Meta Quest Pro` `ZED 2i`

---

## 🛠 Hardware Used

- 🎥 ZED 2i Camera (motion tracking)
- 🧠 Meta Quest Pro (VR rendering, eye tracking)
- 💻 Dell Precision 3680 (TouchDesigner + Unity environment)
- 🖥 External display for audience visuals

---

## 🎭 Narrative & Scenes

### Scene 1 – Collective Memory
- The *Intriqué* walks on a giant RAM-like landscape of circuit boards.
- Interacts with ephemeral fragments and glyphs.
- Scene fosters introspection, guided by gesture-driven affordances.

### Scene 2 – Fake News World
- Triggered by eye contact with glyphs.
- The *Intriqué* disappears into a suspended void flooded with smartphones.
- Fake news is generated in real-time (via OpenAI/DALL·E prompts).
- These affect the body’s visual representation based on emotional impact: fear, manipulation, confusion, etc.

**Exit Mechanism:**  
User must fix their gaze on a phone labeled "EXIT" to return to the base scene—now altered.

---

## ✅ Accomplished
- Full-body IK setup and avatar customization 
- Eye-gaze interaction system (glyphs, EXIT phone)
- Scene-switching logic and visual transformation tracking
- Audio coordination across transitions
- Fake news image generation pipeline with DALL·E 2

---

## 🧠 Technical Architecture

### VR Hardware
- Meta Quest Pro (used for both VR rendering and **eye tracking**)
- ZED 2i camera for **body pose estimation**

### Software Stack
- **Unity** (scene design, interaction logic, procedural animation)
- **TouchDesigner** (visual composition and external display)
- MetaSDK + Meta XR Plugin
- OpenAI API (DALL·E 2) for fake news image generation

---

## 🧪 Core Features

### 🧿 Eye Tracking (Meta Quest Pro)
- Detects gaze direction via IR cameras and raycasting.
- Triggers glyph activation and scene transitions.

### 🧍 Body Tracking
- Uses IK algorithms and upper body motion to infer full pose via [Meta Movement SDK](https://github.com/oculus-samples/Unity-Movement).
- Adapts the *Intriqué*’s form to reflect psychological transformations.

### 🗞 Fake News Generation
- Six predefined prompts create real-time DALL·E images every 3.5 seconds.
- Textures are applied to all renderers in the fake-news scene.

### 🎧 Audio Control & Synchronization
- Cross-scene audio variables (e.g., `hasPlayedFirstAudioSource`)
- Dynamic soundscape with scene-specific ambient sound and voice messages.
- Timing and audio triggers controlled through shared variable scripts.

---

## 🎧 Sound Design

### Scene 1 – Collective Memory
- Ambient textures, soft drones, introspective.
- Glyph interaction: crystal chimes, radiant sound.

### Scene 2 – Fake News
- Glitched pop chaos, sensory overload.
- EXIT: digital breath, hard click.

Voiceovers include:
- "Welcome to the memory space..."
- "You have returned, but your body is affected..."

<!-- ## 🔧 Improvements to Consider
- Improve lower-body leg generation accuracy
- Integrate direct eye-tracking data from MetaSDK for finer control
- Expand fake news prompts and visual variation
- Add more body morph states for nuanced disinformation effects -->

---

## 📄 License
MIT License (or specify your custom license)

---

## 🙌 Acknowledgements
This project was developed as part of the PHC ORCHID project led by Pr. Chu-Yin CHEN.

