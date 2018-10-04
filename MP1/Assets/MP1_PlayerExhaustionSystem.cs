using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MP1_PlayerExhaustionSystem : MonoBehaviour
{

    // Note: player exh system should be made a non-monobehavior
    // Note: link to service locator
    // Note: like to obj update manager

    public RectTransform exhaustionMeter;
    public float exhPercentage { get; private set; }

    [SerializeField] private AnimationCurve recoveryCurve;
    private Vector3 _exhBarScale;
    private SCG_FSM<MP1_PlayerExhaustionSystem> _fsm;
    private float _exertionAmount;

    void Start()
    {
        _exhBarScale = exhaustionMeter.localScale;
        exhPercentage = 0;
        _fsm = new SCG_FSM<MP1_PlayerExhaustionSystem>(this);
        _fsm.TransitionTo<State_Recover>();
    }

    void Update()
    {
        _fsm.Update();

        exhPercentage = Mathf.Clamp(exhPercentage, 0, 100);

        _exhBarScale.x = exhPercentage / 100;

        exhaustionMeter.localScale = _exhBarScale;

        _DebugInput();
    }

    #region Internal Functions

    private void _DebugInput()
    {
        // Debug
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            exhPercentage = 100;
        }
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            exhPercentage = 10;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            exhPercentage = 20;
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            exhPercentage = 30;
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            exhPercentage = 40;
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            exhPercentage = 50;
        }
        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            exhPercentage = 60;
        }
        if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            exhPercentage = 70;
        }
        if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            exhPercentage = 80;
        }
        if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            exhPercentage = 90;
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            exhPercentage = 0;
        }
    }

    #endregion

    #region Public Functions

    public void Exert(float exertionPercentage)
    {
        _exertionAmount = exertionPercentage;
        _fsm.TransitionTo<State_Exert>();
    }

    public float ExhaustionThreshold()
    {
        return (.2f - .1f * (exhPercentage / 100));
    }

    #endregion

    #region States

    public class State_Base : SCG_FSM<MP1_PlayerExhaustionSystem>.State
    {

    }

    public class State_Recover : State_Base
    {
        public override void Update()
        {
            base.Update();
            Context.exhPercentage -= Context.recoveryCurve.Evaluate(Context.exhPercentage / 100) * Time.deltaTime * 24.33f;
        }
    }

    public class State_Exert : State_Base
    {
        private float exertionTimer;
        private float exertionTimeOut = .25f;

        public override void OnEnter()
        {
            base.OnEnter();
            exertionTimer = 0;
        }

        public override void Update()
        {
            base.Update();
            Context.exhPercentage += Context._exertionAmount;

            exertionTimer += Time.deltaTime;
            if (exertionTimer >= exertionTimeOut)
            {
                TransitionTo<State_Recover>();
            }
        }
    }

    #endregion
}
