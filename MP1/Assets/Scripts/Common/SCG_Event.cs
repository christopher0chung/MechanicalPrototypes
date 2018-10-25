using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SCG_Event
{
    public delegate void Handler(SCG_Event e);
}

public class MP1_InitLevel_PlayLevel
{
    public MP1_InitLevel_PlayLevel() { }
}