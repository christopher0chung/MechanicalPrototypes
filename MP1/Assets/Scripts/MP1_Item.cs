using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MP1_Item : MonoBehaviour
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

    public ItemData itemData;

    public void Start()
    {
        if (startingLooseObject)
        {          
            Init(new ItemData(this.itemType, this.startingCapacity), transform.position);
        }
    }

    public void Init(ItemData itemData, Vector3 where)
    {
        this.itemData = itemData;

        gameObject.name = itemData.itemType.ToString();

        transform.position = where;

        gameObject.AddComponent<Rigidbody>();
        this.itemData.GetSerializedRigidbody().RestoreRigidbody(GetComponent<Rigidbody>());

        GameObject gO = Instantiate(Resources.Load<GameObject>(this.itemData.itemType.ToString() + "_Prefab"), transform.position, transform.rotation, transform);
    }

    public void CleanUp()
    {
        Destroy(gameObject);
    }
}

public class ItemData
{
    public Items itemType;
    public float capacity;
    private bool _carryable;
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

    private void _MakeABody(Vector3 where)
    {
        GameObject gO = new GameObject();
        MP1_Item i = gO.AddComponent<MP1_Item>();
        i.Init(this, where);
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