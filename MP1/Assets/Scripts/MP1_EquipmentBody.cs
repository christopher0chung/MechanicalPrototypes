using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MP1_EquipmentBody : MP1_PhysicalForm
{
    // The class handles the physical aspects of equipment

    // Equipment are objects in the game that let the player act upon other objects indirectly
    // Equipment types determine certain properties
    // Carryable equipment is carryable by the player
    // Type determines rigidbody properties
    // Equipment can be damaged
    // Interaction with and through equipment is handled via menu
    // Equipment may have requirements in order to operate

    public bool startingLooseObject;
    public Equipment startingEquipmentType;
    public bool startingDamagedCondition;

    private void Start()
    {
        if (startingLooseObject)
        {
            MP1_EquipmentData e = new MP1_EquipmentData(startingEquipmentType, startingDamagedCondition);
            e.LooseInit(transform.position, this);
        }
    }

    public void Init(MP1_EquipmentData eqpData, Vector3 where)
    {
        data = (MP1_EquipmentData)eqpData;
        MP1_EquipmentData e = data as MP1_EquipmentData;

        gameObject.name = eqpData.equipmentType.ToString();

        transform.position = where;

        gameObject.layer = 9;
        gameObject.AddComponent<Rigidbody>();
        e.GetSerializedRigidbody().RestoreRigidbody(GetComponent<Rigidbody>());

        GameObject gO = Instantiate(Resources.Load<GameObject>(e.equipmentType.ToString() + "_Prefab"), transform.position, Quaternion.identity, transform);
    }

    public void CleanUp()
    {
        Destroy(gameObject);
    }
}

public class MP1_EquipmentData : MP1_Data
{
    public Equipment equipmentType { get; private set; }
    public bool damaged;
    private EquipmentStates _state;
    private SCG_RigidBodySerialized _rigidBody;
    private MP1_EquipmentBody _physicalForm;
    private bool _muscleable;

    private List<Items> _equipableItems;
    private List<MP1_ItemData> _equippedItems;

    public MP1_EquipmentData(Equipment equipmentType, bool damaged)
    {
        this.equipmentType = equipmentType;
        this.damaged = damaged;

        _equipableItems = new List<Items>();
        _equippedItems = new List<MP1_ItemData>();

        if (equipmentType == Equipment.Accumulator)
        {
            _carryable = false;
            _equipableItems.Add(Items.ArTank);
            _rigidBody = new SCG_RigidBodySerialized(50, 2, 2, false, true, RigidbodyInterpolation.None, CollisionDetectionMode.Continuous, RigidbodyConstraints.FreezeAll);
            _state = EquipmentStates.Static;
        }
        else if (equipmentType == Equipment.ArCharger)
        {
            _carryable = false;
            _equipableItems.Add(Items.ArTank);
            _rigidBody = new SCG_RigidBodySerialized(50, 2, 2, false, true, RigidbodyInterpolation.None, CollisionDetectionMode.Continuous, RigidbodyConstraints.FreezeAll);
            _state = EquipmentStates.Static;
        }
        else if (equipmentType == Equipment.BatteryCharger)
        {
            _carryable = false;
            _equipableItems.Add(Items.Battery);
            _rigidBody = new SCG_RigidBodySerialized(50, 2, 2, false, true, RigidbodyInterpolation.None, CollisionDetectionMode.Continuous, RigidbodyConstraints.FreezeAll);
            _state = EquipmentStates.Static;
        }
        else if (equipmentType == Equipment.Door)
        {
            _carryable = false;
            _rigidBody = new SCG_RigidBodySerialized(50, 2, 2, false, true, RigidbodyInterpolation.None, CollisionDetectionMode.Continuous, RigidbodyConstraints.FreezeAll);
            _state = EquipmentStates.Static;
        }
        else if (equipmentType == Equipment.Lockers)
        {
            _carryable = false;
            _equipableItems.Add(Items.ArTank);
            _equipableItems.Add(Items.Battery);
            _equipableItems.Add(Items.O2Tank);
            _equipableItems.Add(Items.PatchPlate);
            _rigidBody = new SCG_RigidBodySerialized(50, 2, 2, false, true, RigidbodyInterpolation.None, CollisionDetectionMode.Continuous, RigidbodyConstraints.FreezeAll);
            _state = EquipmentStates.Static;
        }
        else if (equipmentType == Equipment.O2Charger)
        {
            _carryable = false;
            _equipableItems.Add(Items.O2Tank);
            _rigidBody = new SCG_RigidBodySerialized(50, 2, 2, false, true, RigidbodyInterpolation.None, CollisionDetectionMode.Continuous, RigidbodyConstraints.FreezeAll);
            _state = EquipmentStates.Static;
        }
        else if (equipmentType == Equipment.PneumaticWrench)
        {
            _carryable = true;
            _equipableItems.Add(Items.O2Tank);
            _rigidBody = new SCG_RigidBodySerialized(50, 2, 2, true, false, RigidbodyInterpolation.Interpolate, CollisionDetectionMode.ContinuousDynamic, RigidbodyConstraints.FreezePositionZ);
        }
        else if (equipmentType == Equipment.Welder)
        {
            _carryable = true;
            _equipableItems.Add(Items.ArTank);
            _rigidBody = new SCG_RigidBodySerialized(100, 2, 2, true, false, RigidbodyInterpolation.Interpolate, CollisionDetectionMode.ContinuousDynamic, RigidbodyConstraints.FreezePositionZ);
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

    public bool IfMuscleable()
    {
        return _muscleable;
    }

    public SCG_RigidBodySerialized GetSerializedRigidbody()
    {
        return _rigidBody;
    }

    public EquipmentStates GetState()
    {
        return _state;
    }
    #endregion

    #region Internal Functions

    private void _MakeALooseBody(Vector3 where, MP1_EquipmentBody eqp)
    {
        _physicalForm = eqp;
        _physicalForm.Init(this, where);
    }

    private void _MakeABody(Vector3 where)
    {
        GameObject gO = new GameObject();
        _physicalForm = gO.AddComponent<MP1_EquipmentBody>();
        _physicalForm.Init(this, where);
    }

    private void _RemoveBody()
    {
        Debug.Assert(_physicalForm != null, "Attempting to destroy a non-existant body");
        _physicalForm.CleanUp();
    }

    private bool _EquippedContainsType(Items itemType)
    {
        if (_equippedItems.Count == 0)
            return false;
        else
        {
            bool checkingList = false;
            for (int i = 0; i < _equippedItems.Count; i++)
            {
                if (_equippedItems[i].itemType == itemType)
                    checkingList = true;
            }
            return checkingList;
        }

    }

    #endregion

    #region Body Functions

    // Called by MP1_Item if and only if a loose item at start of level.
    public void LooseInit(Vector3 where, MP1_EquipmentBody looseObject)
    {
        if (equipmentType == Equipment.PneumaticWrench || equipmentType == Equipment.Welder)
            _state = EquipmentStates.Free;
        _MakeALooseBody(where, looseObject);
    }

    #endregion

    #region Item Manager Functions

    // Called by MP1_ItemManager
    public void MakeHeld()
    {
        _state = EquipmentStates.Held;
        _RemoveBody();
    }

    public void MakeFree(Vector3 where)
    {
        _state = EquipmentStates.Free;
        _MakeABody(where);
    }

    public bool AttemptToEquipItem(MP1_ItemData i)
    {
        // Only equip to Free or Static equipment
        // If it's of an equipable type, check to see if there's already one of that type
        // If it's not an equipable type, do not permit
        if (_state == EquipmentStates.Held)
            return false;

        if (_equipableItems.Contains(i.itemType))
        {
            if (!_EquippedContainsType(i.itemType))
                return true;
            else
                return false;
        }
        else
            return false;
    }

    public void EquipItem(MP1_ItemData i)
    {
        _equippedItems.Add(i);
        _rigidBody.mass += i.GetSerializedRigidbody().mass;
        if (_physicalForm != null)
            _physicalForm.GetComponent<Rigidbody>().mass = _rigidBody.mass;
    }

    public bool AttemptToUnequipItem(MP1_ItemData i)
    {
        if (_equippedItems.Contains(i))
            return true;
        else
            return false;
    }

    public void UnequipItem(MP1_ItemData i)
    {
        _equippedItems.Remove(i);
        _rigidBody.mass -= i.GetSerializedRigidbody().mass;
        if (_physicalForm != null)
            _physicalForm.GetComponent<Rigidbody>().mass = _rigidBody.mass;
    }

    #endregion
}

public enum EquipmentStates { Free, Held, Static }