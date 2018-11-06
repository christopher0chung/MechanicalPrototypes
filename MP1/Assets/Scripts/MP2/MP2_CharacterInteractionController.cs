using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MP2_CharacterInteractionController : MonoBehaviour, MP2_IConstructable {

    private Enum_MP2_ID _thisID;

    private SCG_EventManager _eventManager;

    private MP2_ItemManager _itemManager;
    private MP2_EquipmentManager _eqptManager;

    private GameObject _GO_controlledCharacter;

    private SCG_FSM<MP2_CharacterInteractionController> _fsm;

    // potentially null references
    private Transform headlamp;
    private MP2_View_Item_Held hands;

    public void CalledAwake(Enum_MP2_ID id)
    {
        _thisID = id;
        _itemManager = MP2_ServiceLocator.instance.ItemManager;
        _eqptManager = MP2_ServiceLocator.instance.EquipmentManager;

        _eventManager = MP2_ServiceLocator.instance.EventManager;
        _eventManager.Register<E_CharConstructed>(CharacterConstructed);
        _eventManager.Register<E_ControlStateSwitched>(ControlStateSwitchedHandler);
        _eventManager.Register<E_CharViewHeadlampConstructed>(CharacterHeadlampViewConstructedHandler);
        _eventManager.Register<E_CharViewHandConstructed>(CharacterHandViewConstructedHandler);

        if (id == Enum_MP2_ID.Player0)
        {
            _itemManager.CalledAwake();
            _eqptManager.CalledAwake();
        }

        // Old items and equipment are cleared at Awake.
        // Event at start causes the level's items and equipment to register themselves with the manager.
    }

	public void CalledStart ()
    {
        // Registration of  items and equipment will be handled with an event from the constructor.
        // Since CIC exist for each char, the constructor is used so that items and equipment are initialized just once.

        _fsm = new SCG_FSM<MP2_CharacterInteractionController>(this);
        _fsm.TransitionTo<State_ActiveAndStandingBy>();
	}
	
	void Update () {
        if (_thisID == Enum_MP2_ID.Player0)
            _fsm.Update();
	}

    #region Internal Functions
     
    #endregion

    #region Handlers
    public void CharacterConstructed(SCG_Event e)
    {
        E_CharConstructed cc = e as E_CharConstructed;
        
        if (cc != null)
        {
            if (cc.constructedID == _thisID)
            {
                _GO_controlledCharacter = cc.characterGO;
            }
        }
    }

    public void ControlStateSwitchedHandler(SCG_Event e)
    {
        E_ControlStateSwitched css = e as E_ControlStateSwitched;

        if (css != null)
        {
            if(css.switchingID == _thisID)
            {
                if (css.switchingState == Enum_MP2_ControlState.Character)
                    _fsm.TransitionTo<State_ActiveAndStandingBy>();
                else if (css.switchingState == Enum_MP2_ControlState.Menu)
                    _fsm.TransitionTo<State_OpMenu>();
                else
                    _fsm.TransitionTo<State_Station>();
            }
        }
    }

    public void CharacterHeadlampViewConstructedHandler(SCG_Event e)
    {
        //Debug.Log("headlamp handler called");

        E_CharViewHeadlampConstructed chvc = e as E_CharViewHeadlampConstructed;

        if (chvc != null)
        {
            if (chvc.constructedID == _thisID)
            {
                headlamp = chvc.headlampTransform;
                //Temp until hands transform has a view
                //Debug.Log(headlamp.name + " " + hands.name);
            }
        }
    }

    public void CharacterHandViewConstructedHandler(SCG_Event e)
    {
        E_CharViewHandConstructed cvhc = e as E_CharViewHandConstructed;

        if (cvhc != null)
        {
            if (cvhc.constructedID == _thisID)
            {
                //Temp until hands transform has a view
                hands = cvhc.handView;

                //Debug.Log(headlamp.name + " " + hands.name);
            }
        }
    }
    #endregion

    #region FSM States
    public class State_Base : SCG_FSM<MP2_CharacterInteractionController>.State
    {
        protected RaycastHit _rch;
        protected MP2_View_Item_Loose _itemLookingAt;
        protected LayerMask _interactLM;

        public override void OnEnter()
        {
            Debug.Log("Entering " + this.GetType().ToString());
        }

        public override void Init()
        {
            _interactLM = 1 << 9;
        }

        protected void UpdateHeadlampAngle()
        {
            // Need to reference appropriate model
            if (Input.GetKey(KeyCode.U))
                Context.headlamp.Rotate(0, 0, 30 * Time.deltaTime);
            if (Input.GetKey(KeyCode.I))
                Context.headlamp.Rotate(0, 0, -30 * Time.deltaTime);
        }

        protected void Muscle()
        {
            Ray headForward = new Ray(Context.headlamp.position, Context.headlamp.right);
            Physics.Raycast(headForward, out _rch, 2.0f, _interactLM,  QueryTriggerInteraction.Ignore);
            if (_rch.collider != null)
                _itemLookingAt = _rch.collider.transform.root.gameObject.GetComponent<MP2_View_Item_Loose>();
        }
    }

    public class State_ActiveAndStandingBy : State_Base
    {
        public override void OnEnter()
        {
            base.OnEnter();
        }

        public override void Update()
        {
            base.Update();

            UpdateHeadlampAngle();

            //Grab
            if (MP2_ServiceLocator.instance.InputBuffer.KeyDown(Context.GetType(), KeyCode.K))
            {
                TransitionTo<State_AttemptToHold>();
            }

        }

        public override void OnExit()
        {
            base.OnExit();
        }
    }

    public class State_AttemptToHold : State_Base
    {
        public override void OnEnter()
        {
            base.OnEnter();
            Muscle();
        }

        public override void Update()
        {
            base.Update();

            //Debug.Log(Context._itemManager.AttemptHoldLooseItem(Context._thisID, _itemLookingAt, Context.hands));
            if (_itemLookingAt != null)
            {
                Debug.Log("Didn't miss w raycast");
                // Item Check first
                if (Context._itemManager.AttemptHoldLooseItem(Context._thisID, _itemLookingAt, Context.hands))
                    TransitionTo<State_HoldingItem>();
                //else if (Context._equipmentManager.AttemptHoldLooseItem(Context._thisID, _itemLookingAt, Context.hands))
                //    TransitionTo<State_HoldingEquipment>();
                else 
                    TransitionTo<State_ActiveAndStandingBy>();
            }
            else
            {
                Debug.Log("no item looking at");
                TransitionTo<State_ActiveAndStandingBy>();
            }
        }

        public override void OnExit()
        {
            base.OnExit();
        }
    }

    public class State_HoldingItem : State_Base
    {
        public override void OnEnter()
        {
            base.OnEnter();
        }

        public override void Update()
        {
            base.Update();
            if (MP2_ServiceLocator.instance.InputBuffer.KeyRate(Context.GetType(), KeyCode.K) > .1f)
                return;
            else
                TransitionTo<State_ActiveAndStandingBy>();
        }

        public override void OnExit()
        {
            base.OnExit();
            Context._itemManager.DropHeldItem(Context._thisID, Context.hands);
        }
    }

    public class State_HoldingEquipment : State_Base
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

    public class State_AttemptToOperate : State_Base
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

    public class State_OpMenu : State_Base
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

    public class State_Station : State_Base
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
