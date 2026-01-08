# Component State Machine

A clean and explicit component-level state system for Unity.  
No magic. No reflection. Just states.

---

## 🎯 Purpose

This package provides a lightweight foundation for managing **component states** in Unity.

The system focuses on **explicit state control at the component level**, avoiding:
- whole GameObject state switching
- hidden behavior
- automatic serialization
- reflection-based logic

You define what a state is and how it is applied.  
The system only orchestrates state changes.

---

## 🧱 Core Components

### ComponentStateMachine

A generic base class that represents a state machine for a single component.

**Responsibilities:**
- Stores a set of predefined states
- Applies states explicitly
- Tracks the current active state
- Exposes change events

**Key characteristics:**
- Fully type-safe
- Works with any component type
- State logic is user-defined
- No assumptions about state structure

---

### ComponentStateData

A simple data container that maps a state identifier to its state data.

**Responsibilities:**
- Defines the relationship between a state ID and its state values
- Serves as a serialized representation of a state

---

### ComponentStateMachineGroup

A grouping utility that allows multiple component state machines to be controlled together.

**Responsibilities:**
- Automatically collects child state machines
- Applies the same state ID to all grouped machines
- Provides centralized state control

**Notes:**
- State machines can opt out of grouping
- Groups do not manage or define state logic
- Groups only orchestrate state changes

---

## 🧠 Design Principles

- Component-level state management
- Explicit over implicit behavior
- Composition over automation
- Minimal core with no hidden logic
- Architecture-friendly by design

---

## 🚫 Out of Scope

This system intentionally does NOT provide:
- GameObject or scene state saving
- Automatic state generation
- Reflection-based serialization
- Animator or timeline replacement
- Save/load functionality

It is designed to be a **small, focused core**, not a full state framework.

