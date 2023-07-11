using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monk : ChessPiece
{
    private void Awake()
    {
        ranged = RangedAttackType.Convert;
    }


    public override List<Vector2Int> GetAvailableMoves(ref ChessPiece[,] board, int tileCountX, int tileCountY)
    {
        List<Vector2Int> r = new List<Vector2Int>();


        //Vertical Move Up(y++)
        if (currentY + 1 < tileCountY)
        {
            if (board[currentX, currentY + 1] == null)
            {
                r.Add(new Vector2Int(currentX, currentY + 1));
            }
        }
        //Vertical Move Down(y--)
        if (currentY - 1 >= 0)
        {
            if (board[currentX, currentY - 1] == null)
            {
                r.Add(new Vector2Int(currentX, currentY - 1));
            }
        }
        //Horizontal Move Right(x++)
        if (currentX + 1 < tileCountX)
        {
            if (board[currentX + 1, currentY] == null)
            {
                r.Add(new Vector2Int(currentX + 1, currentY));
            }
            //Diagonal Move Top Right(x++,y++)
            if (currentY + 1 < tileCountY)
            {
                if (board[currentX + 1, currentY + 1] == null)
                {
                    r.Add(new Vector2Int(currentX + 1, currentY + 1));
                }
            }
            //Diagonal Move Bottom Right(x++,y--)
            if (currentY - 1 >= 0)
            {
                if (board[currentX + 1, currentY - 1] == null)
                {
                    r.Add(new Vector2Int(currentX + 1, currentY - 1));
                }
            }
        }
        //Horizontal Move Left(x--)
        if (currentX - 1 >= 0)
        {
            if (board[currentX - 1, currentY] == null)
            {
                r.Add(new Vector2Int(currentX - 1, currentY));
            }
            //Diagonal Move Top Left(x--,y++)
            if (currentY + 1 < tileCountY)
            {
                if (board[currentX - 1, currentY + 1] == null)
                {
                    r.Add(new Vector2Int(currentX - 1, currentY + 1));
                }
            }
            //Diagonal Move Bottom Left(x--,y--)
            if (currentY - 1 >= 0)
            {
                if (board[currentX - 1, currentY - 1] == null)
                {
                    r.Add(new Vector2Int(currentX - 1, currentY - 1));
                }
            }
        }



        return r;
    }


    public override List<Vector2Int> GetRangedMoves(ref ChessPiece[,] board, int tileCountX, int tileCountY)
    {
        List<Vector2Int> r = new List<Vector2Int>();


        //Vertical Convert Up(y++)
        if (currentY + 1 < tileCountY)
        {
            if (board[currentX, currentY + 1] != null)
            {
                if(board[currentX, currentY + 1].type == ChessPieceType.Pawn)
                {
                    r.Add(new Vector2Int(currentX, currentY + 1));
                }
            }
        }
        //Vertical Convert Down(y--)
        if (currentY - 1 >= 0)
        {
            if (board[currentX, currentY - 1] != null)
            {
                if (board[currentX, currentY - 1].type == ChessPieceType.Pawn)
                {
                    r.Add(new Vector2Int(currentX, currentY - 1));
                }
            }
        }
        //Horizontal Convert Right(x++)
        if (currentX + 1 < tileCountX)
        {
            if (board[currentX + 1, currentY] != null)
            {
                if (board[currentX + 1, currentY].type == ChessPieceType.Pawn)
                {
                    r.Add(new Vector2Int(currentX + 1, currentY));
                }
            }
            //Diagonal Convert Top Right(x++,y++)
            if (currentY + 1 < tileCountY)
            {
                if (board[currentX + 1, currentY + 1] != null)
                {
                    if (board[currentX + 1, currentY + 1].type == ChessPieceType.Pawn)
                    {
                        r.Add(new Vector2Int(currentX + 1, currentY + 1));
                    }
                }
            }
            //Diagonal Convert Bottom Right(x++,y--)
            if (currentY - 1 >= 0)
            {
                if (board[currentX + 1, currentY - 1] != null)
                {
                    if (board[currentX + 1, currentY - 1].type == ChessPieceType.Pawn)
                    {
                        r.Add(new Vector2Int(currentX + 1, currentY - 1));
                    }
                }
            }
        }
        //Horizontal Convert Left(x--)
        if (currentX - 1 >= 0)
        {
            if (board[currentX - 1, currentY] != null)
            {
                if (board[currentX - 1, currentY].type == ChessPieceType.Pawn)
                {
                    r.Add(new Vector2Int(currentX - 1, currentY));
                }
            }
            //Diagonal Convert Top Left(x--,y++)
            if (currentY + 1 < tileCountY)
            {
                if (board[currentX - 1, currentY + 1] != null)
                {
                    if (board[currentX - 1, currentY + 1].type == ChessPieceType.Pawn)
                    {
                        r.Add(new Vector2Int(currentX - 1, currentY + 1));
                    }
                }
            }
            //Diagonal Convert Bottom Left(x--,y--)
            if (currentY - 1 >= 0)
            {
                if (board[currentX - 1, currentY - 1] != null)
                {
                    if (board[currentX - 1, currentY - 1].type == ChessPieceType.Pawn)
                    {
                        r.Add(new Vector2Int(currentX - 1, currentY - 1));
                    }
                }
            }
        }

        return r;
    }


}
