using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vampire : Queen
{
    public override SpecialMove GetSpecialMove(ref ChessPiece[,] board, ref List<Vector2Int[]> moveList, ref List<Vector2Int> availableMoves)
    {
        return base.GetSpecialMove(ref board, ref moveList, ref availableMoves);
    }
}
