using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MP2_View_Item_Loose : MP2_View_Item {

    [Header("For use for new loose items:")]
    public bool makeNewLooseItem;
    public Enum_MP2_ItemType newItemType;
    [Range(0, 100)] public float startingPercent;

    public void Awake()
    {
        MP2_ServiceLocator.instance.EventManager.Register<E_ItemManagerConstructed>(MakeItemHandler);
    }

    public void MakeItemHandler(SCG_Event e)
    {
        if (makeNewLooseItem)
            MP2_ServiceLocator.instance.ItemManager.RegisterLooseItem(newItemType, startingPercent, this);
    }

    public override void AttachNewItemToView(MP2_Item item)
    {
        base.AttachNewItemToView(item);

        GameObject.Instantiate(item.gOPrefab, transform.position, transform.rotation, transform);
    }

    public override void UnattachItem()
    {
        base.UnattachItem();

        Destroy(this.gameObject);
    }
}
