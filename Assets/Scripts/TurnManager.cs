using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    public static TurnManager Instance { get; private set; }
    public ChessPieceTeam currentTurn = ChessPieceTeam.White;
    
    private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }


    public void SwitchTeams()
    {
        currentTurn = currentTurn == ChessPieceTeam.White ? ChessPieceTeam.Black : ChessPieceTeam.White;
    }
}
