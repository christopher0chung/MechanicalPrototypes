using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

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

    TestPlayerController p1Controller;
    TestPlayerController p2Controller;
    TestPlayerController activep1Controller;
    RaycastHit info;
    Ray interactionRay;
    // Bitshifted to raycast only UserLayer 8 which should be "Interactables"
    LayerMask lm = 1 << 9;

    public void PlayerInteractSelect(PlayerID ID, Vector3 lookOrigin, Vector3 lookDirection)
    {
        if (ID == PlayerID.Player0)
            activep1Controller = p1Controller;
        else
            activep1Controller = p2Controller;

        interactionRay.origin = lookOrigin;
        interactionRay.direction = lookDirection;

        Physics.Raycast(interactionRay, out info, 1.8f, lm, QueryTriggerInteraction.Ignore);
        if (info.collider != null)
        {
            if (info.transform.root.GetComponent<MP1_PhysicalForm>())
            {
                //if (info.transform.root.GetComponent<MP1_PhysicalForm>().GetType == typeof(MP1_Item))
            }
        }
    }
       
    public void RequestToHoldItem(ItemData item, TestPlayerController player, TestPlayerController.grabCallback playerCallback)
    {
        Debug.Assert(_sceneItems.Contains(item), "Modifying unregistered item");
        Debug.Assert(item.GetState() == ItemStates.Free, "Attempting to control a non-free item");

        //player.Hold(item); does not yet exist
        item.Hold();
        playerCallback(item);
    }

    public void RequestToStageItem(ItemData item, MP1_Equipment equipment, TestPlayerController.stageCallback playerCallback)
    {
        Debug.Assert(_sceneItems.Contains(item), "Modifying unregistered item");
        Debug.Assert(item.GetState() == ItemStates.Held, "Attempting to stage a uncontrolled item");

        //equipment.Stage(item); does not yet exist
        item.Stage();
        playerCallback();
    }

    public void RequestToReleaseItem(ItemData item, TestPlayerController player, LastFacingDirection lastFacingDirection, TestPlayerController.releaseCallback playerCallback)
    {
        Debug.Assert(_sceneItems.Contains(item), "Modifying unregistered item");
        Debug.Assert(item.GetState() == ItemStates.Held, "Attempting to release an non-held item");

        Vector3 posToMakeDroppedItem;
        if (lastFacingDirection == LastFacingDirection.Left)
            posToMakeDroppedItem = player.transform.position + Quaternion.Euler(0, -90, 0) * player.GetGrabOffset().localPosition;
        else
            posToMakeDroppedItem = player.transform.position + Quaternion.Euler(0, 90, 0) * player.GetGrabOffset().localPosition;

        item.LetGo(posToMakeDroppedItem);
        playerCallback();
    }

    public void RequestToLaunchItem(ItemData item, MP1_Equipment equipment, Vector3 direction, float launchImpulse, Delegate equipmentCallback)
    {
        Debug.Assert(_sceneItems.Contains(item), "Modifying unregistered item");
        Debug.Assert(item.GetState() == ItemStates.Staged, "Attempting to launch unstaged item");

        item.Unstage(equipment.transform.position, direction, launchImpulse);
    }

}

public enum PlayerID { Player0, Player1 }
