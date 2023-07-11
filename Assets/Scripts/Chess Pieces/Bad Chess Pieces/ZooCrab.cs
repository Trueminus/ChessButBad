using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZooCrab : ChessPiece
{
    public override List<Vector2Int> GetAvailableMoves(ref ChessPiece[,] board, int tileCountX, int tileCountY)
    {
        List<Vector2Int> r = new List<Vector2Int>();

        //Horizontal Move Right(x++)
        for (int i = currentX + 1; i < tileCountX; i++)
        {
            if (board[i, currentY] != null)
            {
                if (board[i, currentY].team != team)
                {
                    r.Add(new Vector2Int(i, currentY));
                }
                break;
            }
            else
            {
                r.Add(new Vector2Int(i, currentY));
            }
        }
        //Horizontal Move Left(x--)
        for (int i = currentX - 1; i >= 0; i--)
        {
            if (board[i, currentY] != null)
            {
                if (board[i, currentY].team != team)
                {
                    r.Add(new Vector2Int(i, currentY));
                }
                break;
            }
            else
            {
                r.Add(new Vector2Int(i, currentY));
            }
        }

        return r;
    }
}
