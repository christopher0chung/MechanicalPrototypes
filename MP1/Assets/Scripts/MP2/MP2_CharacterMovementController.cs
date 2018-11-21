using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MP2_CharacterMovementController : MonoBehaviour, MP2_IConstructable {

    private SCG_EventManager _eventManager;

    private GameObject _GO_controlledCharacter;
    private Rigidbody _RB_controlledCharacter;
    private MP2_Model_CharStats _model_controlledCharacter;

    private SCG_FSM<MP2_CharacterMovementController> _fsm;

    private Enum_MP2_ID _thisID;

    public void CalledAwake(Enum_MP2_ID id) {
        _thisID = id;

        _eventManager = MP2_ServiceLocator.instance.EventManager;
        _eventManager.Register<E_ControlStateSwitched>(ControlStateSwitchHandler);
    }

    public void CalledStart () {
        ConstructCharacter();

        _eventManager.Fire(new E_CharConstructed(_thisID, _GO_controlledCharacter));

        _fsm = new SCG_FSM<MP2_CharacterMovementController>(this);
        _fsm.TransitionTo<State_Boost>();
    }

    void Update () {
        _fsm.Update();
	}

    #region Internal Functions
    private void ConstructCharacter()
    {
        _GO_controlledCharacter = Instantiate<GameObject>(Resources.Load<GameObject>("Player_Empty"));

        _GO_controlledCharacter.name = _thisID.ToString();

        _RB_controlledCharacter = _GO_controlledCharacter.GetComponent<Rigidbody>();

        _model_controlledCharacter = _GO_controlledCharacter.GetComponent<MP2_Model_CharStats>();
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
                    _fsm.TransitionTo<State_Boost>();
                else
                    _fsm.TransitionTo<State_NoMovementInput>();
            }
        }
    }
    #endregion

    #region FSM States
    public class State_Base : SCG_FSM<MP2_CharacterMovementController>.State
    {
        protected RaycastHit[] _allHits;
        protected LayerMask _lm;
        protected float _maxRayLength = .15f;

        public override void OnEnter()
        {
            base.OnEnter();
            _lm = 1 << 10;
        }

        private Ray WalkBoostRay;

        protected void GetRayHits()
        {
            WalkBoostRay.origin = Context._GO_controlledCharacter.transform.position;
            WalkBoostRay.direction = Vector3.down;

            _allHits = Physics.RaycastAll(WalkBoostRay, _maxRayLength, _lm, QueryTriggerInteraction.Ignore);
            Debug.DrawLine(WalkBoostRay.origin, WalkBoostRay.origin + Vector3.down * _maxRayLength);
        }
    }

    public class State_Boost: State_Base
    {


        public override void OnEnter()
        {
            base.OnEnter();
            //Debug.Log("In Boost");
        }

        public override void Update()
        {
            base.Update();
            if (Context._thisID == Enum_MP2_ID.Player0)
                _Boost();
            _SwitchToWalkAssess();
        }

        public override void OnExit()
        {
            base.OnExit();
        }

        private void _SwitchToWalkAssess()
        {
            GetRayHits();
            if (_allHits.Length > 0)
                TransitionTo<State_Walk>();
        }

        private void _Boost()
        {
            if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.D))
            {

            }
            else if (Input.GetKey(KeyCode.A))
            {
                Context._RB_controlledCharacter.AddForce(Vector3.left * 3000);
                Context._model_controlledCharacter.SetDir(FacingDirection.Left);
            }
            else if (Input.GetKey(KeyCode.D))
            {
                Context._RB_controlledCharacter.AddForce(Vector3.right * 3000);
                Context._model_controlledCharacter.SetDir(FacingDirection.Right);
            }

            if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.S))
            {

            }
            else if (Input.GetKey(KeyCode.W))
            {
                Context._RB_controlledCharacter.AddForce(Vector3.up * 3000);
            }
            else if (Input.GetKey(KeyCode.S))
            {
                Context._RB_controlledCharacter.AddForce(Vector3.down * 3000);

            }
        }
    }

    public class State_Walk: State_Base
    {

        public override void OnEnter()
        {
            base.OnEnter();
            //Debug.Log("In walk");
        }

        public override void Update()
        {
            base.Update();
            if (Context._thisID == Enum_MP2_ID.Player0)
                _Walk();
            _SwitchToBoostAssess();
        }

        public override void OnExit()
        {
            base.OnExit();
        }

        private void _SwitchToBoostAssess()
        {
            GetRayHits();
            if (_allHits.Length == 0)
                TransitionTo<State_Boost>();
        }

        private void _Walk ()
        {
            if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.D))
            {

            }
            else if (Input.GetKey(KeyCode.A))
            {
                Context._RB_controlledCharacter.AddForce(Vector3.left * 3000);
                Context._model_controlledCharacter.SetDir(FacingDirection.Left);
            }
            else if (Input.GetKey(KeyCode.D))
            {
                Context._RB_controlledCharacter.AddForce(Vector3.right * 3000);
                Context._model_controlledCharacter.SetDir(FacingDirection.Right);
            }

            if (Input.GetKey(KeyCode.W))
            {
                Context._RB_controlledCharacter.AddForce(Vector3.up * 6000);
            }

        }
    }

    public class State_NoMovementInput : State_Base
    {
        public override void OnEnter()
        {
            base.OnEnter();
        }

        public override void Update()
        {
            base.Update();
        }

        public override void OnExit()
        {
            base.OnExit();
        }
    }
    #endregion
}

public enum Enum_MP2_ID { Player0, Player1 }
public enum Enum_MP2_ControlState { Character, Menu, Station, InterruptableAction}