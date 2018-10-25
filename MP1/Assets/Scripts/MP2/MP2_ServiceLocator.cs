using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MP2_ServiceLocator {

    private static MP2_ServiceLocator _instance;
    public static MP2_ServiceLocator instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new MP2_ServiceLocator();
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

    private MP2_ItemManager _im;
    public MP2_ItemManager ItemManager
    {
        get
        {
            if (_im == null)
                return _im = new MP2_ItemManager();
            else
                return _im;
        }
    }

    private MP2_EquipmentManager _eqm;
    public MP2_EquipmentManager EquipmentManager
    {
        get
        {
            if (_eqm == null)
                return _eqm = new MP2_EquipmentManager();
            else
                return _eqm;
        }
    }
    #endregion
}
