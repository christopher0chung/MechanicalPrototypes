using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MP2_Events : SCG_Event {}

public class E_CharConstructed : MP2_Events
{
    public Enum_MP2_ID constructedID;
    public GameObject characterGO;

    public E_CharConstructed (Enum_MP2_ID id, GameObject charGO)
    {
        constructedID = id;
        characterGO = charGO;
    }
}

public class E_CharViewHeadlampConstructed: MP2_Events
{
    public Enum_MP2_ID constructedID;
    public Transform headlampTransform;

    public E_CharViewHeadlampConstructed( Enum_MP2_ID id, Transform headlamp)
    {
        constructedID = id;
        headlampTransform = headlamp;
    }
}

public class E_CharViewHandConstructed: MP2_Events
{
    public Enum_MP2_ID constructedID;
    public MP2_View_Item_Held handView;

    public E_CharViewHandConstructed (Enum_MP2_ID id, MP2_View_Item_Held view)
    {
        constructedID = id;
        handView = view;
    }
}

public class E_ItemManagerConstructed : MP2_Events
{
    public E_ItemManagerConstructed() { }
}

public class E_EqptManagerConstructed : MP2_Events
{
    public E_EqptManagerConstructed() { }
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