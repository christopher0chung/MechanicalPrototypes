using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using MP3.MCV;
using MP3.Application;

public class Model_Game : MP3.MCV.Model.Model_Base {

    private Database_Character original_CharacterData;
    private Database_Game original_GameData;
    private Database_GameState original_StateData;

    public Database_Character working_CharacterData { get; private set; }
    public Database_Game working_GameData { get; private set; }
    public Database_GameState working_GameState { get; private set; }

    public Transform p0_Transform { get; private set; }
    public Transform p1_Transform { get; private set; }

    public void Awake()
    {
        LoadReferencesFromResources();
        CreateWorkingCopies();
    }

    private void LoadReferencesFromResources()
    {
        original_CharacterData = Resources.Load<Database_Character>("Data/CharacterData");
        original_GameData = Resources.Load<Database_Game>("Data/GameData");
        original_StateData = Resources.Load<Database_GameState>("Data/GameStateData");
    }

    private void CreateWorkingCopies()
    {
        working_CharacterData = ScriptableObject.Instantiate<Database_Character>(original_CharacterData);
        working_GameData = ScriptableObject.Instantiate<Database_Game>(original_GameData);
        working_GameState = ScriptableObject.Instantiate<Database_GameState>(original_StateData);

        p0_Transform = app.View_ParentObj.transform.Find("P0");
        p1_Transform = app.View_ParentObj.transform.Find("P1");
    }

    #region Public Controller Functions
    public void SetBoostVector(Type caller, GeneralTypes.ID id, Vector3 boostVector)
    {
        Debug.Assert(caller.GetType() != typeof(MP3.MCV.Controller.Controller_Base), "Model being changed outside of controller");

        working_GameState.SetThrustForce(this.GetType(), id, boostVector);
    }
    #endregion
}
