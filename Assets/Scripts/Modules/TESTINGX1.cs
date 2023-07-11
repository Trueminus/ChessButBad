using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TESTINGX1 : ChessPieceModule
{
    public override bool FillBoard(ref ChessPiece[,] board, int tileCountX, int tileCountY, Vector3 pieceScale)
    {
        if (tileCountX > 8 || tileCountY > 8)
        {
            return base.FillBoard(ref board, tileCountX, tileCountY, pieceScale);
        }
        //White Team
        board[1, 3] = SpawnPiece(ChessPieceType.Extra1, ChessPieceTeam.White, pieceScale);

        board[3, 3] = SpawnPiece(ChessPieceType.Extra1, ChessPieceTeam.White, pieceScale);
        board[4, 3] = SpawnPiece(ChessPieceType.Extra1, ChessPieceTeam.White, pieceScale);

        board[6, 3] = SpawnPiece(ChessPieceType.Extra1, ChessPieceTeam.White, pieceScale);

        //Black Team
        board[0, 7] = SpawnPiece(ChessPieceType.Rook, ChessPieceTeam.Black, pieceScale);
        board[1, 7] = SpawnPiece(ChessPieceType.Knight, ChessPieceTeam.Black, pieceScale);
        board[2, 7] = SpawnPiece(ChessPieceType.Bishop, ChessPieceTeam.Black, pieceScale);
        board[3, 7] = SpawnPiece(ChessPieceType.Queen, ChessPieceTeam.Black, pieceScale);
        board[4, 7] = SpawnPiece(ChessPieceType.King, ChessPieceTeam.Black, pieceScale);
        board[5, 7] = SpawnPiece(ChessPieceType.Bishop, ChessPieceTeam.Black, pieceScale);
        board[6, 7] = SpawnPiece(ChessPieceType.Knight, ChessPieceTeam.Black, pieceScale);
        board[7, 7] = SpawnPiece(ChessPieceType.Rook, ChessPieceTeam.Black, pieceScale);
        for (int i = 0; i < tileCountX; i++)
        {
            board[i, 6] = SpawnPiece(ChessPieceType.Pawn, ChessPieceTeam.Black, pieceScale);
        }







        return true;
    }
}
