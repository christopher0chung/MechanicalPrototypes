using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MP1_Data
{
    protected bool _carryable;
}

public abstract class MP1_PhysicalForm : MonoBehaviour
{
    public MP1_Data data;
}