using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestUIScript : MonoBehaviour {

    Camera cam;
    RectTransform myRT;
    public Transform character;

	void Start () {
        cam = Camera.main;
        myRT = (RectTransform)this.transform;
	}
	
	void Update () {
        myRT.position = (cam.WorldToScreenPoint(character.position));
	}
}
