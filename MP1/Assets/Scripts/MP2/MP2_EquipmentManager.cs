using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MP2_EquipmentManager {

    //Managers do not handle loose equipment construction for level design purposes.

    public List<MP2_Equipment> _managedEquipment;

    public MP2_EquipmentManager ()
    {
        _managedEquipment = new List<MP2_Equipment>();
    }

    public void CalledAwake()
    {
        _managedEquipment.Clear();
    }
}
