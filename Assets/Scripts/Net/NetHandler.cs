using System.Collections;
using System.Collections.Generic;
using Unity.Networking.Transport;
using UnityEngine;

public class NetHandler : MonoBehaviour
{
    private int playerCount = -1;
    public int currentTeam = -1;
    public int readyCount = 0;

    private void Awake()
    {
        RegisterEvents();
    }

    #region
    private void RegisterEvents()
    {
        NetUtility.S_WELCOME += OnWelcomeServer;
        NetUtility.C_WELCOME += OnWelcomeClient;

        NetUtility.C_START_GAME += OnStartGameClient;

        NetUtility.S_GAME_READY += OnGameReadyServer;
        NetUtility.C_GAME_READY += OnGameReadyClient;

        NetUtility.C_LAUNCH_GAME += OnLaunchGameClient;
    }
    private void UnregisterEvents()
    {
        NetUtility.S_WELCOME -= OnWelcomeServer;
        NetUtility.C_WELCOME -= OnWelcomeClient;

        NetUtility.C_START_GAME -= OnStartGameClient;

        NetUtility.S_GAME_READY -= OnGameReadyServer;
        NetUtility.C_GAME_READY -= OnGameReadyClient;

        NetUtility.C_LAUNCH_GAME -= OnLaunchGameClient;
    }

    private void OnWelcomeServer(NetMessage msg, NetworkConnection cnn)
    {
        //Client has connected, assign a team and respond
        NetWelcome nw = msg as NetWelcome;

        nw.AssignedTeam = ++playerCount;

        Server.Instance.SendToClient(cnn, nw);

        //If full, start the game
        if(playerCount == 1)
        {
            Server.Instance.Broadcast(new NetStartGame());
        }
    }
    private void OnWelcomeClient(NetMessage msg)
    {
        //Receive the connection message
        NetWelcome nw = msg as NetWelcome;

        currentTeam = nw.AssignedTeam;

        Debug.Log($"Client assigned team: {nw.AssignedTeam}");
    }

    private void OnStartGameClient(NetMessage msg)
    {
        Debug.Log("LE GAME HAS BEGUN!!!1!!! CUE THE TRUMPETS!");

        //Lobby is full, move to game selection screen
        //GameManager.Instance.Load(1);
        //GameUIManager.Instance.OnOlineGameConnected();
    }

    private void OnGameReadyServer(NetMessage msg, NetworkConnection cnn)
    {
        NetGameReady ng = msg as NetGameReady;

        if (ng.isReady == 0)
        {
            Debug.Log($"Player ID: {currentTeam} just unreadied!");
            readyCount--;
            if (readyCount < 0)
            {
                readyCount = 0;
            }
        }
        else if (ng.isReady == 1)
        {
            Debug.Log($"Player ID: {currentTeam} just readied up!");
            readyCount++;
        }
        else
        {
            Debug.LogError("I don't know how you even sent this. A ready message was sent with a GameReady of 2 or more");
        }
        if(readyCount == 2)
        {
            Debug.Log("BEGEN THE FUCKIN GAME");
            if(currentTeam == 0)
            {
                GameUIManager.Instance.ActivateStartGameButton();
            }
        }

        Server.Instance.SendToClient(cnn, ng);
    }
    private void OnGameReadyClient(NetMessage msg)
    {
        
        NetGameReady ng = msg as NetGameReady;

        Debug.Log($"Got a Ready with game size: {ng.gameSize} and gameId: {ng.gameId}");

        CustomGameMenu.gameSize = ng.gameSize;
        CustomGameMenu.gameId = ng.gameId;

    }

    private void OnLaunchGameClient(NetMessage msg)
    {
        GameManager.Instance.customGameMenu.LoadSceneMulti();
    }
    #endregion

    public void PlayerLeft()
    {
        playerCount--;
    }
}
