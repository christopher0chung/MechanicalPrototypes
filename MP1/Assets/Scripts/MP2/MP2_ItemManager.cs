using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MP2_ItemManager {

    //Managers do not handle loose item construction for level design purposes.

    private List<MP2_Item> _managedItems;

    public MP2_ItemManager()
    {
        _managedItems = new List<MP2_Item>();
    }

    public void CalledAwake()
    {
        _managedItems.Clear();
    }
}
