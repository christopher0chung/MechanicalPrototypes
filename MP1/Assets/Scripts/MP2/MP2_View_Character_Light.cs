using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MP2_View_Character_Light : MonoBehaviour {

	void Start () {
        string name = this.transform.root.gameObject.name;

        int index;
        int.TryParse(name.Substring(name.Length - 1), out index);
        Enum_MP2_ID id = (Enum_MP2_ID)index;

        MP2_ServiceLocator.instance.EventManager.Fire(new E_CharViewHeadlampConstructed(id, this.transform));

        //Debug.Log("Light event fired");
    }
}
