using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MP2_View_Item : MonoBehaviour {

    [Header("Standard operation: (Do Not Modify In Inspector)")]
    [SerializeField] protected MP2_Item item;

    public virtual void AttachNewItemToView(MP2_Item item)
    {
        this.item = item;
    }

    public virtual void UnattachItem()
    {
        item = null;
    }

    public virtual MP2_Item GetItem()
    {
        return item;
    }
}
