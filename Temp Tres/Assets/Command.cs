using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

#pragma warning disable 0618

public class Command : NetworkBehaviour
{
    Player myPlayer;

    public void OnEnable()
    {
        EventManager.StartListening(E_EventName.Player_Join, UpdatePlayerLocation);
    }
    public void OnDisable()
    {
        EventManager.StopListening(E_EventName.Player_Join, CommandOnPlayerJoin);
    }
    void Start()
    {
        myPlayer = GetComponent<Player>();
    }



    #region E_EventName.Player_Join
    private void UpdatePlayerLocation(EventParam param)
    {
        CmdUpdatePlayerLocation();
    }
    [Command]
    void CmdUpdatePlayerLocation()
    {
        RpcUpdatePlayerLocation();
    }
    [ClientRpc]
    void RpcUpdatePlayerLocation()
    {
        if (myPlayer.isLocalPlayer)
        {

        }
    }
    #endregion
}
