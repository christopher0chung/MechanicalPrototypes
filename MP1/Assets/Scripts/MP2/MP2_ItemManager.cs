using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MP2_ItemManager {

    //Managers do not handle loose item construction for level design purposes.
    #region Documentation
    //------------------------------------------------------------------------------------
    // Basic Item Management Concepts
    //
    // Stowed items will not have associated an associated GameObject
    // Loose items have associated GameObjects
    // Held items retain their GameObject from when they were loose
    //
    // A stowed item can become loose
    // A loose item can become held
    // A held item can be stowed
    // A held item can be dropped loose
    //
    //                   ---> Held  ----
    //               (1)|       |(3)    |(2)       
    //                  |       |       V
    // Highlight <--> Loose <---+--- Stowed ---- Use
    //                  |(5)     (4)         (6)
    //                  V
    //                 Read
    //
    // 1 - AttemptHoldLooseItem
    // 2 - ItemManagerStowCheck, (pending Controller Check) StowHeldItem
    // 3 - DropHeldItem
    // 4 - UnstowItem
    // 5 - AttemptToReadLooseItem
    // 6 - AttemptToUseStowedItem
    //------------------------------------------------------------------------------------
    #endregion

    private List<MP2_Item> _managedItems;

    private MP2_Item _p0_HeldItem;
    private MP2_Item _p1_HeldItem;

    private MP2_Item _ArTankRef;
    private MP2_Item _O2TankRef;
    private MP2_Item _PatchPlateRef;
    private MP2_Item _BatteryRef;
    private MP2_Item _ObstructionRef;

    #region Externally Called Setup Methods
    public MP2_ItemManager()
    {
        _managedItems = new List<MP2_Item>();

        _ArTankRef = Resources.Load<MP2_Item>("Items/" + "ArTank");
        _O2TankRef = Resources.Load<MP2_Item>("Items/" + "O2Tank");
        _PatchPlateRef = Resources.Load<MP2_Item>("Items/" + "PatchPlate");
        _BatteryRef = Resources.Load<MP2_Item>("Items/" + "Battery");
        _ObstructionRef = Resources.Load<MP2_Item>("Items/" + "Obstruction");
    }

    public void CalledAwake()
    {
        _managedItems.Clear();

        MP2_ServiceLocator.instance.EventManager.Fire(new E_ItemManagerConstructed());
    }
    #endregion

    #region Registration
    public void RegisterStowedItem (MP2_Item item)
    {
        if (_managedItems.Contains(item))
            return;
        else
        {
            _managedItems.Add(item);
            item.currentState = Enum_MP2_ItemStates.Stowed;
        }
    }

    public void RegisterLooseItem(Enum_MP2_ItemType type, float startingPercent, MP2_View_Item itemView)
    {
        MP2_Item newItem;
        if (type == Enum_MP2_ItemType.ArTank)
            newItem = ScriptableObject.Instantiate(_ArTankRef);
        else if (type == Enum_MP2_ItemType.O2Tank)
            newItem = ScriptableObject.Instantiate(_O2TankRef);
        else if (type == Enum_MP2_ItemType.PatchPlate)
            newItem = ScriptableObject.Instantiate(_PatchPlateRef);
        else if (type == Enum_MP2_ItemType.Battery)
            newItem = ScriptableObject.Instantiate(_BatteryRef);
        else
            newItem = ScriptableObject.Instantiate(_ObstructionRef);

        newItem.currentState = Enum_MP2_ItemStates.Loose;
        newItem.percent = startingPercent;

        _managedItems.Add(newItem);
        itemView.AttachNewItemToView(newItem);
     }
    #endregion

    #region Public Management Functions
    #region (1) Loose To Held
    public bool AttemptHoldLooseItem(Enum_MP2_ID requestingHolder, MP2_View_Item_Loose looseView, MP2_View_Item_Held heldView)
    {
        if (!_CanIHoldLooseItem(requestingHolder, looseView))
            return false;

        else
        {
            Debug.Assert(requestingHolder != null, "holder is null");
            Debug.Assert(looseView != null, "loose view is null");
            Debug.Assert(heldView != null, "held view is null");

            _LooseToHeld(requestingHolder, looseView, heldView);
            return true;
        }
    }
    #endregion

    #region (2) Held To Stowed
    public bool ItemManagerStowCheck(Enum_MP2_ID stower)
    {
        return _IsStowRequesterHoldingAnItem(stower);
    }

    public MP2_Item GetItemToBeStowed(Enum_MP2_ID stower)
    {
        // Used by controller to get item reference for equipment
        MP2_Item ItemToBeStowed =  _PrepRefOfObjectToBeStowed(stower);

        return ItemToBeStowed;
    }

    public void StowHeldItem(Enum_MP2_ID stower, MP2_Equipment stowingEquipment)
    {
        _SetHeldItemToStowed(stower);
    }
    #endregion

    #region (3) Held To Loose
    public void DropHeldItem(Enum_MP2_ID dropper, MP2_View_Item_Held heldView)
    {
        _HeldToLoose(dropper, heldView);
    }
    #endregion

    #region (4) Stowed To Loose
    public void UnstowItem(MP2_Item stowedItemToUnstow, Vector3 unstowLocation, Vector3 launchVector, float launchImpulse)
    {
        _MakeBodyToUnstow(stowedItemToUnstow, unstowLocation, launchVector, launchImpulse);
    }
    #endregion

    #region (5) Read Loose
    public float AttemptToReadLooseItem(GameObject looseItem)
    {
        return _ReadItemValue(looseItem);
    }
    #endregion

    #region (6) Use Stowed
    public bool AttemptToUseStowedItem(MP2_Item item, float ratePerSec)
    {
        if (!_CanIChargeOrDischarge(item, ratePerSec))
            return false;
        else
        {
            _ChargeDischarge(item, ratePerSec);
            return true;
        }
    }
    #endregion
    #endregion

    #region Private Functions
    private bool _CanIHoldLooseItem (Enum_MP2_ID whoWantsToHold, MP2_View_Item_Loose looseView)
    {
        if (looseView == null)
        {
            Debug.Log("Loose item is null");
            return false;
        }

        if (whoWantsToHold == Enum_MP2_ID.Player0)
        {
            MP2_Item item = looseView.GetItem();

            // This player must be empty handed
            if (_p0_HeldItem != null)
                return false;
            // The other player must not be holding this item if they're holding something
            if (_p1_HeldItem != null)
            {
                if (item == _p1_HeldItem)
                    return false;
            }
            // If player is not empty handed and this item isn't being held by anyone
            return true;
        }
        else
        {
            MP2_Item item = looseView.GetItem();

            if (_p1_HeldItem != null)
                return false;
            if (_p0_HeldItem != null)
            {
                if (item == _p0_HeldItem)
                    return false;
            }

            return true;
        }
    }

    private void _LooseToHeld (Enum_MP2_ID whoWillHold, MP2_View_Item_Loose looseView, MP2_View_Item_Held heldView)
    {
        Debug.Log("newParent.name is " + heldView.name);
        Debug.Log("looseItem.name is " + looseView.name);
        Debug.Log("holder is " + whoWillHold.ToString());

        MP2_Item i = looseView.GetItem();
        heldView.AttachNewItemToView(i);
        looseView.UnattachItem();

        if (whoWillHold == Enum_MP2_ID.Player0)
            _p0_HeldItem = i;
        else
            _p1_HeldItem = i;
    }

    private void _HeldToLoose (Enum_MP2_ID whoIsDropping, MP2_View_Item_Held heldView)
    {
        if (whoIsDropping == Enum_MP2_ID.Player0)
        {
            Debug.Assert(heldView.GetItem() == _p0_HeldItem, "Dropping unknown item");

            Vector3 realignedPos = heldView.gameObject.transform.position;
            realignedPos.z = 0;

            GameObject viewObject = GameObject.Instantiate(Resources.Load<GameObject>("Views/LooseItem"), realignedPos, heldView.transform.rotation, null);

            viewObject.GetComponent<MP2_View_Item_Loose>().AttachNewItemToView(heldView.GetItem());
            heldView.UnattachItem();

            _p0_HeldItem = null;
        }
        else
        {
            Debug.Assert(heldView.GetItem() == _p1_HeldItem, "Dropping unknown item");

            GameObject viewObject = GameObject.Instantiate(Resources.Load<GameObject>("Views/LooseItem"), heldView.gameObject.transform.position, heldView.transform.rotation, null);

            viewObject.GetComponent<MP2_View_Item_Loose>().AttachNewItemToView(heldView.GetItem());
            heldView.UnattachItem();

            _p1_HeldItem = null;
        }
    }

    private bool _IsStowRequesterHoldingAnItem(Enum_MP2_ID whoIsTryingToStow)
    {
        //if (whoIsTryingToStow == Enum_MP2_ID.Player0)
        //{
        //    if (_p0_HeldItem != null)
        //        return true;
        //    else
        //        return false;
        //}
        //else
        //{
        //    if (_p1_HeldItem != null)
        //        return true;
        //    else
        //        return false;
        //}

        return false;
    }

    private MP2_Item _PrepRefOfObjectToBeStowed(Enum_MP2_ID stower)
    {
        if (stower == Enum_MP2_ID.Player0)
           return _p0_HeldItem;
        else
            return _p1_HeldItem;
    }

    private void _SetHeldItemToStowed(Enum_MP2_ID stower)
    {
        //if (stower == Enum_MP2_ID.Player0)
        //{
        //    GameObject gameObjectToStow;

        //    _dict_itemToView.TryGetValue(_p0_HeldItem, out gameObjectToStow);

        //    _dict_itemToView.Remove(_p0_HeldItem);
        //    _dict_viewToItem.Remove(gameObjectToStow);

        //    GameObject.Destroy(gameObjectToStow);

        //    _p0_HeldItem.currentState = Enum_MP2_ItemStates.Stowed;

        //    _p0_HeldItem = null;
        //    _p0_HeldRB = null;
        //}
        //else
        //{
        //    GameObject gameObjectToStow;

        //    _dict_itemToView.TryGetValue(_p1_HeldItem, out gameObjectToStow);

        //    _dict_itemToView.Remove(_p0_HeldItem);
        //    _dict_viewToItem.Remove(gameObjectToStow);

        //    GameObject.Destroy(gameObjectToStow);

        //    _p1_HeldItem.currentState = Enum_MP2_ItemStates.Stowed;

        //    _p1_HeldItem = null;
        //    _p1_HeldRB = null;
        //}
    }

    private void _MakeBodyToUnstow(MP2_Item item, Vector3 location, Vector3 direction, float magnitude)
    {
        //GameObject itemBody = GameObject.Instantiate(Resources.Load<GameObject>(item.type.ToString() + "_Prefab"), location, Quaternion.Euler(0, 0, -15), null);
        
        //// Tag for item id
        //// Layer for discovery
        //itemBody.tag = "Item";
        //itemBody.layer = 9;

        //_RecordNewLooseBody(item, itemBody);
        //itemBody.GetComponent<Rigidbody>().AddForce(Vector3.Normalize(direction) * magnitude, ForceMode.Impulse);
    }

    private void _RecordNewLooseBody(MP2_Item item, MP2_View_Item_Loose body)
    {
        //_dict_itemToView.Add(item, body);
        //_dict_viewToItem.Add(body, item);
    }

    private float _ReadItemValue(GameObject body)
    {
        //MP2_Item item;
        //_dict_viewToItem.TryGetValue(body, out item);
        //return item.percent;

        return 0;
    }

    private bool _CanIChargeOrDischarge (MP2_Item item, float rate)
    {
        if (rate > 0)
        {
            if (item.percent >= 100)
                return false;
            else
                return true;
        }
        else if (rate < 0)
        {
            if (item.percent <= 0)
                return false;
            else
                return true;
        }
        else
            return true;
    }

    private void _ChargeDischarge(MP2_Item item, float rate)
    {
        if (rate == 0)
            return;

        item.percent += rate * Time.deltaTime;

        if (item.percent >= 100)
            item.percent = 100;

        else if (item.percent <= 0)
            item.percent = 0;

    }
    #endregion
}
