using System.Collections.Generic;
using UnityEngine;

public class ArcherTower : ChessPiece
{
    private void Awake()
    {
        range = 5;
        ranged = RangedAttackType.Gun;
        ammunition = 4;
    }

    public override List<Vector2Int> GetRangedMoves(ref ChessPiece[,] board, int tileCountX, int tileCountY)
    {
        List<Vector2Int> r = new List<Vector2Int>();

        int direction = (team == ChessPieceTeam.White) ? 1 : -1;

        if (ammunition > 0)
        {
            // 5 Spaces total
            //currentX currentY+Range
            //currentX+-1 currentY+Range
            //currentX currentY+Range+1
            //currentX currentY+Range-1

            if(board[currentX, currentY+range] != null)
            {
                if(board[currentX, currentY+range].team != team)
                {
                    r.Add(new Vector2Int(currentX, currentY + range));
                }
            }
            if (board[currentX + 1, currentY + range] != null)
            {
                if (board[currentX + 1, currentY + range].team != team)
                {
                    r.Add(new Vector2Int(currentX + 1, currentY + range));
                }
            }
            if (board[currentX - 1, currentY + range] != null)
            {
                if (board[currentX - 1, currentY + range].team != team)
                {
                    r.Add(new Vector2Int(currentX - 1, currentY + range));
                }
            }
            if (board[currentX, currentY + range + 1] != null)
            {
                if (board[currentX, currentY + range + 1].team != team)
                {
                    r.Add(new Vector2Int(currentX, currentY + range + 1));
                }
            }
            if (board[currentX, currentY + range - 1] != null)
            {
                if (board[currentX, currentY + range - 1].team != team)
                {
                    r.Add(new Vector2Int(currentX, currentY + range - 1));
                }
            }

        }
        return r;
    }
}
