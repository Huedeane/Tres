using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;

#pragma warning disable 0618

public class InfoConnectionStatus : MonoBehaviour {

    public TextMeshProUGUI Player1;
    public TextMeshProUGUI Player2;

    private void Start()
    {
        this.gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        //Player1.SetText("Player 1: " + Player1Status);
        //Player2.SetText("Player 2: " + Player2Status);
    }
}
