using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MP2_View_Item_Held : MP2_View_Item {

    public GameObject visibleModel;

    public override void AttachNewItemToView(MP2_Item item)
    {
        base.AttachNewItemToView(item);

        visibleModel = GameObject.Instantiate(item.gOPrefab, transform.position, transform.rotation, transform);
        Destroy(visibleModel.GetComponent<Rigidbody>());
        Destroy(visibleModel.transform.GetChild(0).GetComponentInChildren<Collider>());
    }

    public override void UnattachItem()
    {
        base.UnattachItem();
        Destroy(visibleModel);
    }

    public void Start()
    {
        string name = this.transform.root.gameObject.name;

        int index;
        int.TryParse(name.Substring(name.Length - 1), out index);
        Enum_MP2_ID id = (Enum_MP2_ID)index;

        MP2_ServiceLocator.instance.EventManager.Fire(new E_CharViewHandConstructed(id, this));
    }
}
