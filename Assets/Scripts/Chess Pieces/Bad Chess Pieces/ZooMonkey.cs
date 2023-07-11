using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZooMonkey : ChessPiece
{
    public override List<Vector2Int> GetAvailableMoves(ref ChessPiece[,] board, int tileCountX, int tileCountY)
    {
        List<Vector2Int> r = new List<Vector2Int>();

        //Vertical Move Up(y++)
        if (currentY + 1 < tileCountY)
        {
            if (board[currentX, currentY + 1] == null || board[currentX, currentY + 1].team != team)
            {
                r.Add(new Vector2Int(currentX, currentY + 1));
            }
        }
        //Vertical Move Down(y--)
        if (currentY - 1 >= 0)
        {
            if (board[currentX, currentY - 1] == null || board[currentX, currentY - 1].team != team)
            {
                r.Add(new Vector2Int(currentX, currentY - 1));
            }
        }
        //Horizontal Move Right(x++)
        if (currentX + 1 < tileCountX)
        {
            if (board[currentX + 1, currentY] == null || board[currentX + 1, currentY].team != team)
            {
                r.Add(new Vector2Int(currentX + 1, currentY));
            }
            //Diagonal Move Top Right(x++,y++)
            if (currentY + 1 < tileCountY)
            {
                if (board[currentX + 1, currentY + 1] == null || board[currentX + 1, currentY + 1].team != team)
                {
                    r.Add(new Vector2Int(currentX + 1, currentY + 1));
                }
            }
            //Diagonal Move Bottom Right(x++,y--)
            if (currentY - 1 >= 0)
            {
                if (board[currentX + 1, currentY - 1] == null || board[currentX + 1, currentY - 1].team != team)
                {
                    r.Add(new Vector2Int(currentX + 1, currentY - 1));
                }
            }
        }
        //Horizontal Move Left(x--)
        if (currentX - 1 >= 0)
        {
            if (board[currentX - 1, currentY] == null || board[currentX - 1, currentY].team != team)
            {
                r.Add(new Vector2Int(currentX - 1, currentY));
            }
            //Diagonal Move Top Left(x--,y++)
            if (currentY + 1 < tileCountY)
            {
                if (board[currentX - 1, currentY + 1] == null || board[currentX - 1, currentY + 1].team != team)
                {
                    r.Add(new Vector2Int(currentX - 1, currentY + 1));
                }
            }
            //Diagonal Move Bottom Left(x--,y--)
            if (currentY - 1 >= 0)
            {
                if (board[currentX - 1, currentY - 1] == null || board[currentX - 1, currentY - 1].team != team)
                {
                    r.Add(new Vector2Int(currentX - 1, currentY - 1));
                }
            }
        }

        return r;
    }
    public override List<Vector2Int> GetRangedMoves(ref ChessPiece[,] board, int tileCountX, int tileCountY)
    {
        bool xp1, xp2, xm1, xm2, yp1, yp2, ym1, ym2;

        xp1 = (currentX + 1 < tileCountX);
        xp2 = (currentX + 2 < tileCountX);

        xm1 = (currentX - 1 > 0);
        xm2 = (currentX - 2 > 0);

        yp1 = (currentY + 1 < tileCountY);
        yp2 = (currentY + 2 < tileCountY);

        ym1 = (currentY - 1 > 0);
        ym2 = (currentY - 2 > 0);

        List<Vector2Int> r = new List<Vector2Int>();
        //Y+2
        if (yp2)
        {
            if(board[currentX,currentY + 2 ] != null)
            {
                r.Add(new Vector2Int(currentX, currentY + 2));
            }
            if(xp1)
            {
                if(board[currentX + 1, currentY + 2] != null)
                {
                    r.Add(new Vector2Int(currentX + 1, currentY + 2));
                }
            }
            if(xp2)
            {
                if (board[currentX + 2, currentY + 2] != null)
                {
                    r.Add(new Vector2Int(currentX + 2, currentY + 2));
                }
            }
            if(xm1)
            {
                if (board[currentX - 1, currentY + 2] != null)
                {
                    r.Add(new Vector2Int(currentX - 1, currentY + 2));
                }
            }
            if(xm2)
            {
                if (board[currentX - 2, currentY + 2] != null)
                {
                    r.Add(new Vector2Int(currentX - 2, currentY + 2));
                }
            }
        }
        //Y+1
        if(yp1)
        {
            if(xp2)
            {
                if(board[currentX + 2, currentY + 1] != null)
                {
                    r.Add(new Vector2Int(currentX + 2, currentY + 1));
                }
            }
            if(xm2)
            {
                if (board[currentX - 2, currentY + 1] != null)
                {
                    r.Add(new Vector2Int(currentX - 2, currentY + 1));
                }
            }
        }
        //Y
        if(xp2)
        {
            if (board[currentX + 2, currentY] != null)
            {
                r.Add(new Vector2Int(currentX + 2, currentY));
            }
        }
        if(xm2)
        {
            if (board[currentX - 2, currentY] != null)
            {
                r.Add(new Vector2Int(currentX - 2, currentY));
            }
        }
        //Y-2
        if(ym2)
        {
            if (board[currentX, currentY - 2] != null)
            {
                r.Add(new Vector2Int(currentX, currentY - 2));
            }
            if (xp1)
            {
                if (board[currentX + 1, currentY - 2] != null)
                {
                    r.Add(new Vector2Int(currentX + 1, currentY - 2));
                }
            }
            if (xp2)
            {
                if (board[currentX + 2, currentY - 2] != null)
                {
                    r.Add(new Vector2Int(currentX + 2, currentY - 2));
                }
            }
            if (xm1)
            {
                if (board[currentX - 1, currentY - 2] != null)
                {
                    r.Add(new Vector2Int(currentX - 1, currentY - 2));
                }
            }
            if (xm2)
            {
                if (board[currentX - 2, currentY - 2] != null)
                {
                    r.Add(new Vector2Int(currentX - 2, currentY - 2));
                }
            }
        }
        //Y-1
        if(ym1)
        {
            if (xp2)
            {
                if (board[currentX + 2, currentY - 1] != null)
                {
                    r.Add(new Vector2Int(currentX + 2, currentY - 1));
                }
            }
            if (xm2)
            {
                if (board[currentX - 2, currentY - 1] != null)
                {
                    r.Add(new Vector2Int(currentX - 2, currentY - 1));
                }
            }
        }

        return r;
    }
}
