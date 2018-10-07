using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MP1_Item : MP1_Interactables
{
    protected SCG_RigidBodySerialized _serializedRigidBody;

    public Items itemType;

    public ItemData data;

    private void Start()
    {
        // For items that exist at the beginning of the level

        Debug.Assert(GetComponent<Rigidbody>(), gameObject.name + " does not have a RigidBody");
        _serializedRigidBody = new SCG_RigidBodySerialized(GetComponent<Rigidbody>());
    }

    private void _MakeRigidBody()
    {
        // Does not work for Obstructions
        // Obstructions should not be created at runtime

        if (data.itemType == Items.ArTank)
        {
            _serializedRigidBody = new SCG_RigidBodySerialized(50, 2, 2, true, false, RigidbodyInterpolation.Interpolate, CollisionDetectionMode.ContinuousDynamic, RigidbodyConstraints.FreezePositionZ);
        }
        else if (data.itemType == Items.Battery)
        {
            _serializedRigidBody = new SCG_RigidBodySerialized(50, 2, 2, true, false, RigidbodyInterpolation.Interpolate, CollisionDetectionMode.ContinuousDynamic, RigidbodyConstraints.FreezePositionZ);
        }
        else if (data.itemType == Items.O2Tank)
        {
            _serializedRigidBody = new SCG_RigidBodySerialized(50, 2, 2, true, false, RigidbodyInterpolation.Interpolate, CollisionDetectionMode.ContinuousDynamic, RigidbodyConstraints.FreezePositionZ);
        }
        else if (data.itemType == Items.PatchPlate)
        {
            _serializedRigidBody = new SCG_RigidBodySerialized(50, 2, 2, true, false, RigidbodyInterpolation.Interpolate, CollisionDetectionMode.ContinuousDynamic, RigidbodyConstraints.FreezePositionZ);
        }
    }

    public void ExternalInit(ItemData itemData)
    {
        // For items that are created after the beginning of the level

        data = itemData;

        GameObject gO = Instantiate<GameObject>(Resources.Load<GameObject>(data.itemType.ToString() + "_Prefab"), transform.position, transform.rotation, transform);
    }

    public void Charge (float rate)
    {
        data.capacity += rate * Time.deltaTime;
        Mathf.Clamp(data.capacity, 0, 100);
    }

    public void Discharge (float rate)
    {
        data.capacity -= rate * Time.deltaTime;
        Mathf.Clamp(data.capacity, 0, 100);
    }

    public override void Muscle()
    {

    }

    public override void Operate()
    {

    }

    public bool AttemptGrab()
    {
        return data.IfCarryable();
    }

    public SCG_RigidBodySerialized Grabbed()
    {
        Destroy(GetComponent<Rigidbody>());
        return _serializedRigidBody;
    }

    public void Release()
    {
        gameObject.AddComponent<Rigidbody>();
        _serializedRigidBody.RestoreRigidbody(GetComponent<Rigidbody>());
    }
}

public class ItemData
{
    public Items itemType;
    public float capacity;
    private bool _carryable;

    public ItemData (Items itemType, float capacity)
    {
        this.itemType = itemType;
        this.capacity = capacity;

        if (itemType == Items.Obstruction)
            _carryable = false;
        else
            _carryable = true;
    }

    public bool IfCarryable()
    {
        return _carryable;
    }
}
