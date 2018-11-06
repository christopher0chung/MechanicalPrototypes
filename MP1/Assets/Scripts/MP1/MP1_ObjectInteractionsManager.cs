using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MP1_ObjectInteractionsManager {

    private List<MP1_Data> _sceneItems;

    public MP1_ObjectInteractionsManager()
    {
        _sceneItems = new List<MP1_Data>();
    }

    public bool RequestToRegister(MP1_Data objectData)
    {
        if (_sceneItems.Contains(objectData))
            return false;
        else
            return true;

    }

    public void Register(MP1_Data objectData)
    {
        _sceneItems.Add(objectData);
    }

    public void RegisterPlayerControllers(TestPlayerController tpc)
    {
        if (tpc.ID == PlayerID.Player0)
            p1Controller = tpc;
        else
            p2Controller = tpc;
    }

    public void Clear()
    {
        _sceneItems.Clear();
    }

    TestPlayerController p1Controller;
    TestPlayerController p2Controller;

    private TestPlayerController _GetRefController(PlayerID id)
    {
        if (id == PlayerID.Player0)
            return p1Controller;
        else
            return p2Controller;
    }

    MP1_Data p1Highlighted;
    MP1_Data p2Highlighted;

    private void _SetRefHighlighted(PlayerID ID, MP1_Data data)
    {
        if (ID == PlayerID.Player0)
        {
            p1Highlighted = data;
        }
        else
        {
            p2Highlighted = data;
        }
    }

    private MP1_Data _GetRefHighlighted(PlayerID ID)
    {
        if (ID == PlayerID.Player0)
        {
            return p1Highlighted;
        }
        else
        {
            return p2Highlighted;
        }
    }

    RaycastHit info;
    Ray interactionRay;
    // Bitshifted to raycast only UserLayer 9 which should be "Interactables"
    LayerMask lm = 1 << 9;

    public void PlayerInteractSelect(PlayerID ID, Vector3 lookOrigin, Vector3 lookDirection)
    {
        interactionRay.origin = lookOrigin;
        interactionRay.direction = lookDirection;

        Physics.Raycast(interactionRay, out info, 1.8f, lm, QueryTriggerInteraction.Ignore);
        if (info.collider != null)
        {
            if (info.transform.root.GetComponent<MP1_PhysicalForm>())
            {
                if (info.transform.root.GetComponent<MP1_PhysicalForm>().GetType() == typeof(MP1_ItemBody))
                {
                    _SetRefHighlighted(ID, info.transform.root.GetComponent<MP1_ItemBody>().data);
                }
                else if (info.transform.root.GetComponent<MP1_PhysicalForm>().GetType() == typeof(MP1_EquipmentBody))
                {
                    _SetRefHighlighted(ID, info.transform.root.GetComponent<MP1_EquipmentBody>().data);
                }

                for (int i = 0; i < _sceneItems.Count; i++)
                {
                    if (_sceneItems[i] == p1Highlighted /*Should also do a check for p2, but does not exist yet*/)
                    {
                        _sceneItems[i].SetHighlighted(true);
                    }
                    else
                    {
                        _sceneItems[i].SetHighlighted(false);
                    }
                }
            }
        }
        //If nothing is found
        else
        {
            for (int i = 0; i < _sceneItems.Count; i++)
            {
                _sceneItems[i].SetHighlighted(false);
            }
            _SetRefHighlighted(ID, null);
        }
    }

    public void PlayerInteractDisable(PlayerID ID)
    {
        _SetRefHighlighted(ID, null);
    }

    public bool RequestToMuscle(PlayerID ID)
    {
        Debug.Log("Request to muscle");
        if (_GetRefHighlighted(ID) != null)
        {
            if (_GetRefHighlighted(ID).GetType() == typeof(MP1_ItemData))
            {
                MP1_ItemData i = (MP1_ItemData)_GetRefHighlighted(ID);
                if (i.IfCarryable())
                    return true;
                else
                    return false;
            }
            else if (_GetRefHighlighted(ID).GetType() == typeof(MP1_EquipmentData))
            {
                MP1_EquipmentData e = (MP1_EquipmentData)_GetRefHighlighted(ID);
                if (e.IfCarryable() || e.IfMuscleable())
                    return true;
                else
                    return false;
            }
            else
                return false;
        }
        else
            return false;
    }

    public void Muscle(PlayerID ID)
    {
        Debug.Log(_GetRefHighlighted(ID).GetType().ToString());
        if (_GetRefHighlighted(ID).GetType() == typeof(MP1_ItemData))
        {
            _RequestToHoldItem((MP1_ItemData)_GetRefHighlighted(ID), _GetRefController(ID));
        }
        else if (_GetRefHighlighted(ID).GetType() == typeof(MP1_EquipmentData))
        {
            MP1_EquipmentData e = (MP1_EquipmentData)_GetRefHighlighted(ID);
            if (e.IfCarryable())
                _RequestToHoldItem((MP1_EquipmentData)_GetRefHighlighted(ID), _GetRefController(ID));
            else
                _RequestToOtherMuscleAction();
        }
    }

    public bool RequestToOperate(PlayerID ID)
    {
        return false;
    }

    public void Operate(PlayerID ID)
    {

    }
       
    private void _RequestToHoldItem(MP1_Data thing, TestPlayerController player)
    {
        Debug.Assert(_sceneItems.Contains(thing), "Modifying unregistered item");
        if (thing.GetType() == typeof(MP1_ItemData))
        {
            MP1_ItemData i = (MP1_ItemData)thing;
            Debug.Assert(i.GetState() == ItemStates.Free, "Attempting to control a non-free item");
        }
        else if (thing.GetType() == typeof(MP1_EquipmentData))
        {
            MP1_EquipmentData e = (MP1_EquipmentData)thing;
            Debug.Assert(e.GetState() == EquipmentStates.Free, "Attempting to control a non-free item");
        }

        if (thing.GetType() == typeof(MP1_ItemData))
        {
            MP1_ItemData i = (MP1_ItemData)thing;

            i.MakeHeld();
            player.GrabCallback(i);
        }
        else if (thing.GetType() == typeof(MP1_EquipmentData))
        {
            MP1_EquipmentData e = (MP1_EquipmentData)thing;

            e.MakeHeld();
            player.GrabCallback(e);
        }
    }

    private void _RequestToOtherMuscleAction()
    { }

    private void _RequestToStageItem(MP1_Data item, MP1_EquipmentBody equipment)
    {
        // Commented because menu does not exist

        //Debug.Assert(_sceneItems.Contains(item), "Modifying unregistered item");
        //Debug.Assert(item.GetState() == ItemStates.Held, "Attempting to stage a uncontrolled item");

        ////equipment.Stage(item); does not yet exist
        //item.MakeStaged();
    }

    private void _RequestToLaunchItem(MP1_Data item, MP1_EquipmentBody equipment, Vector3 direction, float launchImpulse)
    {
        // Commented because menu does not exist

        //Debug.Assert(_sceneItems.Contains(item), "Modifying unregistered item");
        //Debug.Assert(item.GetState() == ItemStates.Staged, "Attempting to launch unstaged item");

        //item.MakeLaunched(equipment.transform.position, direction, launchImpulse);
    }

    public void RequestToReleaseItem(MP1_Data item, TestPlayerController player, LastFacingDirection lastFacingDirection)
    {
        Debug.Assert(_sceneItems.Contains(item), "Modifying unregistered item");
        //Debug.Assert(item.GetState() == ItemStates.Held, "Attempting to release an non-held item");

        Vector3 posToMakeDroppedItem;
        if (lastFacingDirection == LastFacingDirection.Left)
            posToMakeDroppedItem = player.transform.position + Quaternion.Euler(0, -90, 0) * player.GetGrabOffset().localPosition;
        else
            posToMakeDroppedItem = player.transform.position + Quaternion.Euler(0, 90, 0) * player.GetGrabOffset().localPosition;

        if (item.GetType() == typeof(MP1_ItemData))
        {
            MP1_ItemData i = (MP1_ItemData)item;
            i.MakeFree(posToMakeDroppedItem);
        }
        else if (item.GetType() == typeof(MP1_EquipmentData))
        {
            MP1_EquipmentData e = (MP1_EquipmentData)item;
            e.MakeFree(posToMakeDroppedItem);
        }

        player.ReleaseCallback();
    }
}

public enum PlayerID { Player0, Player1 }
