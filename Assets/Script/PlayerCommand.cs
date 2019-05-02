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
                    card.IsInteractable = true;
                }
                else
                {
                    card.Hide();
                }

                myPlayer.myTurn = false;
                otherPlayer.myTurn = true;
                break;
            case "Play Card":
                Pile pile = GameObject.FindGameObjectWithTag("Pile").GetComponent<Pile>();

                card = GetCard(int.Parse(parts[1]));
                Hand hand = card.GetComponentInParent<Hand>();

                card.CardLocation = E_ZoneType.Pile;
                card.Reveal();
                card.IsInteractable = false;
                card.ToggleAvailable(false);
                card.ToggleSelected(false);
                card.MoveCard(pile.gameObject);
                hand.UpdateCardList();
                myPlayer.myTurn = false;
                otherPlayer.myTurn = true;
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
            case "End Game":
                GameObject finishMenu = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>().finishMenu;
                finishMenu.SetActive(true);

                if (myPlayer.playerHand.HandCardList.Count == 0)
                {
                    if (myPlayer.isLocalPlayer)
                        finishMenu.GetComponentInChildren<TextMeshProUGUI>().SetText("You Win");
                    else
                        finishMenu.GetComponentInChildren<TextMeshProUGUI>().SetText("You Lose");
                }
                else
                {
                    finishMenu.GetComponentInChildren<TextMeshProUGUI>().SetText("You Lose");
                }

               
                break;
        }



    }

    public void playCmd()
    {
        if (myPlayer.myTurn == true)
        {
            myPlayer.playerScore += 50;
            myPlayer.CmdServerCommandTurn("Play Card:" + myPlayer.playerHand.SelectedCard.CardObjectId);         
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
        // && myPlayer.playerHand.HasAvailable == false
        if (myPlayer.myTurn == true)
        {
            myPlayer.playerScore += 100;
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

    private Card GetCard(int x)
    {
        GameObject[] cardArray = GameObject.FindGameObjectsWithTag("Card");
        foreach (GameObject card in cardArray)
        {
            if (card.GetComponent<Card>().CardObjectId == x)
            {
                return card.GetComponent<Card>();
            }
        }

        return null;
    }

}

