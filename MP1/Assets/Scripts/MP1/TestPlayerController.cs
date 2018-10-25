using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TestPlayerController : MonoBehaviour {

    #region Private Variables
    private Rigidbody _rigidBody;
    private Transform _model;
    private Transform _grabOffset;
    private ShowGrabbedItems _showGrabbedItems;
    //protected GameObject grabbable;

    private Vector3 _thrustVector;

    private SCG_FSM<TestPlayerController> _fsm_Actions;
    private SCG_FSM<TestPlayerController> _fsm_Movement;

    private LastFacingDirection _dir;
    private MovementStates __mov;
    private MovementStates _mov
    {
        get { return __mov; }
        set
        {
            if (value != __mov)
            {
                __mov = value;
                if (__mov == MovementStates.Move)
                {
                    _fsm_Movement.TransitionTo<M_State_Boost>();
                    _fsm_Actions.TransitionTo<A_State_NoInteraction>();
                }
                else if (__mov == MovementStates.Static)
                {
                    _fsm_Movement.TransitionTo<M_State_Static>();
                    _fsm_Actions.TransitionTo<A_State_InMenu>();
                }
            }
        }
    }

    private MP1_PlayerAirSystem _air;
    private MP1_PlayerExhaustionSystem _exh;

    private MP1_Data _h;
    private MP1_Data _heldItem
    {
        get { return _h; }
        set
        {
            Debug.Log("Attempt made to set _heldItem");
            if (value != _h)
            {
                _h = value;
                if (_h == null)
                    _showGrabbedItems.ShowHeld(null);
                else
                {
                    if (_h.GetType() == typeof(MP1_ItemData))
                    {
                        MP1_ItemData i = (MP1_ItemData)_h;
                        _showGrabbedItems.ShowHeld(i.itemType.ToString());
                    }
                    else if (_h.GetType() == typeof(MP1_EquipmentData))
                    {
                        MP1_EquipmentData e = (MP1_EquipmentData)_h;
                        _showGrabbedItems.ShowHeld(e.equipmentType.ToString());
                    }
                }
            }
        }
    }

    public Transform headLamp;
    #endregion

    public float thrustMagnitude;
    public PlayerID ID{ get; private set;}

    void Start () {
        _Initialize();
	}
	
	void Update () {
        _fsm_Movement.Update();
        _fsm_Actions.Update();
    }

    void FixedUpdate()
    {
        _rigidBody.AddForce(_thrustVector * thrustMagnitude, ForceMode.Force);
    }

    #region Internal Functions

    private void _Initialize()
    {
        _rigidBody = GetComponent<Rigidbody>();
        _model = transform.GetChild(0);

        _fsm_Actions = new SCG_FSM<TestPlayerController>(this);
        _fsm_Actions.TransitionTo<A_State_NoInteraction>();

        _fsm_Movement = new SCG_FSM<TestPlayerController>(this);
        _fsm_Movement.TransitionTo<M_State_Boost>();

        _air = GetComponent<MP1_PlayerAirSystem>();
        _exh = GetComponent<MP1_PlayerExhaustionSystem>();

        MP1_ServiceLocator.instance.ObjectInteractionsManager.RegisterPlayerControllers(this);
    }

    private void _SetThrustVector()
    {
        _thrustVector = Vector3.zero;
        if (_air.airPercent > 0.1f)
        {
            if (Input.GetKey(KeyCode.W))
            {
                _thrustVector += Vector3.up;
            }
            if (Input.GetKey(KeyCode.S))
            {
                _thrustVector -= Vector3.up;
            }
            if (Input.GetKey(KeyCode.A))
            {
                _thrustVector -= Vector3.right;
            }
            if (Input.GetKey(KeyCode.D))
            {
                _thrustVector += Vector3.right;
            }

            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
            {
                _air.dischargeAir(.5f * Time.deltaTime);
            }
        }
    }

    private void _SetModelTempAnim()
    {
        if (Input.GetKey(KeyCode.A))
        {
            _model.rotation = Quaternion.Euler(0, -90, 0);
            _dir = LastFacingDirection.Left;
        }
        if (Input.GetKey(KeyCode.D))
        {
            _model.rotation = Quaternion.Euler(0, 90, 0);
            _dir = LastFacingDirection.Right;
        }
        if (!Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D))
        {
            _model.rotation = Quaternion.Euler(0, -180, 0);
        }
    }

    private void _SetHeadLampAngle()
    {
        if (Input.GetKey(KeyCode.U))
            headLamp.Rotate(Vector3.right, -90 * Time.deltaTime);
        if (Input.GetKey(KeyCode.I))
            headLamp.Rotate(Vector3.right, 90 * Time.deltaTime);

        MP1_ServiceLocator.instance.ObjectInteractionsManager.PlayerInteractSelect(ID, headLamp.position, headLamp.forward);
    }

    private void _AdjustButDontSetHeadLampAngle()
    {
        if (Input.GetKey(KeyCode.U))
            headLamp.Rotate(Vector3.right, -90 * Time.deltaTime);
        if (Input.GetKey(KeyCode.I))
            headLamp.Rotate(Vector3.right, 90 * Time.deltaTime);

        MP1_ServiceLocator.instance.ObjectInteractionsManager.PlayerInteractDisable(ID);
    }

    #endregion

    #region Public Functions
    public void GrabCallback(MP1_Data d)
    {
        _heldItem = d;
    }

    public void ReleaseCallback()
    {
        _heldItem = null;
    }

    public void StageCallback()
    {
        _heldItem = null;
    }

    public void SetGrabOffset(Transform grab)
    {
        _grabOffset = grab;
    }

    public Transform GetGrabOffset()
    {
        return _grabOffset;
    }

    public void SetShowGrabbedItems(ShowGrabbedItems s)
    {
        _showGrabbedItems = s;
    }

    public void SetMove (MovementStates MState)
    {
        if (MState == MovementStates.Move)
            _mov = MovementStates.Move;
        else
            _mov = MovementStates.Static;
    }

    #endregion

    #region FSM States

    public class A_State_Base : SCG_FSM<TestPlayerController>.State
    {
        protected Collider[] cols;
    }

    public class A_State_NoInteraction : A_State_Base
    {
        Vector3 grabAreaDimensions = new Vector3(.1f, 2f, .1f);

        public override void OnEnter()
        {
            base.OnEnter();
            //Context.grabbable = null;
        }
        public override void Update()
        {
            Context._SetHeadLampAngle();

            if (MP1_ServiceLocator.instance.InputBuffer.KeyDown(Context.GetType(), KeyCode.J))
            {
                if (MP1_ServiceLocator.instance.ObjectInteractionsManager.RequestToMuscle(Context.ID))
                {
                    MP1_ServiceLocator.instance.ObjectInteractionsManager.Muscle(Context.ID);
                    TransitionTo<A_State_Grab>();
                }
            }
        }
    }

    public class A_State_Grab : A_State_Base
    {
        SCG_RigidBodySerialized rb;
        private float _mass;

        public override void OnEnter()
        {
            base.OnEnter();

            if (Context._heldItem.GetType() == typeof(MP1_ItemData))
            {
                MP1_ItemData i = (MP1_ItemData)Context._heldItem;
                _mass = i.GetSerializedRigidbody().mass;
            }
            else if (Context._heldItem.GetType() == typeof(MP1_EquipmentData))
            {
                MP1_EquipmentData e = (MP1_EquipmentData)Context._heldItem;
                _mass = e.GetSerializedRigidbody().mass;
            }

            Context._rigidBody.mass += _mass;
        }

        public override void Update()
        {
            Context._AdjustButDontSetHeadLampAngle();

            if (Context._fsm_Movement.CurrentState.ToString() == typeof(M_State_Boost).ToString() ||
                Context._fsm_Movement.CurrentState.ToString() == typeof(M_State_Walk).ToString())
            {
                Context._exh.Exert(3 * Time.deltaTime);

                if (MP1_ServiceLocator.instance.InputBuffer.TimeSinceInactive(this.GetType(), KeyCode.J) >= Context._exh.ExhaustionThreshold())
                {
                    TransitionTo<A_State_GrabRecovery>();
                }
            }
        }

        public override void OnExit()
        {
            if (Context._heldItem.GetType() == typeof(MP1_ItemData))
            {
                MP1_ItemData i = (MP1_ItemData)Context._heldItem;
                _mass = i.GetSerializedRigidbody().mass;
            }
            else if (Context._heldItem.GetType() == typeof(MP1_EquipmentData))
            {
                MP1_EquipmentData e = (MP1_EquipmentData)Context._heldItem;
                _mass = e.GetSerializedRigidbody().mass;
            }

            Context._rigidBody.mass -= _mass;
            MP1_ServiceLocator.instance.ObjectInteractionsManager.RequestToReleaseItem(Context._heldItem, Context, Context._dir);
        }
    }

    public class A_State_GrabRecovery : A_State_Base
    {
        private float recoverTimer;

        public override void OnEnter()
        {
            recoverTimer = 0;
        }

        public override void Update()
        {
            base.Update();

            Context._AdjustButDontSetHeadLampAngle();

            recoverTimer += Time.deltaTime;
            if (recoverTimer >= .7f)
                TransitionTo<A_State_NoInteraction>();
        }
    }

    public class A_State_StaticActionOverTime : A_State_Base
    {

    }

    public class A_State_InMenu : A_State_Base
    {

    }

    public class M_State_Base : SCG_FSM<TestPlayerController>.State
    {

    }

    public class M_State_Boost : M_State_Base
    {
        public override void Update()
        {
            base.Update();
            Context._SetThrustVector();
            Context._SetModelTempAnim();
        }
    }

    public class M_State_Stationary : M_State_Base
    {
        public override void Update()
        {
            base.Update();
            Context._SetModelTempAnim();
        }
    }

    public class M_State_Static : M_State_Base
    {
        public override void Update()
        {
            base.Update();
        }
    }

    public class M_State_Walk : M_State_Base
    {

    }

    #endregion
}

public enum LastFacingDirection { Left, Right }
public enum MovementStates { Move, Static }
