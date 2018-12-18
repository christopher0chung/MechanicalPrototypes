using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CharacterData", menuName = "Data/Character Data")]
public class Database_Character : MP3.MCV.Data.Database_Base {

    public float force_floatingBoost;
    public float force_groundedBoost;
    public float force_walkingFoce;
    public float range_interaction;
    public float range_highlight;
    public float time_dropRecovery;
    public float time_action;
    public float angle_maxUp;
    public float angle_maxDown;
}
