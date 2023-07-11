using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StormtheCastle : ChessPieceModule
{
    public override bool FillBoard(ref ChessPiece[,] board, int tileCountX, int tileCountY, Vector3 pieceScale)
    {
        //Wall = PieceType.Queen
        //Archer Tower = PieceType.Extra1

        if (tileCountX != 16 || tileCountY != 16)
        {
            return base.FillBoard(ref board, tileCountX, tileCountY, pieceScale);
        }

        //White Team: Castle
        //x3 y0 Wall
        //x3-12 y1 Wall
        //x12 y0 Wall
        board[1, 0] = SpawnPiece(ChessPieceType.Extra1, ChessPieceTeam.White, pieceScale);
        board[2, 0] = SpawnPiece(ChessPieceType.Queen, ChessPieceTeam.White, pieceScale);
        board[3, 0] = SpawnPiece(ChessPieceType.Queen, ChessPieceTeam.White, pieceScale);
        board[4, 0] = SpawnPiece(ChessPieceType.Bishop, ChessPieceTeam.White, pieceScale);
        board[5, 0] = SpawnPiece(ChessPieceType.Bishop, ChessPieceTeam.White, pieceScale);
        board[8, 0] = SpawnPiece(ChessPieceType.King, ChessPieceTeam.White, pieceScale);
        board[10, 0] = SpawnPiece(ChessPieceType.Bishop, ChessPieceTeam.White, pieceScale);
        board[11, 0] = SpawnPiece(ChessPieceType.Bishop, ChessPieceTeam.White, pieceScale);
        board[12, 0] = SpawnPiece(ChessPieceType.Queen, ChessPieceTeam.White, pieceScale);
        board[13, 0] = SpawnPiece(ChessPieceType.Queen, ChessPieceTeam.White, pieceScale);
        board[14, 0] = SpawnPiece(ChessPieceType.Extra1, ChessPieceTeam.White, pieceScale);

        board[3, 1] = SpawnPiece(ChessPieceType.Queen, ChessPieceTeam.White, pieceScale);
        board[5, 1] = SpawnPiece(ChessPieceType.Extra1, ChessPieceTeam.White, pieceScale);
        board[10, 1] = SpawnPiece(ChessPieceType.Extra1, ChessPieceTeam.White, pieceScale);
        board[12, 1] = SpawnPiece(ChessPieceType.Queen, ChessPieceTeam.White, pieceScale);

        board[0, 2] = SpawnPiece(ChessPieceType.Knight, ChessPieceTeam.White, pieceScale);
        board[1, 2] = SpawnPiece(ChessPieceType.Knight, ChessPieceTeam.White, pieceScale);
        board[14, 2] = SpawnPiece(ChessPieceType.Knight, ChessPieceTeam.White, pieceScale);
        board[15, 2] = SpawnPiece(ChessPieceType.Knight, ChessPieceTeam.White, pieceScale);


        board[3, 2] = SpawnPiece(ChessPieceType.Extra1, ChessPieceTeam.White, pieceScale);
        board[4, 2] = SpawnPiece(ChessPieceType.Queen, ChessPieceTeam.White, pieceScale);
        board[5, 2] = SpawnPiece(ChessPieceType.Queen, ChessPieceTeam.White, pieceScale);
        board[6, 2] = SpawnPiece(ChessPieceType.Queen, ChessPieceTeam.White, pieceScale);
        board[7, 2] = SpawnPiece(ChessPieceType.Queen, ChessPieceTeam.White, pieceScale);
        board[8, 2] = SpawnPiece(ChessPieceType.Queen, ChessPieceTeam.White, pieceScale);
        board[9, 2] = SpawnPiece(ChessPieceType.Queen, ChessPieceTeam.White, pieceScale);
        board[10, 2] = SpawnPiece(ChessPieceType.Queen, ChessPieceTeam.White, pieceScale);
        board[11, 2] = SpawnPiece(ChessPieceType.Queen, ChessPieceTeam.White, pieceScale);
        board[12, 2] = SpawnPiece(ChessPieceType.Extra1, ChessPieceTeam.White, pieceScale);


        //Black Team: Peasants

        //y14 pawns
        //x0-4, 11-15 y15 pawns
        //
        //x8 y15 king
        //x4-11 y13 pawns
        for (int i = 0; i < tileCountX; i++)
        {
            board[i, 14] = SpawnPiece(ChessPieceType.Pawn, ChessPieceTeam.Black, pieceScale);
        }
        board[0, 15] = SpawnPiece(ChessPieceType.Pawn, ChessPieceTeam.Black, pieceScale);
        board[1, 15] = SpawnPiece(ChessPieceType.Pawn, ChessPieceTeam.Black, pieceScale);
        board[2, 15] = SpawnPiece(ChessPieceType.Pawn, ChessPieceTeam.Black, pieceScale);
        board[3, 15] = SpawnPiece(ChessPieceType.Pawn, ChessPieceTeam.Black, pieceScale);
        board[4, 15] = SpawnPiece(ChessPieceType.Pawn, ChessPieceTeam.Black, pieceScale);

        board[5, 15] = SpawnPiece(ChessPieceType.Knight, ChessPieceTeam.Black, pieceScale);
        board[6, 15] = SpawnPiece(ChessPieceType.Knight, ChessPieceTeam.Black, pieceScale);
        board[7, 15] = SpawnPiece(ChessPieceType.Pawn, ChessPieceTeam.Black, pieceScale);
        board[8, 15] = SpawnPiece(ChessPieceType.King, ChessPieceTeam.Black, pieceScale); 
        board[9, 15] = SpawnPiece(ChessPieceType.Knight, ChessPieceTeam.Black, pieceScale);
        board[10, 15] = SpawnPiece(ChessPieceType.Knight, ChessPieceTeam.Black, pieceScale);

        board[11, 15] = SpawnPiece(ChessPieceType.Pawn, ChessPieceTeam.Black, pieceScale);
        board[12, 15] = SpawnPiece(ChessPieceType.Pawn, ChessPieceTeam.Black, pieceScale);
        board[13, 15] = SpawnPiece(ChessPieceType.Pawn, ChessPieceTeam.Black, pieceScale);
        board[14, 15] = SpawnPiece(ChessPieceType.Pawn, ChessPieceTeam.Black, pieceScale);
        board[15, 15] = SpawnPiece(ChessPieceType.Pawn, ChessPieceTeam.Black, pieceScale);

        board[0, 13] = SpawnPiece(ChessPieceType.Knight, ChessPieceTeam.Black, pieceScale);
        board[2, 13] = SpawnPiece(ChessPieceType.Knight, ChessPieceTeam.Black, pieceScale);
        board[4, 13] = SpawnPiece(ChessPieceType.Pawn, ChessPieceTeam.Black, pieceScale);
        board[5, 13] = SpawnPiece(ChessPieceType.Pawn, ChessPieceTeam.Black, pieceScale);
        board[6, 13] = SpawnPiece(ChessPieceType.Pawn, ChessPieceTeam.Black, pieceScale);
        board[7, 13] = SpawnPiece(ChessPieceType.Pawn, ChessPieceTeam.Black, pieceScale);
        board[8, 13] = SpawnPiece(ChessPieceType.Pawn, ChessPieceTeam.Black, pieceScale);
        board[9, 13] = SpawnPiece(ChessPieceType.Pawn, ChessPieceTeam.Black, pieceScale);
        board[10, 13] = SpawnPiece(ChessPieceType.Pawn, ChessPieceTeam.Black, pieceScale);
        board[11, 13] = SpawnPiece(ChessPieceType.Pawn, ChessPieceTeam.Black, pieceScale);
        board[13, 13] = SpawnPiece(ChessPieceType.Knight, ChessPieceTeam.Black, pieceScale);
        board[15, 13] = SpawnPiece(ChessPieceType.Knight, ChessPieceTeam.Black, pieceScale);


        return true;
    }
}
