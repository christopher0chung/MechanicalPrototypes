using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour {

    public Transform tgt;
    Vector3 origin;

	// Use this for initialization
	void Start () {
        origin = transform.localPosition;
	}
	
	// Update is called once per frame
	void Update () {
        transform.localPosition = origin + new Vector3(.3f * Mathf.Sin(Time.time / 4.25f), .1f * Mathf.Cos(Time.time), 0);
        transform.LookAt(tgt);
	}
}
