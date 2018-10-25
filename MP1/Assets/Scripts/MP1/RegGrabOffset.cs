using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegGrabOffset : MonoBehaviour {

	void Start () {
        transform.root.GetComponent<TestPlayerController>().SetGrabOffset(this.transform);
	}

}
