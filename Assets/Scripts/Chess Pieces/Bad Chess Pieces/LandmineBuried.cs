using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandmineBuried : Pawn
{
    public Material blind;

    private void Update()
    {
        
    
    
        GetComponent<MeshRenderer>().material = blind;
    }


    public override SpecialMove GetSpecialMove(ref ChessPiece[,] board, ref List<Vector2Int[]> moveList, ref List<Vector2Int> availableMoves)
    {
        return SpecialMove.None;
    }
}
