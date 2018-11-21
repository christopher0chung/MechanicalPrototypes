using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MP3
{
    public class ServiceLocator
    {
        private static ServiceLocator _instance;
        public static ServiceLocator instance
        {
            get
            {
                if (_instance == null)
                    _instance = new ServiceLocator();
                return _instance;
            }
        }

        #region Managed References

        private MCV.Application.Application_MCV _ActiveApplication;
        public MCV.Application.Application_MCV ActiveApplication
        {
            get
            {
                if (_ActiveApplication == null)
                    _ActiveApplication = GameObject.FindObjectOfType<MCV.Application.Application_MCV>();
                return _ActiveApplication;
            }
        }

        private SCG_EventManager _EventManager;
        public SCG_EventManager EventManager
        {
            get
            {
                if (_EventManager == null)
                    return _EventManager = new SCG_EventManager();
                else
                    return _EventManager;
            }
        }

        private SCG_InputBuffer _InputBuffer;
        public SCG_InputBuffer InputBuffer
        {
            get
            {
                if (_InputBuffer == null)
                {
                    //Is there already one?
                    try
                    {
                        _InputBuffer = ActiveApplication.gameObject.AddComponent<SCG_InputBuffer>();
                    }
                    catch
                    {
                        Debug.Log("Missing Input Buffer in Application");
                    }
                }
                return _InputBuffer;
            }
        }

        #endregion
    }
}
