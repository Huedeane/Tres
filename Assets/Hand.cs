using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Hand : MonoBehaviour
{

    [Header("Card Holder Prefab")]
    [SerializeField] private GameObject CardHolderPrefab;

    public void AddCard(GameObject cardObject)
    {
        GameObject newHolder = Instantiate(CardHolderPrefab, transform.position, transform.rotation) as GameObject;
        newHolder.transform.SetParent(this.transform, true);
        newHolder.GetComponent<RectTransform>().rotation = GetComponent<RectTransform>().rotation;
        cardObject.transform.SetParent(newHolder.transform, true);
    }
}
