using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZooSnake : ChessPiece
{
    public override List<Vector2Int> GetAvailableMoves(ref ChessPiece[,] board, int tileCountX, int tileCountY)
    {
        List<Vector2Int> r = new List<Vector2Int>();

        //Vertical Move Up(y++)
        for (int i = currentY + 1; i < tileCountY; i++)
        {
            if (board[currentX, i] != null)
            {
                if (board[currentX, i].team != team)
                {
                    r.Add(new Vector2Int(currentX, i));
                }
                break;
            }
            else
            {
                r.Add(new Vector2Int(currentX, i));
            }
        }
        //Vertical Move Down(y--)
        for (int i = currentY - 1; i >= 0; i--)
        {
            if (board[currentX, i] != null)
            {
                if (board[currentX, i].team != team)
                {
                    r.Add(new Vector2Int(currentX, i));
                }
                break;
            }
            else
            {
                r.Add(new Vector2Int(currentX, i));
            }
        }

        return r;
    }
}
