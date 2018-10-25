using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MP2_CharacterInteractionController : MonoBehaviour, MP2_IConstructable {

    private Enum_MP2_ID _thisID;

    private MP2_ItemManager _itemManager;
    private MP2_EquipmentManager _eqptManager;

    public void CalledAwake(Enum_MP2_ID id)
    {
        _thisID = id;
        _itemManager = MP2_ServiceLocator.instance.ItemManager;
        _eqptManager = MP2_ServiceLocator.instance.EquipmentManager;

        _itemManager.CalledAwake();
        _eqptManager.CalledAwake();

        // Old items and equipment are cleared at Awake.
        // Event at start causes the level's items and equipment to register themselves with the manager.
    }

	public void CalledStart ()
    {
		// Registration of  items and equipment will be handled with an event from the constructor.
        // Since CIC exist for each char, the constructor is used so that items and equipment are initialized just once.
	}
	
	void Update () {
		
	}
}
