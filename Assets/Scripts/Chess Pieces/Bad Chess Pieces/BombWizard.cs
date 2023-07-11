using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombWizard : Bishop
{
    public override List<Vector2Int> GetRangedMoves(ref ChessPiece[,] board, int tileCountX, int tileCountY)
    {
        List<Vector2Int> r = new List<Vector2Int>();

        if (currentY + 1 < tileCountY)
        {
            if (board[currentX, currentY + 1] != null)
            {
                r.Add(new Vector2Int(currentX, currentY + 1));
            }
        }
        if (currentY + 2 < tileCountY)
        {
            if (board[currentX, currentY + 2] != null)
            {
                r.Add(new Vector2Int(currentX, currentY + 2));
            }
        }
        if (currentY - 1 > 0)
        {
            if (board[currentX, currentY - 1] != null)
            {
                r.Add(new Vector2Int(currentX, currentY - 1));
            }
        }
        if (currentY - 2 > 0)
        {
            if (board[currentX, currentY - 2] != null)
            {
                r.Add(new Vector2Int(currentX, currentY - 2));
            }
        }
        if (currentX + 1 < tileCountX)
        {
            if (board[currentX + 1, currentY] != null)
            {
                r.Add(new Vector2Int(currentX + 1, currentY));
            }
        }
        if (currentX + 2 < tileCountX)
        {
            if (board[currentX + 2, currentY] != null)
            {
                r.Add(new Vector2Int(currentX + 2, currentY));
            }
        }
        if (currentX - 1 > 0)
        {
            if (board[currentX - 1, currentY] != null)
            {
                r.Add(new Vector2Int(currentX - 1, currentY));
            }
        }
        if (currentY - 2 > 0)
        {
            if (board[currentX - 2, currentY] != null)
            {
                r.Add(new Vector2Int(currentX - 2, currentY));
            }
        }

        return r;
    }
}
