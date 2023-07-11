using System.Collections.Generic;
using UnityEngine;

public class PawnOnRollerSkates : ChessPiece
{
    public override List<Vector2Int> GetAvailableMoves(ref ChessPiece[,] board, int tileCountX, int tileCountY)
    {
        List<Vector2Int> r = new List<Vector2Int>();

        int direction = (team == 0) ? 1 : -1;

        if (currentY + direction < tileCountY && currentY + direction >= 0)
        {
            //Vertical Move Up
            if(team == ChessPieceTeam.White)
            {
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
            }
            //Vertical Move Down
            if(team == ChessPieceTeam.Black)
            {
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
            }
            //Diagonal Capture
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

}
