using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MP2_Model_CharStats : MonoBehaviour {

    public FacingDirection currentDirection { get; private set; }

    public float AirPercent { get; private set; }

    public float ExhPercent { get; private set; }


    public void ConsumeAir(float ratePerSec)
    {
        AirPercent -= ratePerSec * Time.deltaTime;
        if (AirPercent <= 0)
            AirPercent = 0;
    }

    public void ChargeAir (float ratePerSec)
    {
        AirPercent += ratePerSec * Time.deltaTime;
        if (AirPercent >= 100)
            AirPercent = 100;
    }

    public void SetDir (FacingDirection dir)
    {
        currentDirection = dir;
    }
}

public enum FacingDirection { Left, Right}
