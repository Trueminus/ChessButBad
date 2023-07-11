using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public static GameManager Instance;

    public ChessPieceModule module;

    public bool assigned = false;

    public static int assignedTeam;

    public CameraManager camManager;
    public NetHandler netHandler;
    public CustomGameMenu customGameMenu;

    public bool isMultiplayer = false;

    private void Awake()
    {
        if(Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        assigned = true;
        DontDestroyOnLoad(gameObject);

    }

    public void Load(int gamesize)
    {

        SceneManager.LoadScene(gamesize);
        camManager.camerasSearching = true;

    }
}
