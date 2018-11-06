using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MP2_View_Character_Animation : MonoBehaviour {

    private MP2_Model_CharStats myModel;

    private void Awake()
    {
        myModel = transform.root.GetComponent<MP2_Model_CharStats>();
    }

    private void Update()
    {
        if (myModel.currentDirection == FacingDirection.Left)
        {
            transform.rotation = Quaternion.Euler(0, -90, 0);
        }
        else
        {
            transform.rotation = Quaternion.Euler(0, 90, 0);
        }
    }
}
