using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Model_Game : MonoBehaviour {

    public Database_Character characterData;
    public Database_Game gameData;
    public Database_GameState gameStateData;

    public void Awake()
    {
        characterData = Resources.Load<Database_Character>("Data/CharacterData");
        gameData = Resources.Load<Database_Game>("Data/GameData");
        gameStateData = Resources.Load<Database_GameState>("Data/GameStateData");
    }

}
