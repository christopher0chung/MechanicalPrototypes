using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//---------------------------------------
// Event Manager
//---------------------------------------

public class SCG_EventManager
{
    //---------------------
    // Creates singleton for ease of access
    //---------------------

    static private SCG_EventManager _instance;
    static public SCG_EventManager instance
    {
        get
        {
            if (_instance == null)
                return _instance = new SCG_EventManager();
            else
                return _instance;
        }
    }

    //---------------------
    // Storage of Events
    //---------------------

    private Dictionary<Type, SCG_Event.Handler> registeredHandlers = new Dictionary<Type, SCG_Event.Handler>();

    //---------------------
    // Register and Unregister
    //---------------------

    public void Register<T>(SCG_Event.Handler handler) where T : SCG_Event
    {
        Type type = typeof(T);
        if (registeredHandlers.ContainsKey(type))
        {
            registeredHandlers[type] += handler;
        }
        else
        {
            registeredHandlers[type] = handler;
        }
    }

    public void Unregister<T>(SCG_Event.Handler handler) where T : SCG_Event
    {
        Type type = typeof(T);
        SCG_Event.Handler handlers;
        if (registeredHandlers.TryGetValue(type, out handlers))
        {
            handlers -= handler;
            if (handlers == null)
            {
                registeredHandlers.Remove(type);
            }
            else
            {
                registeredHandlers[type] = handlers;
            }
        }
    }

    //---------------------
    // Call event
    //---------------------

    public void Fire(SCG_Event e)
    {
        Type type = e.GetType();
        SCG_Event.Handler handlers;
        if (registeredHandlers.TryGetValue(type, out handlers))
        {
            handlers(e);
        }
    }
}
