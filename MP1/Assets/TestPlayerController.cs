using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPlayerController : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (MP1_ServiceLocator.instance.InputBuffer.KeyRate(this.GetType(), KeyCode.J) > 0)
        {
            transform.position += Vector3.up * MP1_ServiceLocator.instance.InputBuffer.KeyRate(this.GetType(), KeyCode.J) / 100;
        }

        if (MP1_ServiceLocator.instance.InputBuffer.KeyRate(this.GetType(), KeyCode.K) > 0)
        {
            transform.position = Vector3.zero;
        }
    }
}
