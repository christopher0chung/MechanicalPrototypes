using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPlayerController : MonoBehaviour {

    #region Private Variables
    private Rigidbody _rigidBody;
    private Transform _model;
    private Transform _grabOffset;
    protected GameObject grabbable;

    private Vector3 _thrustVector;

    private SCG_FSM<TestPlayerController> _fsm;

    private LastFacingDirection _dir;

    private MP1_PlayerAirSystem _air;
    private MP1_PlayerExhaustionSystem _exh;
    #endregion

    public float thrustMagnitude;

    void Start () {
        _Initialize();
	}
	
	void Update () {
        _SetThrustVector();
        _SetModelTempAnim();
        _fsm.Update();

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

        _fsm = new SCG_FSM<TestPlayerController>(this);
        _fsm.TransitionTo<State_NoInteraction>();

        _air = GetComponent<MP1_PlayerAirSystem>();
        _exh = GetComponent<MP1_PlayerExhaustionSystem>();
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

    #endregion

    #region Public Functions

    public void SetGrabOffset(Transform grab)
    {
        _grabOffset = grab;
    }

    #endregion

    #region FSM States

    public class State_Base : SCG_FSM<TestPlayerController>.State
    {
        protected Collider[] cols;
    }

    public class State_NoInteraction : State_Base
    {
        Vector3 grabAreaDimensions = new Vector3(.1f, 1f, .1f);

        public override void OnEnter()
        {
            base.OnEnter();
            Context.grabbable = null;
        }
        public override void Update()
        {
            if (MP1_ServiceLocator.instance.InputBuffer.KeyDown(Context.GetType(), KeyCode.J))
            {
                if (Context._dir == LastFacingDirection.Left)
                {
                    cols = Physics.OverlapBox(Context.transform.position - 
                        Vector3.right * Context._grabOffset.localPosition.z + 
                        Vector3.up, grabAreaDimensions);
                }
                else
                {
                    cols = Physics.OverlapBox(Context.transform.position + 
                        Vector3.right * Context._grabOffset.localPosition.z + 
                        Vector3.up, grabAreaDimensions);
                }
                if (cols.Length > 0)
                {
                    //Debug.Log(cols.Length);
                    for (int i = 0; i < cols.Length; i++)
                    {
                        if (cols[i].gameObject.tag == "Grabbable")
                        {
                            Context.grabbable = cols[i].gameObject;
                            TransitionTo<State_Grab>();
                            //Debug.Log("Going to grab");
                        }
                    }
                }
            }
        }
    }

    public class State_Grab : State_Base
    {
        SCG_RigidBodySerialized rb;

        public override void OnEnter()
        {
            base.OnEnter();

            rb = new SCG_RigidBodySerialized(Context.grabbable.GetComponent<Rigidbody>());
            Context._rigidBody.mass += rb.mass;
            Destroy(Context.grabbable.GetComponent<Rigidbody>());

            Context.grabbable.transform.SetParent(Context._grabOffset);
            Context.grabbable.transform.localPosition = Vector3.zero;
            Context.grabbable.GetComponent<Rigidbody>().detectCollisions = false;
        }

        public override void Update()
        {
            //Debug.Log(MP1_ServiceLocator.instance.InputBuffer.KeyRate(this.GetType(), KeyCode.J));

            Context._exh.Exert(3 * Time.deltaTime);

            if (MP1_ServiceLocator.instance.InputBuffer.TimeSinceInactive(this.GetType(), KeyCode.J) >= Context._exh.ExhaustionThreshold())
            {
                TransitionTo<State_GrabRecovery>();
            }
        }

        public override void OnExit()
        {
            base.OnExit();
            Context.grabbable.transform.SetParent(null);

            rb.RestoreRigidbody(Context.grabbable.AddComponent<Rigidbody>());
            Context._rigidBody.mass -= rb.mass;

            if (Context._dir == LastFacingDirection.Left)
            {
                Context.grabbable.transform.position = 
                    Context.transform.position + 
                    Vector3.up * Context._grabOffset.transform.localPosition.y -
                    Vector3.right * Context._grabOffset.transform.localPosition.z;
            }
            else
            {
                Context.grabbable.transform.position =
                    Context.transform.position +
                    Vector3.up * Context._grabOffset.transform.localPosition.y +
                    Vector3.right * Context._grabOffset.transform.localPosition.z;
            }
        }
    }

    public class State_GrabRecovery : State_Base
    {
        private float recoverTimer;

        public override void OnEnter()
        {
            recoverTimer = 0;
        }

        public override void Update()
        {
            base.Update();
            recoverTimer += Time.deltaTime;
            if (recoverTimer >= .7f)
                TransitionTo<State_NoInteraction>();
        }
    }

    #endregion
}

public enum LastFacingDirection { Left, Right }
