using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MP1_Item : MP1_PhysicalForm
{
    // The class handles the physical aspects of the itemss

    // Items are objects in the game that are acted upon and consumed
    // Their types determine certain properties
    // Carryable items are carryable by the player
    // Type determines rigidbody properties
    // Items may have a consumable and replenishable resource associated with them

    public bool startingLooseObject;
    public Items itemType;
    public float startingCapacity;

    //public ItemData itemData;

    public void Start()
    {
        if (startingLooseObject)
        {
            ItemData i = new ItemData(this.itemType, this.startingCapacity);
            i.LooseInit(transform.position, this);
        }
    }

    public void Init(ItemData itemData, Vector3 where)
    {
        data = (ItemData)itemData;
        ItemData i = data as ItemData;

        gameObject.name = itemData.itemType.ToString();

        transform.position = where;

        gameObject.layer = 9;
        gameObject.AddComponent<Rigidbody>();
        i.GetSerializedRigidbody().RestoreRigidbody(GetComponent<Rigidbody>());

        GameObject gO = Instantiate(Resources.Load<GameObject>(i.itemType.ToString() + "_Prefab"), transform.position, Quaternion.Euler(-90, 0, 0), transform);
        transform.Rotate(Vector3.forward, Random.Range(-180, 180));
    }

    public void CleanUp()
    {
        Destroy(gameObject);
    }
}

public class ItemData : MP1_Data
{
    public Items itemType;
    public float capacity;
    private ItemStates _state;
    private SCG_RigidBodySerialized _rigidBody;
    private MP1_Item _physicalForm;

    public ItemData (Items itemType, float capacity)
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

        MP1_ServiceLocator.instance.ItemsManager.Register(this);
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

    private void _MakeALooseBody(Vector3 where, MP1_Item item)
    {
        _physicalForm = item;
        _physicalForm.Init(this, where);
    }

    private void _MakeABody(Vector3 where)
    {
        GameObject gO = new GameObject();
        _physicalForm = gO.AddComponent<MP1_Item>();
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
    // Called by MP1_Item if and only if a loose item at start of level.
    public void LooseInit(Vector3 where, MP1_Item looseObject)
    {
        _state = ItemStates.Free;
        _MakeALooseBody(where, looseObject);
    }

    // Called by MP1_ItemManager
    public void Hold()
    {
        _state = ItemStates.Held;
        _RemoveBody();
    }

    public void Stage()
    {
        _state = ItemStates.Staged;
        _RemoveBody();
    }

    public void LetGo(Vector3 where)
    {
        _state = ItemStates.Free;
        _MakeABody(where);
    }

    public void Unstage(Vector3 where, Vector3 direction, float launchImpulse)
    {
        _state = ItemStates.Free;
        _MakeABody(where);
        _Launch(direction, launchImpulse);
    }
}

public enum ItemStates { Free, Held, Staged }