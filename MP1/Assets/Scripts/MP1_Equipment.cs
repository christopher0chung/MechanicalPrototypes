using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MP1_Equipment : MonoBehaviour
{
    // Equipment are objects in the game that let the player act upon other objects indirectly
    // Equipment types determine certain properties
    // Carryable equipment is carryable by the player
    // Type determines rigidbody properties
    // Equipment can be damaged
    // Interaction with and through equipment is handled via menu
    // Equipment may have requirements in order to operate

    protected SCG_RigidBodySerialized _serializedRigidBody;

    public Equipment equipmentType;

    public EquipmentData equipmentData;

    private void Start()
    {
        // For items that exist at the beginning of the level
        if (equipmentType == Equipment.Welder || equipmentType == Equipment.PneumaticWrench)
        {
            Debug.Assert(GetComponent<Rigidbody>(), gameObject.name + " does not have a RigidBody");
            _serializedRigidBody = new SCG_RigidBodySerialized(GetComponent<Rigidbody>());
        }
    }

    private void _MakeRigidBody()
    {
        // Does not work for Obstructions
        // Obstructions should not be created at runtime

        if (equipmentData.equipmentType == Equipment.Welder)
        {
            _serializedRigidBody = new SCG_RigidBodySerialized(50, 2, 2, true, false, RigidbodyInterpolation.Interpolate, CollisionDetectionMode.ContinuousDynamic, RigidbodyConstraints.FreezePositionZ);
        }
        else if (equipmentData.equipmentType == Equipment.PneumaticWrench)
        {
            _serializedRigidBody = new SCG_RigidBodySerialized(50, 2, 2, true, false, RigidbodyInterpolation.Interpolate, CollisionDetectionMode.ContinuousDynamic, RigidbodyConstraints.FreezePositionZ);
        }
    }

    public void ExternalInit(EquipmentData equipmentData)
    {
        // For items that are created after the beginning of the level

        this.equipmentData = equipmentData;

        GameObject gO = Instantiate<GameObject>(Resources.Load<GameObject>(equipmentData.equipmentType.ToString() + "_Prefab"), transform.position, transform.rotation, transform);
    }

    public bool AttemptGrab()
    {
        return equipmentData.IfCarryable();
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

public class EquipmentData
{
    public Equipment equipmentType;
    public bool damaged;
    private bool _carryable;

    public EquipmentData(Equipment equipmentType, bool damaged)
    {
        this.equipmentType = equipmentType;
        this.damaged = damaged;

        if (equipmentType == Equipment.Welder || equipmentType == Equipment.PneumaticWrench)
            _carryable = true;
        else
            _carryable = true;
    }

    public bool IfCarryable()
    {
        return _carryable;
    }
}