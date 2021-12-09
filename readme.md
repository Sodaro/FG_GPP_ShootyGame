# Project: SpookyShooty
Created by David Bång for the course "Design Patterns for Game Development"\
Description: A short first-person shooter game using the HauntedPSX render pipeline.

## Patterns
### Singleton:
<pre>
-InputHandler.cs in "class InputHandler" as "InputHandler.Instance"
  -A single instance that is accessible anywhere, injects EarlyUpdate in which it checks and stores inputs.
-ObjectPool.cs, LinePool.cs, ServiceLocator.cs
</pre>
    
### ObjectPools
<pre>
-ObjectPool.cs in "abstract class ObjectPool<T>" and LinePool.cs in "class LinePool<BulletLine>
-An abstract pool which can store components in a "free list" (queue with only available objects)
-The class is a baseclass for other pools, LinePool derives from it and stores BulletLine components.
</pre>

### ServiceLocator
<pre>
-ServiceLocator.cs in "class ServiceLocator"
-A singleton containing a Dictionary with services of type IService.
-ServiceLocator is used to register non-monobehaviour objects and provide access to them to other classes.
-Services used: EventHandler.cs,  SceneHandler.cs
</pre>
### Component
<pre>
-PlayerController.cs in "class PlayerController"
-PlayerController uses the component pattern to separate responsibilities into different areas.
-It consists of 3 derived versions of "PlayerBaseComponent":
--PlayerPhysics.cs component, responsible for handling movement and gravity.
--PlayerWeaponHandler.cs component, responsible for handling the shooting.
--PlayerMouseLook.cs component, responsible for rotating the camera or player based on mouse movement.
</pre>
### StateMachine
<pre>
-EnemyFSM.cs in "class EnemyFSM"
-States: EnemyHostileState.cs, EnemyIdleState.cs
-The finite-state machine is used to control logic of the enemies in the game.
-The Enemy.cs calls update on it's instance of EnemyFSM, which updates the current active state that it stores.
--Enemy.cs has no control over the inner workings of the state machine.
--It does not know what state it is currently in.
--It merely updates the machine and let's the states decide when to switch.
</pre>
### Observer
<pre>
-EventHandler.cs in "class EventHandler"
-The EventHandler is a service which provides ways to listen to and raise events.
-This allows eg. the UIHandler to update the current displayed "Player Health" amount whenever the 
-player heals or takes damage, without actually needing any knowledge of the Player class
</pre>
