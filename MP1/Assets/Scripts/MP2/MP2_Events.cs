using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MP2_Events : SCG_Event {}

public class E_CharConstructed : MP2_Events
{
    public Enum_MP2_ID constructedID;

    public E_CharConstructed (Enum_MP2_ID id)
    {
        constructedID = id;
    }
}

public class E_ControlStateSwitched : MP2_Events
{
    public Enum_MP2_ID switchingID;
    public Enum_MP2_ControlState switchingState;

    public E_ControlStateSwitched (Enum_MP2_ID id, Enum_MP2_ControlState state)
    {
        switchingID = id;
        switchingState = state;
    }
}