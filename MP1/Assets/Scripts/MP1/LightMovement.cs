using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightMovement : MonoBehaviour {

    public float zAnchor = 1;

    public float zWaveMag = 2.5f;

    public float xWaveMag;

    Vector3 conv;

    private void Start()
    {
        conv = transform.position;
    }

    void Update () {
        conv.z = zAnchor + zWaveMag * Mathf.Sin(Time.time * 2);
        conv.x += xWaveMag * Mathf.Sin (30 * Time.time);
        transform.position = conv;
	}
}
