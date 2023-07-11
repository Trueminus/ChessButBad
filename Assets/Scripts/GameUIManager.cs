using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameUIManager : MonoBehaviour
{
    public static GameUIManager Instance { set; get; }

    public Server server;
    public Client client;

    public GameObject gameStartButton;

    [SerializeField] private Animator menuAnimator;
    [SerializeField] private TMP_InputField addressInput;

    private void Awake()
    {
        Instance = this;
        RegisterEvents();
        //DontDestroyOnLoad(Instance);
    }



    public void OnLocalGameButton()
    {
        //Debug.Log("Ya sure pressed that local button");
        menuAnimator.SetTrigger("LocalGame");
    }

    public void OnOnlineGameButton()
    {
        //Debug.Log("Ya sure pressed that online button");
        menuAnimator.SetTrigger("OnlineMenu");
    }


    public void OnOnlineHostButton()
    {
        //Debug.Log("Ya sure pressed that online host button");
        GameManager.Instance.isMultiplayer = true;  
        server.Init(8007);
        client.Init("127.0.0.1", 8007);
        menuAnimator.SetTrigger("HostMenu");
    }

    public void OnOnlineConnectButton()
    {
        //Debug.Log("Ya sure pressed that online connect button");
        //Debug.Log("Trying to connect to: " + addressInput.text);
        GameManager.Instance.isMultiplayer = true;
        client.Init(addressInput.text, 8007);
    }

    public void OnOlineGameConnected()
    {
        menuAnimator.SetTrigger("OnlineGame");
    }

    public void OnOptionsButton()
    {
        //Debug.Log("Ya sure pressed that options button");
        menuAnimator.SetTrigger("OptionsMenu");
    }

    public void OnOnlineBackButton()
    {
        //Debug.Log("Ya sure pressed that online back button");
        menuAnimator.SetTrigger("StartMenu");
    }

    public void OnHostBackButton()
    {
        client.Shutdown();
        server.Shutdown();
        GameManager.Instance.isMultiplayer = false;
        menuAnimator.SetTrigger("OnlineMenu");
    }

    public void OnLocalGameBackButton()
    {
        menuAnimator.SetTrigger("StartMenu");
    }

    public void OnOnlineGameBackButton()
    {
        server.Shutdown();
        client.Shutdown();
        GameManager.Instance.isMultiplayer = false;
        menuAnimator.SetTrigger("StartMenu");
    }

    public void OnOptionsBackButton()
    {
        //Debug.Log("Ya sure pressed that options back button");
        menuAnimator.SetTrigger("StartMenu");
    }

    public void OnQuitButton()
    {
        //Debug.Log("Ya sure pressed that quit button");
        Application.Quit();
    }

    public void ActivateStartGameButton()
    {
        gameStartButton.SetActive(true);
    }
    public void OnStartGame()
    {
        Debug.Log("Start the game");
        server.Broadcast(new NetLaunchGame());
    }

    #region
    private void RegisterEvents()
    {
        NetUtility.C_START_GAME += OnStartGameClient;
    }
    private void UnregisterEvents()
    {
        NetUtility.C_START_GAME -= OnStartGameClient;
    }

    private void OnStartGameClient(NetMessage msg)
    {
        menuAnimator.SetTrigger("OnlineGame");
    }
    #endregion
}
