using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Landmine : Pawn
{
    public override SpecialMove GetSpecialMove(ref ChessPiece[,] board, ref List<Vector2Int[]> moveList, ref List<Vector2Int> availableMoves)
    {
        return SpecialMove.None;
    }
}
