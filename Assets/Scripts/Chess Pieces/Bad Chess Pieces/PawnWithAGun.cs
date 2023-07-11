using System.Collections.Generic;
using UnityEngine;

public class PawnWithAGun : Pawn
{
    private void Awake()
    {
        ranged = RangedAttackType.Gun;
        ammunition = 50;
    }

    public override List<Vector2Int> GetRangedMoves(ref ChessPiece[,] board, int tileCountX, int tileCountY)
    {
        List<Vector2Int> r = new List<Vector2Int>();

        int direction = (team == ChessPieceTeam.White) ? 1 : -1;


        //if board[currentx, currenty+ (direction * 3) != null 
        //if board[See above^].team != team
        //add to r

        if(board[currentX, currentY + (direction * 3)] != null)
        {
            if(board[currentX,currentY + (direction * 3)].team != team)
            {
                r.Add(new Vector2Int(currentX, currentY + (direction * 3)));
            }
        }


        return r;
    }
}
