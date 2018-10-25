using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtBehavior : MonoBehaviour {

    public Transform tgt;

	void Update () {
        transform.LookAt(tgt);
	}
}
