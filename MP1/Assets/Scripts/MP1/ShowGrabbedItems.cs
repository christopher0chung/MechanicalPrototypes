using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowGrabbedItems : MonoBehaviour {

    private List<GameObject> _itemVisuals;

	void Start () {
        transform.root.GetComponent<TestPlayerController>().SetShowGrabbedItems(this);

        _itemVisuals = new List<GameObject>();

        for (int i = transform.childCount - 1; i >= 0; i--)
        {
            _itemVisuals.Add(transform.GetChild(i).gameObject);
            transform.GetChild(i).gameObject.SetActive(false);
        }
	}
	
    public void ShowHeld(string ItemTypeAsString)
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            if (ItemTypeAsString == null)
            {
                _itemVisuals[i].SetActive(false);
            }
            else
            {
                if (_itemVisuals[i].name == ItemTypeAsString)
                    _itemVisuals[i].SetActive(true);
                else
                    _itemVisuals[i].SetActive(false);
            }
        }
    }
}
