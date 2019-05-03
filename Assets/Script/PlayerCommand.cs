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
                GameObject.Find("Game Manager").GetComponent<GameManager>().GameBoard.RpcDealCard(otherPlayer.playerNum.ToString(), 1, 1f);
                myPlayer.myTurn = false;
                //GameObject.Find("Game Manager").GetComponent<GameManager>().GameBoard.RpcSetTurns(otherPlayer.playerNum);
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
                ProcessPlayingCard(card.CardType);
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
                    {
                        finishMenu.GetComponentInChildren<TextMeshProUGUI>().SetText("You Win");
                        AudioManager.Instance.ChangeBackground(E_BackGroundMusic.Victory_Theme);
                    }
                    else
                    {
                        finishMenu.GetComponentInChildren<TextMeshProUGUI>().SetText("You Lose");
                        AudioManager.Instance.ChangeBackground(E_BackGroundMusic.Defeat_Theme);
                    }
                        
                }              
                break;
        }



    }

    public void ProcessPlayingCard(E_CardType cardType)
    {
        Player otherPlayer = myPlayer.getNextPlayer();
        switch (cardType)
        {
            case E_CardType.Draw_Two:
                GameObject.Find("Game Manager").GetComponent<GameManager>().GameBoard.RpcDealCard(otherPlayer.playerNum.ToString(), 2, 1f);
                //GameObject.Find("Game Manager").GetComponent<GameManager>().GameBoard.RpcSetTurns(myPlayer.playerNum);
                if (myPlayer.isLocalPlayer)
                    AudioManager.Instance.PlaySoundEffect(E_SoundEffect.Your_Turn);
                break;
            case E_CardType.Draw_Four:
                GameObject.Find("Game Manager").GetComponent<GameManager>().GameBoard.RpcDealCard(otherPlayer.playerNum.ToString(), 4, 1f);
                //GameObject.Find("Game Manager").GetComponent<GameManager>().GameBoard.RpcSetTurns(myPlayer.playerNum);
                if (myPlayer.isLocalPlayer)
                    AudioManager.Instance.PlaySoundEffect(E_SoundEffect.Your_Turn);
                break;
            case E_CardType.Reverse:
                myPlayer.myTurn = false;
                otherPlayer.myTurn = true;
                break;
            case E_CardType.Skip:
                myPlayer.playerHand.SetHighlight(true);
                if (myPlayer.isLocalPlayer)
                    AudioManager.Instance.PlaySoundEffect(E_SoundEffect.Your_Turn);
                break;
            case E_CardType.Change_Color:
                break;
            default:
                myPlayer.myTurn = false;
                otherPlayer.myTurn = true;
                break;
        }
    }

    public void playCmd()
    {
        if (myPlayer.myTurn == true && myPlayer.playerHand.SelectedCard != null)
        {
            myPlayer.playerScore += 50;
            myPlayer.CmdServerCommandTurn("Play Card:" + myPlayer.playerHand.SelectedCard.CardObjectId);         
        }
        else if(myPlayer.myTurn == false)
        {
            Debug.Log(myPlayer.myTurn);
            myPlayer.sendMessage("NOT YOUR TURN");
        }
        else if (myPlayer.playerHand.SelectedCard == null)
        {
            myPlayer.sendMessage("NO CARD SELECTED");
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
        else if (myPlayer.myTurn != true)
        {
            myPlayer.sendMessage("NOT YOUR TURN");
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

