using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MP2_View_Menu_Element : MonoBehaviour {

    Text t;

    public void Initialize (string label)
    {
        t = transform.GetChild(0).GetComponent<Text>();
        t.text = label;
    }
}
