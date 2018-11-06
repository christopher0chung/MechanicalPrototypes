using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptableObjectReferencePassTest : MonoBehaviour
{
    public MP2_Item itemReference;
    [Range(-1, 100)] public float startingPercent;

    public MP2_Item item { get; private set; }

    public ScriptableObjectReferencePassTest otherOne;

    void Start()
    {
        item = ScriptableObject.Instantiate(itemReference);
        item.percent = 100;
        item.currentState = Enum_MP2_ItemStates.Loose;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (item != null)
                otherOne.item = item;
            else
                return;
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (gameObject.name == "Two")
            {
                item.percent += 10;
                Debug.Log("Object " + gameObject.name + " has a percent of: " + item.percent);
            }
            else
            {
                Debug.Log("Object " + gameObject.name + " has a percent of: " + item.percent);
            }
        }
    }
}
