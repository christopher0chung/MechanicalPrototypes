using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MP1_Data
{
    protected bool _carryable;
    protected bool _highlighted;

    public void SetHighlighted (bool highlighted)
    {
        _highlighted = highlighted;
    }

    public bool GetHighlighted ()
    {
        return _highlighted;
    }
}

public abstract class MP1_PhysicalForm : MonoBehaviour
{
    public MP1_Data data;
}