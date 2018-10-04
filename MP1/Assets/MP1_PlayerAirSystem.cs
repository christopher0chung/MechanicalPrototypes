using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MP1_PlayerAirSystem : MonoBehaviour {

    // Note: player air system should be made a non-monobehavior
    // Note: link to service locator
    // Note: like to obj update manager

    public RectTransform airMeter;

    public float airPercent{ get; private set; }

    private Vector3 _airBarScale;

    void Start()
    {
        _airBarScale = airMeter.localScale;
        airPercent = 100;
    }
	
	void Update () {
        airPercent = Mathf.Clamp(airPercent, 0, 100);

        _airBarScale.x = airPercent / 100;

        airMeter.localScale = _airBarScale;

        // Debug
        if (Input.GetKeyDown(KeyCode.P))
            airPercent = 100;
	}

    public void dischargeAir(float consumptionPercent)
    {
        airPercent -= consumptionPercent;
    }

    public void chargeAir(float chargePercent)
    {
        airPercent += chargePercent;
    }

}
