using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MP1_ItemsManager {

    private List<ItemData> _sceneItems;

    public MP1_ItemsManager()
    {
        _sceneItems = new List<ItemData>();
    }

    public bool Register(ItemData interactable)
    {
        if (_sceneItems.Contains(interactable))
            return false;
        else
        {
            _sceneItems.Add(interactable);
            return true;
        }
    }

    public void Clear()
    {
        _sceneItems.Clear();
    }

    public void RequestToHoldItem(ItemData item, TestPlayerController player)
    {
        Debug.Assert(_sceneItems.Contains(item), "Modifying unregistered item");
        Debug.Assert(item.GetState() == ItemStates.Free, "Attempting to control a non-free item");

        //player.Hold(item); does not yet exist
        item.Hold();
    }

    public void RequestToStageItem(ItemData item, MP1_Equipment equipment)
    {
        Debug.Assert(_sceneItems.Contains(item), "Modifying unregistered item");
        Debug.Assert(item.GetState() == ItemStates.Held, "Attempting to stage a uncontrolled item");

        //equipment.Stage(item); does not yet exist
        item.Stage();
    }

    public void RequestToReleaseItem(ItemData item, TestPlayerController player)
    {
        Debug.Assert(_sceneItems.Contains(item), "Modifying unregistered item");
        Debug.Assert(item.GetState() == ItemStates.Held, "Attempting to release an non-held item");

        item.LetGo(player.GetGrabOffset().position);
    }

    public void RequestToLaunchItem(ItemData item, MP1_Equipment equipment, Vector3 direction, float launchImpulse)
    {
        Debug.Assert(_sceneItems.Contains(item), "Modifying unregistered item");
        Debug.Assert(item.GetState() == ItemStates.Staged, "Attempting to launch unstaged item");

        item.Unstage(equipment.transform.position, direction, launchImpulse);
    }










    public ItemData p0Grabbed;
    public ItemData p1Grabbed;

    public ItemData PlayerHolding(PlayerID playerID)
    {
        if (playerID == PlayerID.Player0)
            return p0Grabbed;
        else
            return p1Grabbed;
    }

    public void PlayerAttemptToGrab(PlayerID playerID, ItemData item)
    {
        Debug.Assert(item != null, "Null interactable attempted to be grabbed");

        if (_sceneItems.Contains(item))
        {
            if (playerID == PlayerID.Player0)
            {
                if (p0Grabbed == null)
                {
                    p0Grabbed = item;
                }
            }
            if (playerID == PlayerID.Player1)
            {
                if (p1Grabbed == null)
                {
                    p1Grabbed = item;
                }
            }
        }
    } 
}

public enum PlayerID { Player0, Player1 }
