using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicChessX2 : ChessPieceModule
{
    public override bool FillBoard(ref ChessPiece[,] board, int tileCountX, int tileCountY, Vector3 pieceScale)
    {
        if (tileCountX != 16 || tileCountY != 16)
        {
            return base.FillBoard(ref board, tileCountX, tileCountY, pieceScale);
        }
        //White Team
        board[4, 0] = SpawnPiece(ChessPieceType.Rook, ChessPieceTeam.White, pieceScale);
        board[5, 0] = SpawnPiece(ChessPieceType.Knight, ChessPieceTeam.White, pieceScale);
        board[6, 0] = SpawnPiece(ChessPieceType.Bishop, ChessPieceTeam.White, pieceScale);
        board[7, 0] = SpawnPiece(ChessPieceType.Queen, ChessPieceTeam.White, pieceScale);
        board[8, 0] = SpawnPiece(ChessPieceType.King, ChessPieceTeam.White, pieceScale);
        board[9, 0] = SpawnPiece(ChessPieceType.Bishop, ChessPieceTeam.White, pieceScale);
        board[10, 0] = SpawnPiece(ChessPieceType.Knight, ChessPieceTeam.White, pieceScale);
        board[11, 0] = SpawnPiece(ChessPieceType.Rook, ChessPieceTeam.White, pieceScale);
        for (int i = 0; i < tileCountX; i++)
        {
            board[i, 1] = SpawnPiece(ChessPieceType.Pawn, ChessPieceTeam.White, pieceScale);
        }

        //Black Team
        board[4, 15] = SpawnPiece(ChessPieceType.Rook, ChessPieceTeam.Black, pieceScale);
        board[5, 15] = SpawnPiece(ChessPieceType.Knight, ChessPieceTeam.Black, pieceScale);
        board[6, 15] = SpawnPiece(ChessPieceType.Bishop, ChessPieceTeam.Black, pieceScale);
        board[7, 15] = SpawnPiece(ChessPieceType.Queen, ChessPieceTeam.Black, pieceScale);
        board[8, 15] = SpawnPiece(ChessPieceType.King, ChessPieceTeam.Black, pieceScale);
        board[9, 15] = SpawnPiece(ChessPieceType.Bishop, ChessPieceTeam.Black, pieceScale);
        board[10, 15] = SpawnPiece(ChessPieceType.Knight, ChessPieceTeam.Black, pieceScale);
        board[11, 15] = SpawnPiece(ChessPieceType.Rook, ChessPieceTeam.Black, pieceScale);
        for (int i = 0; i < tileCountX; i++)
        {
            board[i, 14] = SpawnPiece(ChessPieceType.Pawn, ChessPieceTeam.Black, pieceScale);
        }







        return true;
    }
}
