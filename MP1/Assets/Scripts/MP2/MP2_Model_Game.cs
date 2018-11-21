using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MP2_Model_Game : MonoBehaviour {

    public float P0_Air { get; private set; }
    public float P0_Exh { get; private set; }

    public float P1_Air { get; private set; }
    public float P1_Exh { get; private set; }

    public bool splitScreen { get; private set; }

    public bool P0_CanHighlight { get; private set; }
    public GameObject P0_Higlighted { get; private set; }

    public bool P1_CanHighlight { get; private set; }
    public GameObject P1_Highlighted { get; private set; }

    public Enum_MP2_ControlState P0_CS { get; private set; }
    public Enum_MP2_ControlState P1_CS { get; private set; }

    public List<MenuChoice> P0_MenuChoices_Active { get; private set; }
    public List<MenuChoice> P1_MenuChoices_Active { get; private set; }

    #region Controller Functions
    public void AirChargeUpdate(Enum_MP2_ID who, float rate)
    {
        if (who == Enum_MP2_ID.Player0)
        {
            P0_Air += rate * Time.deltaTime;
            if (P0_Air >= 100)
                P0_Air = 100;
        }
        else
        {
            P1_Air += rate * Time.deltaTime;
            if (P1_Air >= 100)
                P1_Air = 100;
        }
    }

    public void AirDishargeUpdate(Enum_MP2_ID who, float rate)
    {
        if (who == Enum_MP2_ID.Player0)
        {
            P0_Air -= rate * Time.deltaTime;
            if (P0_Air <= 0)
                P0_Air = 0;
        }
        else
        {
            P1_Air -= rate * Time.deltaTime;
            if (P1_Air <= 0)
                P1_Air = 0;
        }
    }

    public void ExhIncreaseUpdate(Enum_MP2_ID who, float rate)
    {
        if (who == Enum_MP2_ID.Player0)
        {
            P0_Exh += rate * Time.deltaTime;
            if (P0_Exh >= 100)
                P0_Exh = 100;
        }
        else
        {
            P1_Exh += rate * Time.deltaTime;
            if (P1_Exh >= 100)
                P1_Exh = 100;
        }
    }

    public void ExhDecreaseUpdate(Enum_MP2_ID who, float rate)
    {
        if (who == Enum_MP2_ID.Player0)
        {
            P0_Exh -= rate * Time.deltaTime;
            if (P0_Exh <= 0)
                P0_Exh = 0;
        }
        else
        {
            P1_Exh -= rate * Time.deltaTime;
            if (P1_Exh <= 0)
                P1_Exh = 0;
        }
    }

    public void SetSplitScreen(bool isSplitScreen)
    {
        splitScreen = isSplitScreen;
    }

    public void SetCanHighlight(Enum_MP2_ID who, bool canSetHighlight)
    {
        if (who == Enum_MP2_ID.Player0)
            P0_CanHighlight = canSetHighlight;
        else
            P1_CanHighlight = canSetHighlight;
    }

    public void IsHighlighted (Enum_MP2_ID who, GameObject highlightedObject)
    {
        if (who == Enum_MP2_ID.Player0)
            P0_Higlighted = highlightedObject;
        else
            P1_Highlighted = highlightedObject;
    }

    public void SetMenuChoices(Enum_MP2_ID who, List<MenuChoice> choices)
    {
        if (who == Enum_MP2_ID.Player0)
            P0_MenuChoices_Active = choices;
        else
            P1_MenuChoices_Active = choices;
    }

    public void UpdateControlState(Enum_MP2_ID who, Enum_MP2_ControlState newState)
    {
        if (who == Enum_MP2_ID.Player0)
            P0_CS = newState;
        else
            P1_CS = newState;
    }
    #endregion
}

public enum Enum_MP2_MenuChoices { Operate, Install, Remove, Read }

public class MenuChoice
{
    public Enum_MP2_MenuChoices Verb { get; private set; }
    public Enum_MP2_ItemType ObjectItem { get; private set; }
    public Enum_MP2_ItemType ObjectEquipment { get; private set; }

    public bool itemType { get; private set; }

    public MenuChoice (Enum_MP2_MenuChoices verb, Enum_MP2_ItemType objectItem)
    {
        Verb = verb;
        ObjectItem = objectItem;
        itemType = true;
    }

    public MenuChoice (Enum_MP2_MenuChoices verb, Enum_MP2_ID objectEquipment)
    {
        Verb = verb;
        itemType = false;
    }
}