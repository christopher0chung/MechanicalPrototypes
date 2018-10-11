using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MP1_ItemsManager {

    private List<MP1_ItemData> _sceneItems;

    public MP1_ItemsManager()
    {
        _sceneItems = new List<MP1_ItemData>();
    }

    public bool Register(MP1_ItemData item)
    {
        if (_sceneItems.Contains(item))
            return false;
        else
        {
            _sceneItems.Add(item);
            return true;
        }
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
    // Bitshifted to raycast only UserLayer 8 which should be "Interactables"
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
                    if (_sceneItems.Contains((MP1_ItemData)info.transform.root.GetComponent<MP1_ItemBody>().data))
                    {
                        _SetRefHighlighted(ID, (MP1_ItemData)info.transform.root.GetComponent<MP1_ItemBody>().data);
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
                else if ((info.transform.root.GetComponent<MP1_PhysicalForm>().GetType() == typeof(MP1_Equipment)))
                {
                    Debug.Log("Found Equipment");
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

    public bool RequestToMuscle(PlayerID ID)
    {
        Debug.Log("Request to muscle");
        if (_GetRefHighlighted(ID) != null)
        {
            return true;
        }
        else
            return false;
    }
    public void Muscle(PlayerID ID)
    {
        Debug.Log(_GetRefHighlighted(ID).GetType().ToString());
        if (_GetRefHighlighted(ID).GetType() == typeof(MP1_ItemData))
        {
            RequestToHoldItem((MP1_ItemData)_GetRefHighlighted(ID), _GetRefController(ID));
        }
    }
       
    public void RequestToHoldItem(MP1_ItemData item, TestPlayerController player)
    {
        Debug.Assert(_sceneItems.Contains(item), "Modifying unregistered item");
        Debug.Assert(item.GetState() == ItemStates.Free, "Attempting to control a non-free item");

        //player.Hold(item); does not yet exist
        item.MakeHeld();
        player.GrabCallback(item);
    }

    public void RequestToStageItem(MP1_ItemData item, MP1_Equipment equipment)
    {
        Debug.Assert(_sceneItems.Contains(item), "Modifying unregistered item");
        Debug.Assert(item.GetState() == ItemStates.Held, "Attempting to stage a uncontrolled item");

        //equipment.Stage(item); does not yet exist
        item.MakeStaged();
    }

    public void RequestToReleaseItem(MP1_ItemData item, TestPlayerController player, LastFacingDirection lastFacingDirection)
    {
        Debug.Assert(_sceneItems.Contains(item), "Modifying unregistered item");
        Debug.Assert(item.GetState() == ItemStates.Held, "Attempting to release an non-held item");

        Vector3 posToMakeDroppedItem;
        if (lastFacingDirection == LastFacingDirection.Left)
            posToMakeDroppedItem = player.transform.position + Quaternion.Euler(0, -90, 0) * player.GetGrabOffset().localPosition;
        else
            posToMakeDroppedItem = player.transform.position + Quaternion.Euler(0, 90, 0) * player.GetGrabOffset().localPosition;

        item.MakeFree(posToMakeDroppedItem);
        player.ReleaseCallback();
    }

    public void RequestToLaunchItem(MP1_ItemData item, MP1_Equipment equipment, Vector3 direction, float launchImpulse)
    {
        Debug.Assert(_sceneItems.Contains(item), "Modifying unregistered item");
        Debug.Assert(item.GetState() == ItemStates.Staged, "Attempting to launch unstaged item");

        item.MakeLaunched(equipment.transform.position, direction, launchImpulse);
    }

}

public enum PlayerID { Player0, Player1 }
