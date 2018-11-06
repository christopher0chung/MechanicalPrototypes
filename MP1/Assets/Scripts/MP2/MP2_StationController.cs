using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MP2_StationController : MonoBehaviour, MP2_IConstructable {

    private Enum_MP2_ID _thisID;

    public void CalledAwake(Enum_MP2_ID id)
    {
        _thisID = id;
    }

    public void CalledStart()
    {

    }

}
