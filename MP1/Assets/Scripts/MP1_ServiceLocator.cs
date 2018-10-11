using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MP1_ServiceLocator {

    private static MP1_ServiceLocator _instance;
    public static MP1_ServiceLocator instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new MP1_ServiceLocator();
            }
            return _instance;
        }
    }

    #region Manager References

    private SCG_InputBuffer _ib;
    public SCG_InputBuffer InputBuffer
    {
        get
        {
            if (_ib == null)
                _ib = GameObject.Find("Managers").GetComponent<SCG_InputBuffer>();
            return _ib;
        }
    }

    private SCG_EventManager _em;
    public SCG_EventManager EventManager
    {
        get
        {
            if (_em == null)
                return _em = new SCG_EventManager();
            else
                return _em;
        }
    }

    private MP1_ItemsManager _im;
    public MP1_ItemsManager ItemsManager
    {
        get
        {
            if (_im == null)
                return _im = new MP1_ItemsManager();
            else
                return _im;
        }
    }

    #endregion
}
