using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MP3.MCV.View;

public class View_Character : View_Base {

    public MP3.Application.GeneralTypes.ID charID;
    private Database_GameState myCharData;

    void Start()
    {
        Setup();
    }

	void Update () {
        if (charID == MP3.Application.GeneralTypes.ID._0)
            transform.position += myCharData.P0_ThrustForce * Time.deltaTime;
        else
            transform.position += myCharData.P1_ThrustForce * Time.deltaTime;
	}

    void Setup()
    {
        myCharData = app.Model_ParentObj.transform.GetComponentInChildren<Model_Game>().working_GameState;
    }
}
