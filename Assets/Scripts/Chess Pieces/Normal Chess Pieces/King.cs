using System.Collections.Generic;
using UnityEngine;

public class King : ChessPiece
{
    private void Awake()
    {
        pieceName = "King";
    }
    public override List<Vector2Int> GetAvailableMoves(ref ChessPiece[,] board, int tileCountX, int tileCountY)
    {
        List<Vector2Int> r = new List<Vector2Int>();

        //Vertical Move Up(y++)
        if(currentY + 1 < tileCountY)
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
        if(currentX + 1 < tileCountX)
        {
            if (board[currentX + 1, currentY] == null || board[currentX + 1, currentY].team != team)
            {
                r.Add(new Vector2Int(currentX + 1, currentY));
            }
            //Diagonal Move Top Right(x++,y++)
            if(currentY + 1 < tileCountY)
            {
                if (board[currentX + 1, currentY + 1] == null || board[currentX + 1, currentY + 1].team != team)
                {
                    r.Add(new Vector2Int(currentX + 1, currentY + 1));
                }
            }
            //Diagonal Move Bottom Right(x++,y--)
            if(currentY - 1 >= 0)
            {
                if (board[currentX + 1, currentY - 1] == null || board[currentX + 1, currentY - 1].team != team)
                {
                    r.Add(new Vector2Int(currentX + 1, currentY - 1));
                }
            }
        }
        //Horizontal Move Left(x--)
        if(currentX - 1 >= 0)
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

    public override SpecialMove GetSpecialMove(ref ChessPiece[,] board, ref List<Vector2Int[]> moveList, ref List<Vector2Int> availableMoves)
    {
        SpecialMove r = SpecialMove.None;

        var kingMove = moveList.Find(m => m[0].x == 4 && m[0].y == ((team == ChessPieceTeam.White) ? 0 : 7));
        var leftRook = moveList.Find(m => m[0].x == 0 && m[0].y == ((team == ChessPieceTeam.White) ? 0 : 7));
        var rightRook = moveList.Find(m => m[0].x == 7 && m[0].y == ((team == ChessPieceTeam.White) ? 0 : 7));

        if(kingMove == null && currentX == 4)
        {
            //White Team
            if(team == ChessPieceTeam.White)
            {
                //Left Rook
                if(leftRook == null)
                {
                    if(board[0,0].type == ChessPieceType.Rook && board[0,0].team == ChessPieceTeam.White)
                    {
                        if(board[3,0] == null)
                        {
                            if(board[2,0] == null)
                            {
                                if(board[1,0] == null)
                                {
                                    availableMoves.Add(new Vector2Int(2, 0));
                                    r = SpecialMove.Castling;
                                }
                            }
                        }
                    }
                }
                //Right Rook
                if (rightRook == null)
                {
                    if (board[7, 0].type == ChessPieceType.Rook && board[7, 0].team == ChessPieceTeam.White)
                    {
                        if (board[6, 0] == null)
                        {
                            if (board[5, 0] == null)
                            {
                                availableMoves.Add(new Vector2Int(6, 0));
                                r = SpecialMove.Castling;
                            }
                        }
                    }
                }
            }
            //Black Team
            if (team == ChessPieceTeam.Black)
            {
                //Left Rook
                if (leftRook == null)
                {
                    if (board[0, 7].type == ChessPieceType.Rook && board[0, 7].team == ChessPieceTeam.Black)
                    {
                        if (board[3, 7] == null)
                        {
                            if (board[2, 7] == null)
                            {
                                if (board[1, 7] == null)
                                {
                                    availableMoves.Add(new Vector2Int(2, 7));
                                    r = SpecialMove.Castling;
                                }
                            }
                        }
                    }
                }
                //Right Rook
                if (rightRook == null)
                {
                    if (board[7, 7].type == ChessPieceType.Rook && board[7, 7].team == ChessPieceTeam.Black)
                    {
                        if (board[6, 7] == null)
                        {
                            if (board[5, 7] == null)
                            {
                                availableMoves.Add(new Vector2Int(6, 7));
                                r = SpecialMove.Castling;
                            }
                        }
                    }
                }
            }
        }
        return r;
    }
}
