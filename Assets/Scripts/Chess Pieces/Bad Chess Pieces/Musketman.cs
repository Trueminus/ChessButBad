using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Musketman : Pawn
{
    public override SpecialMove GetSpecialMove(ref ChessPiece[,] board, ref List<Vector2Int[]> moveList, ref List<Vector2Int> availableMoves)
    {
        return SpecialMove.None;
    }

    public override List<Vector2Int> GetRangedMoves(ref ChessPiece[,] board, int tileCountX, int tileCountY)
    {
        List<Vector2Int> r = new List<Vector2Int>();

        int direction = (team == ChessPieceTeam.White) ? 1 : -1;

        if(currentY + (direction * 2) < tileCountY && currentY + (direction * 2) > 0)
        {
            if(board[currentX, currentY + (direction * 2)] != null)
            {
                r.Add(new Vector2Int(currentX, currentY + (direction * 2)));
            }
        }

        if (currentY + (direction * 3) < tileCountY && currentY + (direction * 3) > 0)
        {
            if (board[currentX, currentY + (direction * 3)] != null)
            {
                r.Add(new Vector2Int(currentX, currentY + (direction * 3)));
            }
        }

        return base.GetRangedMoves(ref board, tileCountX, tileCountY);
    }
}
