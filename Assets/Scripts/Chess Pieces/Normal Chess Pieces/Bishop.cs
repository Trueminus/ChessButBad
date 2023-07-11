using System.Collections.Generic;
using UnityEngine;

public class Bishop : ChessPiece
{
    private void Awake()
    {
        pieceName = "Bishop";
    }
    public override List<Vector2Int> GetAvailableMoves(ref ChessPiece[,] board, int tileCountX, int tileCountY)
    {
        List<Vector2Int> r = new List<Vector2Int>();

        //Diagonal Top Right(x++,y++)
        for (int x = currentX+1,y = currentY+1; x < tileCountX && y < tileCountY; x++,y++)
        {
            if(board[x,y] == null)
            {
                r.Add(new Vector2Int(x, y));
            }
            else
            {
                if(board[x,y].team != team)
                {
                    r.Add(new Vector2Int(x, y));
                    
                }
                break;
            }
        }
        //Diagonal Top Left(x--,y++)
        for (int x = currentX - 1, y = currentY + 1; x >= 0 && y < tileCountY; x--, y++)
        {
            if (board[x, y] == null)
            {
                r.Add(new Vector2Int(x, y));
            }
            else
            {
                if (board[x, y].team != team)
                {
                    r.Add(new Vector2Int(x, y));

                }
                break;
            }
        }
        //Diagonal Bottom Right(x++,y--)
        for (int x = currentX + 1, y = currentY - 1; x < tileCountX && y >= 0; x++, y--)
        {
            if (board[x, y] == null)
            {
                r.Add(new Vector2Int(x, y));
            }
            else
            {
                if (board[x, y].team != team)
                {
                    r.Add(new Vector2Int(x, y));

                }
                break;
            }
        }
        //Diagonal Bottom Left(x--,y--)
        for (int x = currentX - 1, y = currentY - 1; x >= 0 && y >= 0; x--, y--)
        {
            if (board[x, y] == null)
            {
                r.Add(new Vector2Int(x, y));
            }
            else
            {
                if (board[x, y].team != team)
                {
                    r.Add(new Vector2Int(x, y));

                }
                break;
            }
        }

        return r;
    }
}
