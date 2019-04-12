using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour
{

    [Header("Card Holder Prefab")]
    [SerializeField] private GameObject CardHolderPrefab;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }



    public void AddCard(GameObject cardObject)
    {
        GameObject newHolder = Instantiate(CardHolderPrefab, transform.position, transform.rotation) as GameObject;
        newHolder.transform.SetParent(this.transform, true);
        cardObject.transform.SetParent(newHolder.transform, true);
    }
}
