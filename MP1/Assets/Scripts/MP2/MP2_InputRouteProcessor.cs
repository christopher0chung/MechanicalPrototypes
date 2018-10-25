using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MP2_InputRouteProcessor : MonoBehaviour, MP2_IConstructable {

    private SCG_EventManager _eventManager;
    //---------------------
    // private OpMenuController OpMenu
    // private CharacterMovementController CharMove
    // private CharacterInteractionController CharInt
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
        _fsm = new SCG_FSM<MP2_InputRouteProcessor>(this);
        _fsm.TransitionTo<State_CharacterControl>();

        _ControllerCreationFollowup();
	}
	
	void Update () {
        _fsm.Update();
	}

    #region Internal Functions
    MP2_CharacterMovementController c;

    private void _MakeAndReferenceControllers()
    {
        c = GameObject.Find("Managers").AddComponent<MP2_CharacterMovementController>();
        c.CalledAwake(_thisID);
    }

    private void _ControllerCreationFollowup()
    {
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
    public class State_CharacterControl : SCG_FSM<MP2_InputRouteProcessor>.State
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

    public class State_MenuControl : SCG_FSM<MP2_InputRouteProcessor>.State
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

    public class State_StationControl : SCG_FSM<MP2_InputRouteProcessor>.State
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