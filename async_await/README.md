# Introduction
You can execute async/await by calling the object to which the async/await class is assigned from a Playmaker action. If you want to execute at an arbitrary timing, make the object disactive and then make it active at the timing of execution.

# Required
Unity 2021.3.10f1 or above.
Package: Unitask
Asset: Playmaker 1.9.5.f3

# Usage
awaitHandler.cs is a custom Playmaker action.
Create an empty GameObject and add asyncHandler.cs as a component.
Assign the FSM to the GameObject's asyncHandler inspector.
In the custom action of the awaitHandler, set the global transtion.
Deactivate the GameObject.

Activating the GameObject will execute the asyncHandler.