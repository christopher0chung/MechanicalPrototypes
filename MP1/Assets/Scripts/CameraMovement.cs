using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour {

    public Transform tgt;
    Vector3 origin;
    float vertRange = 3;
    float originalYPos;

	// Use this for initialization
	void Start () {
        origin = transform.localPosition;
        originalYPos = origin.y;
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey(KeyCode.Y))
        {
            origin.y += Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.H))
        {
            origin.y -= Time.deltaTime;
        }
        origin.y = Mathf.Clamp(origin.y, originalYPos - vertRange, originalYPos + vertRange);

        transform.localPosition = origin + new Vector3(.3f * Mathf.Sin(Time.time / 4.25f), .1f * Mathf.Cos(Time.time), 0);
        transform.LookAt(tgt);
	}
}
