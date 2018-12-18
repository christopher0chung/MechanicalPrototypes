using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using MP3.Application;

[CreateAssetMenu(fileName = "GameStateData", menuName = "Data/Game State Data")]
public class Database_GameState : MP3.MCV.Data.Database_Base
{ 
    public float P0_Air { get; private set; }
    public float P0_Exh { get; private set; }

    public float P1_Air { get; private set; }
    public float P1_Exh { get; private set; }

    public bool splitScreen { get; private set; }

    public bool P0_CanHighlight { get; private set; }
    public GameObject P0_Higlighted { get; private set; }

    public bool P1_CanHighlight { get; private set; }
    public GameObject P1_Highlighted { get; private set; }

    public MP3.Application.GeneralTypes.FacingDirection P0_Facing;
    public MP3.Application.GeneralTypes.FacingDirection P1_Facing;

    public GeneralTypes.ControlStates P0_CS { get; private set; }
    public GeneralTypes.ControlStates P1_CS { get; private set; }

    public Vector3 P0_ThrustForce { get; private set; }
    public Vector3 P1_ThrustForce { get; private set; }

    public float P0_LampAngle { get; private set; }
    public float P1_LampAngle { get; private set; }

    //public List<MenuChoice> P0_MenuChoices_Active { get; private set; }
    //public List<MenuChoice> P1_MenuChoices_Active { get; private set; }

    public void SetThrustForce(Type functionCaller, GeneralTypes.ID id, Vector3 dir)
    {
        Debug.Assert(functionCaller.GetType() != typeof(MP3.MCV.Model.Model_Base), "Database being modified outside of model");

        if (id == GeneralTypes.ID._0)
            P0_ThrustForce = dir;
        else
            P1_ThrustForce = dir;
    }

}

//public class MenuChoice
//{
//    public GeneralTypes.MenuVerbs Verb { get; private set; }
//    public GeneralTypes.Items ObjectItem { get; private set; }
//    public GeneralTypes.Equipment ObjectEquipment { get; private set; }

//    public bool itemType { get; private set; }

//    public MenuChoice(GeneralTypes.MenuVerbs verb, GeneralTypes.Items objectItem)
//    {
//        Verb = verb;
//        ObjectItem = objectItem;
//        itemType = true;
//    }

//    public MenuChoice(GeneralTypes.MenuVerbs verb, GeneralTypes.Equipment objectEquipment)
//    {
//        Verb = verb;
//        ObjectEquipment = objectEquipment;
//        itemType = false;
//    }
//}
