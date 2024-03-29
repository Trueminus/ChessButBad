using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReversePawn : ChessPiece
{
    public override List<Vector2Int> GetAvailableMoves(ref ChessPiece[,] board, int tileCountX, int tileCountY)
    {
        List<Vector2Int> r = new List<Vector2Int>();

        int direction = (team == 0) ? 1 : -1;


        if (currentY + direction < tileCountY && currentY + direction >= 0)
        {
            //One space ahead
            if (board[currentX, currentY + direction] == null)
            {
                r.Add(new Vector2Int(currentX, currentY + direction));
            }

            //Two space ahead
            if (board[currentX, currentY + direction] == null)
            {
                if (moved == false)
                {
                    r.Add(new Vector2Int(currentX, currentY + (direction * 2)));
                }
            }
            //One space diagonal
            if (currentX != tileCountX - 1)
            {
                if (board[currentX + 1, currentY + direction] != null && board[currentX + 1, currentY + direction].team != team)
                {
                    r.Add(new Vector2Int(currentX + 1, currentY + direction));
                }
            }
            if (currentX != 0)
            {
                if (board[currentX - 1, currentY + direction] != null && board[currentX - 1, currentY + direction].team != team)
                {
                    r.Add(new Vector2Int(currentX - 1, currentY + direction));
                }
            }
        }
        return r;
    }

    public override SpecialMove GetSpecialMove(ref ChessPiece[,] board, ref List<Vector2Int[]> moveList, ref List<Vector2Int> availableMoves)
    {
        return base.GetSpecialMove(ref board, ref moveList, ref availableMoves);
    }
}
