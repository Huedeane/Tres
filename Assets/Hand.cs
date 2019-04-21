using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hand : MonoBehaviour
{

    [Header("Card Holder Prefab")]
    [SerializeField] private List<GameObject> HandCardList;
    [SerializeField] private GameObject CardHolderPrefab;
    [SerializeField] private float CardWidth;

    private void ControlSizeWidth()
    {

        float layoutWidth = this.GetComponent<RectTransform>().rect.width;
        float layoutSpacing = this.GetComponent<HorizontalLayoutGroup>().spacing;

        float totalSize = (CardWidth + layoutSpacing) * this.transform.childCount;

        if (totalSize > layoutWidth)
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

    private void UpdateCardList()
    {
        //Clear out the Card List
        HandCardList.Clear();

        //Remake the Card List and update card location
        if (transform.childCount > 0)
        {
            foreach (Transform child in transform)
            {
                if (child.childCount == 0)
                    Destroy(child.gameObject);
                else
                {
                    Transform innerChild = child.GetChild(0);
                    HandCardList.Add(innerChild.gameObject);
                }
            }
        }

        ControlSizeWidth();
    }

    public void AddCard(GameObject cardObject)
    {
        GameObject newHolder = Instantiate(CardHolderPrefab, transform.position, transform.rotation) as GameObject;
        newHolder.transform.SetParent(this.transform, true);
        newHolder.GetComponent<RectTransform>().rotation = GetComponent<RectTransform>().rotation;
        cardObject.transform.SetParent(newHolder.transform, true);
        UpdateCardList();
    }
}
