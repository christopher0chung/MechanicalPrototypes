using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MP2_ManagerConstructor : MonoBehaviour {

    private GameObject managersGO;

    private List<MP2_IConstructable> _p0_Constructables;
    private List<MP2_IConstructable> _p1_Constructables;

    void Awake()
    {
        managersGO = GameObject.Find("Managers");

        _p0_Constructables = new List<MP2_IConstructable>();
        _p1_Constructables = new List<MP2_IConstructable>();

        _p0_Constructables.Add(managersGO.AddComponent<MP2_InputRouteProcessor>());
        _p1_Constructables.Add(managersGO.AddComponent<MP2_InputRouteProcessor>());

        //Debug.Log(_p0_Constructables.Count + " " + _p1_Constructables.Count);

        foreach (MP2_IConstructable c in _p0_Constructables)
            c.CalledAwake(Enum_MP2_ID.Player0);

        foreach (MP2_IConstructable c in _p1_Constructables)
            c.CalledAwake(Enum_MP2_ID.Player1);
    }

    void Start () {
        foreach (MP2_IConstructable c in _p0_Constructables)
            c.CalledStart();

        foreach (MP2_IConstructable c in _p1_Constructables)
            c.CalledStart();
    }
}
