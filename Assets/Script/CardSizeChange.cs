using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardSizeChange : MonoBehaviour {

    [SerializeField] private float CardWidth;
    [SerializeField] private List<GameObject> CardList;

    void ControlSizeWidth()
    {   

        float layoutWidth = this.GetComponent<RectTransform>().rect.width;
        float layoutSpacing = this.GetComponent<HorizontalLayoutGroup>().spacing;
        
        float totalSize = (CardWidth + layoutSpacing) * this.transform.childCount;

        if(totalSize > layoutWidth)
        {
            GetComponent<HorizontalLayoutGroup>().childForceExpandWidth = true;
            GetComponent<HorizontalLayoutGroup>().childControlWidth = true;
        }
        else
        {
            GetComponent<HorizontalLayoutGroup>().childForceExpandWidth = false;
            GetComponent<HorizontalLayoutGroup>().childControlWidth = false;

            foreach (Transform child in transform)
            {
                child.GetComponent<RectTransform>().sizeDelta = new Vector2(CardWidth, 100);

            }
        }
    }

	
	// Update is called once per frame
	void Update () {
        ControlSizeWidth();
	}
}
