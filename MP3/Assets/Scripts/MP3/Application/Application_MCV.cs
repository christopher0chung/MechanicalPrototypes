using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MP3
{
    namespace MCV
    {
        namespace Application
        {
            public class Application_MCV : MonoBehaviour {
                public GameObject AppObj { get { return this.gameObject; } }
                public GameObject Model_ParentObj { get { return transform.Find("Model").gameObject; } }
                public GameObject View_ParentObj { get { return transform.Find("View").gameObject; } }
                public GameObject Controller_Parent_Obj { get { return transform.Find("Controller").gameObject; } }
            }

            public class Element_Base : MonoBehaviour
            {
                public MP3.MCV.Application.Application_MCV app { get { return FindObjectOfType<Application_MCV>(); } }
            }
        }

        namespace Model
        {
            public class Model_Base : MCV.Application.Element_Base
            {
                
            }
        }

        namespace View
        {
            public class View_Base : MCV.Application.Element_Base
            {

            }
        }

        namespace Controller
        {
            public class Controller_Base : MCV.Application.Element_Base
            {

            }
        }
    }
}
