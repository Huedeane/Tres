using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IZone {

    int ZoneId {get; set;}
    List<GameObject> cardList { get; set; }
    GameObject ZoneCardContent { get; set; }

    void AddCard(GameObject cardObject);
}
