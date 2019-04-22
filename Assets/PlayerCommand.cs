using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCommand : MonoBehaviour
{
    Player myPlayer;

    void Start()
    {
        myPlayer = GetComponent<Player>();
    }

    public void processCmdString(string cmd)
    {
        Player otherPlayer = myPlayer.getNextPlayer();

        string[] parts = cmd.Split(':');
        string cmdName = parts[0];
        string cmdParam = parts[1];
        if (cmdName == "Dealing Card")
        {
            Card c = transform.Find("DeckZone/" + cmdParam).GetComponent<Card>();
            c.transform.SetParent(transform.Find("HandZone"));
            if (myPlayer.isLocalPlayer)
                c.reveal();
        }
        else if (cmdName == "Attacking Card")
        {
            Card c = transform.Find("HandZone/" + cmdParam).GetComponent<Card>();
            c.transform.SetParent(transform.Find("AttackZone"));
            c.reveal();
        }
        else if (cmdName == "Destroy Attack Card")
        {
            getAttackCard(myPlayer).Die();
        }


        if (myPlayer.isServer && cmdName != "Destroy Attack Card")
        {
            //DO BATTLE AFTER EVERY CLIENT COMMAND
            Card myAttackCard = getAttackCard(myPlayer);
            Card otherAttackCard = getAttackCard(otherPlayer);
            if (myAttackCard != null && otherAttackCard != null)
            {
                if (myAttackCard.value >= otherAttackCard.value)
                    myPlayer.score += (myAttackCard.value - otherAttackCard.value);
                else
                    otherPlayer.score += (otherAttackCard.value - myAttackCard.value);

                //send command to destory attack card for both players
                myPlayer.RpcClientCommand("Destroy Attack Card:");
                otherPlayer.RpcClientCommand("Destroy Attack Card:");
            }
            else if (myAttackCard != null)
            {
                myPlayer.score += 1;
            }
            else if (otherAttackCard != null)
            {
                otherPlayer.score += 1;
            }
        }
    }
}
