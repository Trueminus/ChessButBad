using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CustomGameMenu : MonoBehaviour
{
    public ChessPieceModule[] modules;
    public static int gameSize = 1;
    public static int gameId = 0;
    public void SetGameSize(int size)
    {
        gameSize = size;
    }
    public void AssignGame(int index)
    {
        GameManager.Instance.module = modules[index];
    }
    public void LoadScene()
    {
        GameManager.Instance.Load(gameSize);
    }

    public void SetGameId(int id)
    {
        gameId = id;
    }
    
    public void LoadSceneMulti()
    {
        AssignGame(gameId);
        GameManager.Instance.Load(gameSize);
    }


    public void SendReady(int isReady)
    {
        Debug.Log($"Game Id: {gameId} Game Size: {gameSize}");

        NetGameReady ng = new NetGameReady();

        ng.gameId = gameId;
        ng.gameSize = gameSize;
        ng.isReady = isReady;

        Client.Instance.SendToServer(ng);
    }
}
