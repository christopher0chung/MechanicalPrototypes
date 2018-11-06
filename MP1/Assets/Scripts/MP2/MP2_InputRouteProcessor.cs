using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MP2_InputRouteProcessor : MonoBehaviour, MP2_IConstructable {

    private SCG_EventManager _eventManager;

    private MP2_CharacterMovementController _charMove;
    private MP2_CharacterInteractionController _charInt;
    
    //---------------------
    // private OpMenuController OpMenu
    // private StationController Stn
    //---------------------

    private SCG_FSM<MP2_InputRouteProcessor> _fsm;

    private Enum_MP2_ID _thisID;


    public void CalledAwake(Enum_MP2_ID id) {
        _thisID = id;

        _eventManager = MP2_ServiceLocator.instance.EventManager;
        _eventManager.Register<E_ControlStateSwitched>(ControlStateSwitchHandler);

        _MakeAndReferenceControllers();
    }

	public void CalledStart () {
        //Debug.Log(this.name.ToString() + " for id number " + _thisID + " was called");
        _fsm = new SCG_FSM<MP2_InputRouteProcessor>(this);
        _fsm.TransitionTo<State_CharacterControl>();

        _ControllerCreationFollowup();
	}
	
	void Update () {
        //Debug.Log(this.name.ToString() + " for id number " + _thisID + " is in Update");

        _fsm.Update();
	}

    #region Internal Functions
    private List<MP2_IConstructable> _myID_Constructables;

    private void _MakeAndReferenceControllers()
    {
        GameObject managersGO = GameObject.Find("Managers");

        _myID_Constructables = new List<MP2_IConstructable>();

        _charMove = managersGO.AddComponent<MP2_CharacterMovementController>();
        _myID_Constructables.Add(_charMove);

        _charInt = managersGO.AddComponent<MP2_CharacterInteractionController>();
        _myID_Constructables.Add(_charInt);

        _myID_Constructables.Add(managersGO.AddComponent<MP2_OpMenuController>());
        _myID_Constructables.Add(managersGO.AddComponent<MP2_StationController>());


        foreach (MP2_IConstructable c in _myID_Constructables)
            c.CalledAwake(_thisID);
    }

    private void _ControllerCreationFollowup()
    {
        foreach (MP2_IConstructable c in _myID_Constructables)
            c.CalledStart();
    }
    #endregion

    #region Handlers
    public void ControlStateSwitchHandler(SCG_Event e)
    {
        E_ControlStateSwitched css = e as E_ControlStateSwitched;

        if (css != null)
        {
            if (css.switchingID == _thisID)
            {
                if (css.switchingState == Enum_MP2_ControlState.Character)
                    _fsm.TransitionTo<State_CharacterControl>();
                else if (css.switchingState == Enum_MP2_ControlState.Menu)
                    _fsm.TransitionTo<State_MenuControl>();
                else 
                    _fsm.TransitionTo<State_StationControl>();
            }
        }
    }
    #endregion

    #region Control States
    public class State_Base : SCG_FSM<MP2_InputRouteProcessor>.State
    {

    }
    public class State_CharacterControl : State_Base
    {
        public override void OnEnter()
        {
            
        }

        public override void Update()
        {
            
        }

        public override void OnExit()
        {
            
        }

    }

    public class State_MenuControl : State_Base
    {
        public override void OnEnter()
        {

        }

        public override void Update()
        {

        }

        public override void OnExit()
        {

        }

    }

    public class State_StationControl : State_Base
    {
        public override void OnEnter()
        {

        }

        public override void Update()
        {

        }

        public override void OnExit()
        {

        }

    }
    #endregion
}