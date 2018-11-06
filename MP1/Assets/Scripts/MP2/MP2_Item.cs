using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "New Item")]
public class MP2_Item : ScriptableObject{

    public Enum_MP2_ItemStates currentState;
    public Enum_MP2_ItemType type;
    public float percent;
    public GameObject gOPrefab;
}
public enum Enum_MP2_ItemStates { Loose, Held, Stowed }
public enum Enum_MP2_ItemType { O2Tank, ArTank, PatchPlate, Battery, Obstruction }