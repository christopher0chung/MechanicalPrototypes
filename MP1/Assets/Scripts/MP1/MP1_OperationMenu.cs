using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MP1_OperationMenu : MonoBehaviour {

    private SCG_FSM<MP1_OperationMenu> _fsm_P0;

	void Start () {
        _fsm_P0 = new SCG_FSM<MP1_OperationMenu>(this);
        _fsm_P0.TransitionTo<IdleState>();
	}
	
	void Update () {
        _fsm_P0.Update();
	}

    #region P0States
    public class State_Base : SCG_FSM<MP1_OperationMenu>.State
    {

    }

    public class IdleState : State_Base
    {

    }
    #endregion
}
