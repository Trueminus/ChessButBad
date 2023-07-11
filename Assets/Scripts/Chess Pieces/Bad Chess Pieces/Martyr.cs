using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Martyr : Pawn
{
    private void Awake()
    {
        System.Random rnd = new System.Random();
        this.ammunition = rnd.Next(1, ammunition);
    }

    public override SpecialMove GetSpecialMove(ref ChessPiece[,] board, ref List<Vector2Int[]> moveList, ref List<Vector2Int> availableMoves)
    {
        
        return SpecialMove.None;
    }
}
