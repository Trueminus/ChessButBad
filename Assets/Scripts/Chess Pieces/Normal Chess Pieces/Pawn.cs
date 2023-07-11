using System.Collections.Generic;
using UnityEngine;

public class Pawn : ChessPiece
{
    private void Awake()
    {
        pieceName = "Pawn";
    }
    public override List<Vector2Int> GetAvailableMoves(ref ChessPiece[,] board, int tileCountX, int tileCountY)
    {
        List<Vector2Int> r = new List<Vector2Int>();

        int direction = (team == 0) ? 1 : -1;

      
        if(currentY + direction < tileCountY && currentY + direction >= 0)
        {
            //One space ahead
            if (board[currentX, currentY + direction] == null)
            {
                r.Add(new Vector2Int(currentX, currentY + direction));
            }

            //Two space ahead
            if(currentY + (direction*2) < tileCountY && currentY + (direction*2) >= 0)
            {
                if (board[currentX, currentY + (direction * 2)] == null)
                {
                    if (moved == false)
                    {
                        r.Add(new Vector2Int(currentX, currentY + (direction * 2)));
                    }
                }

                /*
                if (((team == ChessPieceTeam.White && currentY == 1) || (team == ChessPieceTeam.Black && currentY == (tileCountY-2))) && (board[currentX, currentY + (direction * 2)] == null))
                {
                    r.Add(new Vector2Int(currentX, currentY + (direction * 2)));
                }
                */
            }
            //One space diagonal
            if (currentX != tileCountX - 1)
            {
                if (board[currentX + 1, currentY + direction] != null && board[currentX + 1, currentY + direction].team != team)
                {
                    r.Add(new Vector2Int(currentX + 1, currentY + direction));
                }
            }
            if (currentX != 0)
            {
                if (board[currentX - 1, currentY + direction] != null && board[currentX - 1, currentY + direction].team != team)
                {
                    r.Add(new Vector2Int(currentX - 1, currentY + direction));
                }
            }
        }
        return r;
    }

    public override SpecialMove GetSpecialMove(ref ChessPiece[,] board, ref List<Vector2Int[]> moveList, ref List<Vector2Int> availableMoves)
    {


        int direction = (team == 0) ? 1 : -1;
        //En Passant
        if(moveList.Count > 0)
        {
            Vector2Int[] lastMove = moveList[moveList.Count - 1];

            if(board[lastMove[1].x, lastMove[1].y].type == ChessPieceType.Pawn) // If the last moved piece was a pawn
            {
                if(Mathf.Abs(lastMove[0].y - lastMove[1].y) == 2) // If it moved two spaces
                {
                    if(board[lastMove[1].x, lastMove[1].y].team != team) // Make sure it was the other team
                    {
                        if(lastMove[1].y == currentY) //Are they on the same row as us
                        {
                            if(lastMove[1].x == currentX - 1) //Adjacent to our left
                            {
                                availableMoves.Add(new Vector2Int(currentX - 1, currentY + direction));
                                return SpecialMove.EnPassant;
                            }
                            if (lastMove[1].x == currentX + 1) //Adjaceent to our right
                            {
                                availableMoves.Add(new Vector2Int(currentX + 1, currentY + direction));
                                return SpecialMove.EnPassant;
                            }

                            
                        }
                    }
                }
            }
        }

        //Promotion
        if((team == ChessPieceTeam.White && currentY == (board.GetLength(0)-2)) || (team == ChessPieceTeam.Black && currentY == 1))
        {

            return SpecialMove.Promotion;
        }

        return SpecialMove.None;
    }
}
