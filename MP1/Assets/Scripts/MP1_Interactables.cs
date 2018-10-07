using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MP1_Interactables : MonoBehaviour
{
    public abstract void Muscle();

    public abstract void Operate();
}

public enum Items { O2Tank, ArTank, PatchPlate, Battery, Obstruction }
public enum Equipment { Welder, Door, O2Charger, ArCharger, BatteryCharger, Accumulator, PneumaticWrench, Lockers }
public enum Stations { Helm, LeeHelm, GearHoist, Crane }

public enum Nodes { Leak, Rupture, Valve, InopGate, Jammed}
public enum Operator { WaterTank, BallastTank, Engine, Light, LinearOperator, Claw }

//protected virtual void Carry()
//{
//    // Handles parenting, and spacing when an object is carried by a player.
//}

//protected virtual void Drop()
//{
//    // Handles parenting, and spacing when an object is dropped by a player.
//}

//protected virtual void Operate_Discrete()
//{
//    // Handles interactions that happen discretely.
//    // Used for single use interactions.
//    // Not used for toggling.
//}

//protected virtual void Operate_Toggle()
//{
//    // Handles interactions that happen discretely with multiple states.
//    // Used for binary state operations.
//    // On/Off switches.
//}

//protected virtual void Operate_Continuous()
//{
//    // Handles operations over time.
//    // Ex. Pressure switch type of action.
//    // Ex. Something that takes time to complete.
//}

//protected virtual void Unman()
//{
//    // Handles cleanup of abandoning an interactable.
//    // Ex. Determines what should happen when a task is partially complete.
//    // Ex. Determines what happens when when a station isn't continued to be manned.
//}