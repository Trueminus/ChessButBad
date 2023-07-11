using System.Collections.Generic;
using UnityEngine;

public class Queen : ChessPiece
{
    private void Awake()
    {
        pieceName = "Queen";
    }
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

        //Diagonal Top Right(x++,y++)
        for (int x = currentX + 1, y = currentY + 1; x < tileCountX && y < tileCountY; x++, y++)
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
