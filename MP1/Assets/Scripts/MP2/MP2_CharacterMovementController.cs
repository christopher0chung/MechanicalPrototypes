using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MP2_CharacterMovementController : MonoBehaviour, MP2_IConstructable {

    private SCG_EventManager _eventManager;

    private GameObject _GO_controlledCharacter;
    private Rigidbody _RB_controlledCharacter;

    private Enum_MP2_ID _thisID;

    public void CalledAwake(Enum_MP2_ID id) {
        _thisID = id;

        _eventManager = MP2_ServiceLocator.instance.EventManager;
    }

    public void CalledStart () {
        ConstructCharacter();

        _eventManager.Fire(new E_CharConstructed(_thisID));
    }

    void Update () {
		
	}

    #region Internal Functions
    private void ConstructCharacter()
    {
        _GO_controlledCharacter = Instantiate<GameObject>(Resources.Load<GameObject>("Player_Empty"));

        _GO_controlledCharacter.name = _thisID.ToString();

        _RB_controlledCharacter = _GO_controlledCharacter.GetComponent<Rigidbody>();
    }
    #endregion
}

public enum Enum_MP2_ID { Player0, Player1 }
public enum Enum_MP2_ControlState { Character, Menu, Station }