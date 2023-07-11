using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Builder : ChessPiece
{
    private void Awake()
    {
        ranged = RangedAttackType.Spawn;
    }

    public override List<Vector2Int> GetAvailableMoves(ref ChessPiece[,] board, int tileCountX, int tileCountY)
    {
        List<Vector2Int> r = new List<Vector2Int>();

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

    public override List<Vector2Int> GetRangedMoves(ref ChessPiece[,] board, int tileCountX, int tileCountY)
    {
        List<Vector2Int> r = new List<Vector2Int>();

        //Ahead(x, Y++)

        if(currentY + 1 < tileCountY)
        {
            if (board[currentX, currentY + 1] == null)
            {
                r.Add(new Vector2Int(currentX, currentY + 1));
            }
        }

        //Left(X--,Y)
        if(currentX != 0)
        {
            if (board[currentX - 1, currentY] == null)
            {
                r.Add(new Vector2Int(currentX - 1, currentY));
            }
        }

        //Right(X++,Y)
        if(currentX+1 < tileCountX)
        {
            if (board[currentX + 1, currentY] == null)
            {
                r.Add(new Vector2Int(currentX + 1, currentY));
            }
        }

        //Behind(X, Y--)
        if(currentY != 0)
        {
            if (board[currentX, currentY - 1] == null)
            {
                r.Add(new Vector2Int(currentX, currentY - 1));
            }
        }

        return r;
    }




}
