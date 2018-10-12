using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MP1_ItemBody : MP1_PhysicalForm
{
    // The class handles the physical aspects of the items

    // Items are objects in the game that are acted upon and consumed
    // Their types determine certain properties
    // Carryable items are carryable by the player
    // Type determines rigidbody properties
    // Items may have a consumable and replenishable resource associated with them

    public bool startingLooseObject;
    public Items startingItemType;
    public float startingCapacity;

    public void Start()
    {
        if (startingLooseObject)
        {
            MP1_ItemData i = new MP1_ItemData(this.startingItemType, this.startingCapacity);
            i.LooseInit(transform.position, this);
        }
    }

    public void Init(MP1_ItemData itemData, Vector3 where)
    {
        data = (MP1_ItemData)itemData;
        MP1_ItemData i = data as MP1_ItemData;

        gameObject.name = itemData.itemType.ToString();

        transform.position = where;

        gameObject.layer = 9;
        gameObject.AddComponent<Rigidbody>();
        i.GetSerializedRigidbody().RestoreRigidbody(GetComponent<Rigidbody>());

        GameObject gO = Instantiate(Resources.Load<GameObject>(i.itemType.ToString() + "_Prefab"), transform.position, Quaternion.identity, transform);
        transform.Rotate(Vector3.forward, Random.Range(-180, 180));
    }

    public void CleanUp()
    {
        Destroy(gameObject);
    }
}

public class MP1_ItemData : MP1_Data
{
    public Items itemType { get; private set; }
    public float capacity;
    private ItemStates _state;
    private SCG_RigidBodySerialized _rigidBody;
    private MP1_ItemBody _physicalForm;

    public MP1_ItemData (Items itemType, float capacity)
    {
        this.itemType = itemType;

        this.capacity = Mathf.Clamp(capacity, 0, 100);

        if (itemType == Items.Obstruction)
            _carryable = false;
        else
            _carryable = true;

        if (itemType == Items.ArTank)
        {
            _rigidBody = new SCG_RigidBodySerialized(50, 2, 2, true, false, RigidbodyInterpolation.Interpolate, CollisionDetectionMode.ContinuousDynamic, RigidbodyConstraints.FreezePositionZ);
        }
        else if (itemType == Items.Battery)
        {
            _rigidBody = new SCG_RigidBodySerialized(50, 2, 2, true, false, RigidbodyInterpolation.Interpolate, CollisionDetectionMode.ContinuousDynamic, RigidbodyConstraints.FreezePositionZ);
        }
        else if (itemType == Items.O2Tank)
        {
            _rigidBody = new SCG_RigidBodySerialized(50, 2, 2, true, false, RigidbodyInterpolation.Interpolate, CollisionDetectionMode.ContinuousDynamic, RigidbodyConstraints.FreezePositionZ);
        }
        else if (itemType == Items.PatchPlate)
        {
            _rigidBody = new SCG_RigidBodySerialized(50, 2, 2, true, false, RigidbodyInterpolation.Interpolate, CollisionDetectionMode.ContinuousDynamic, RigidbodyConstraints.FreezePositionZ);
        }

        if (MP1_ServiceLocator.instance.ObjectInteractionsManager.RequestToRegister(this))
            MP1_ServiceLocator.instance.ObjectInteractionsManager.Register(this);
        else
            Debug.Log("ERROR: Attempting to register registered data");
    }

    #region Info Methods

    public bool IfCarryable()
    {
        return _carryable;
    }

    public SCG_RigidBodySerialized GetSerializedRigidbody()
    {
        return _rigidBody;
    }

    public ItemStates GetState()
    {
        return _state;
    }
   
    #endregion

    #region Internal Functions

    private void _MakeALooseBody(Vector3 where, MP1_ItemBody item)
    {
        _physicalForm = item;
        _physicalForm.Init(this, where);
    }

    private void _MakeABody(Vector3 where)
    {
        GameObject gO = new GameObject();
        _physicalForm = gO.AddComponent<MP1_ItemBody>();
        _physicalForm.Init(this, where);
    }

    private void _Launch(Vector3 direction, float launchImpulse)
    {
        _physicalForm.GetComponent<Rigidbody>().AddForce(direction * launchImpulse);
    }

    private void _RemoveBody()
    {
        Debug.Assert(_physicalForm != null, "Attempting to destroy a non-existant body");
        _physicalForm.CleanUp();
    }

    #endregion
    
    #region Body Functions

    // Called by MP1_Item if and only if a loose item at start of level.
    public void LooseInit(Vector3 where, MP1_ItemBody looseObject)
    {
        _state = ItemStates.Free;
        _MakeALooseBody(where, looseObject);
    }

    #endregion

    #region Item Manager Functions

    // Called by MP1_ItemManager
    public void MakeHeld()
    {
        _state = ItemStates.Held;
        _RemoveBody();
    }

    public void MakeStaged()
    {
        _state = ItemStates.Staged;
        _RemoveBody();
    }

    public void MakeFree(Vector3 where)
    {
        _state = ItemStates.Free;
        _MakeABody(where);
    }

    public void MakeLaunched(Vector3 where, Vector3 direction, float launchImpulse)
    {
        _state = ItemStates.Free;
        _MakeABody(where);
        _Launch(direction, launchImpulse);
    }

    #endregion
}

public enum ItemStates { Free, Held, Staged }