using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MP3.MCV.Controller;

public class Controller_Character : Controller_Base {

    public MP3.Application.GeneralTypes.ID playerID;

    private Model_Game _model;
    private Database_GameState _gameState;

    #region Scratch Variables
    private Vector3 _thrustVector_Scratch;
    #endregion

    void Awake()
    {
        Initialize();
    }

	void Start () {
        Setup();
	}
	

	void Update () {

        Boost();
        HeadlampAngle();
    }

    private void Initialize()
    {

    }

    private void Setup()
    {
        _model = app.Model_ParentObj.transform.GetComponentInChildren<Model_Game>();
        _gameState = app.Model_ParentObj.transform.GetComponentInChildren<Model_Game>().working_GameState;
    }

    private void Boost()
    {
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
        {
            _thrustVector_Scratch = Vector3.zero;
            if (Input.GetKey(KeyCode.A))
                _thrustVector_Scratch += Vector3.right * -1;
            if (Input.GetKey(KeyCode.D))
                _thrustVector_Scratch += Vector3.right * 1;
            if (Input.GetKey(KeyCode.W))
                _thrustVector_Scratch += Vector3.up * 1;
            if (Input.GetKey(KeyCode.S))
                _thrustVector_Scratch += Vector3.up * -1;

            _gameState.SetThrustForce(playerID, _thrustVector_Scratch);
        }
        else
            _gameState.SetThrustForce(playerID, Vector3.zero);
    }

    private void HeadlampAngle()
    {
        float upDownRate;

        if (Input.GetKey(KeyCode.U) || Input.GetKey(KeyCode.J))
        {
            if (Input.GetKey(KeyCode.U))
                upDownRate = 30;
            if (Input.GetKey(KeyCode.J))
                upDownRate = -30;
        }
        else
            upDownRate = 0;



    }
}
