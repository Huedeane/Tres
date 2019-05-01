using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlayerCommand : MonoBehaviour
{
    Player myPlayer;

    Coroutine messageCoroutine;

    void Start()
    {
        myPlayer = GetComponent<Player>();
    }

    public void processCmd(string cmd)
    {
        Player otherPlayer = myPlayer.getNextPlayer();

        string[] parts = cmd.Split(':');
        string cmdName = parts[0];

        Deck deck;
        Card card;

        switch (cmdName)
        {
            case "Draw Card":
                deck = GameObject.FindGameObjectWithTag("Deck").GetComponent<Deck>();
                card = deck.GetTopCard();
                card.CardLocation = E_ZoneType.Hand;
                card.MoveCard(GetComponentInChildren<Hand>().gameObject);

                if (myPlayer.isLocalPlayer)
                {
                    card.Reveal();
                    myPlayer.playerHand.SetHighlight();
                }
                else
                {
                    card.Hide();
                }
                break;
            case "Play Card":
                Pile pile = GameObject.FindGameObjectWithTag("Pile").GetComponent<Pile>();
                card = myPlayer.playerHand.SelectedCard;
                card.CardLocation = E_ZoneType.Pile;
                card.Reveal();
                card.IsInteractable = false;
                card.ToggleAvailable(false);
                card.ToggleSelected(false);
                card.MoveCard(pile.gameObject);
                break;
            case "Enable Chat":
                Debug.Log("Test");
                GameObject.Find("Chat Input").GetComponent<InputField>().interactable = true;
                break;
            case "Toggle Time":
                GameObject.Find("Time").GetComponentInChildren<InfoTime>().ToggleTime();
                break;
            case "Set Connection Status":
                GameObject[] playerStats = GameObject.FindGameObjectsWithTag("PlayerStatus");
                foreach (GameObject gameObject in playerStats)
                {
                    if (gameObject.name == "Player 1")
                    {
                        gameObject.GetComponentInChildren<TextMeshProUGUI>().SetText("Player 1: Connected");
                    }
                    else if (gameObject.name == "Player 2")
                    {
                        gameObject.GetComponentInChildren<TextMeshProUGUI>().SetText("Player 2: Connected");
                    }
                }
                break;
        }



    }

    public void playCmd()
    {
        if (myPlayer.myTurn == true)
        {
            myPlayer.CmdServerCommandTurn("Play Card");
        }
        else
        {
            myPlayer.sendMessage("NOT YOUR TURN");
        }
    }

    public void extraCmd()
    {
        if (myPlayer.myTurn == true)
        {

        }
        else
        {

        }
    }

    public void drawCmd()
    {
        if (myPlayer.myTurn == true && myPlayer.playerHand.HasAvailable == false)
        {
            myPlayer.CmdServerCommandTurn("Draw Card");
        }
        else
        {
            myPlayer.sendMessage("CAN'T DRAW");
        }
    }

    public void quitCmd()
    {
        if (myPlayer.myTurn == true)
        {
            
        }
        else
        {

        }
    }

    public void clickCommand()
    {
        if (myPlayer.myTurn == true)
        {
            
        }
        else
        {
            myPlayer.sendMessage("NOT YOUR TURN!");
        }
    }

}

