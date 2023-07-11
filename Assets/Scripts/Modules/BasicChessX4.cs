using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicChessX4 : ChessPieceModule
{
    public override bool FillBoard(ref ChessPiece[,] board, int tileCountX, int tileCountY, Vector3 pieceScale)
    {
        if (tileCountX < 32 || tileCountY < 32)
        {
            return base.FillBoard(ref board, tileCountX, tileCountY, pieceScale);
        }
        //White Team
        board[12, 0] = SpawnPiece(ChessPieceType.Rook, ChessPieceTeam.White, pieceScale);
        board[13, 0] = SpawnPiece(ChessPieceType.Knight, ChessPieceTeam.White, pieceScale);
        board[14, 0] = SpawnPiece(ChessPieceType.Bishop, ChessPieceTeam.White, pieceScale);
        board[15, 0] = SpawnPiece(ChessPieceType.Queen, ChessPieceTeam.White, pieceScale);
        board[16, 0] = SpawnPiece(ChessPieceType.King, ChessPieceTeam.White, pieceScale);
        board[17, 0] = SpawnPiece(ChessPieceType.Bishop, ChessPieceTeam.White, pieceScale);
        board[18, 0] = SpawnPiece(ChessPieceType.Knight, ChessPieceTeam.White, pieceScale);
        board[19, 0] = SpawnPiece(ChessPieceType.Rook, ChessPieceTeam.White, pieceScale);
        for (int i = 0; i < tileCountX; i++)
        {
            board[i, 1] = SpawnPiece(ChessPieceType.Pawn, ChessPieceTeam.White, pieceScale);
        }

        //Black Team
        board[12, 31] = SpawnPiece(ChessPieceType.Rook, ChessPieceTeam.Black, pieceScale);
        board[13, 31] = SpawnPiece(ChessPieceType.Knight, ChessPieceTeam.Black, pieceScale);
        board[14, 31] = SpawnPiece(ChessPieceType.Bishop, ChessPieceTeam.Black, pieceScale);
        board[15, 31] = SpawnPiece(ChessPieceType.Queen, ChessPieceTeam.Black, pieceScale);
        board[16, 31] = SpawnPiece(ChessPieceType.King, ChessPieceTeam.Black, pieceScale);
        board[17, 31] = SpawnPiece(ChessPieceType.Bishop, ChessPieceTeam.Black, pieceScale);
        board[18, 31] = SpawnPiece(ChessPieceType.Knight, ChessPieceTeam.Black, pieceScale);
        board[19, 31] = SpawnPiece(ChessPieceType.Rook, ChessPieceTeam.Black, pieceScale);
        for (int i = 0; i < tileCountX; i++)
        {
            board[i, 30] = SpawnPiece(ChessPieceType.Pawn, ChessPieceTeam.Black, pieceScale);
        }







        return true;
    }
}
