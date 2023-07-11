using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicChess : ChessPieceModule
{
    public override bool FillBoard(ref ChessPiece[,] board, int tileCountX, int tileCountY, Vector3 pieceScale)
    {
        if(tileCountX > 8 || tileCountY > 8)
        {
            return base.FillBoard(ref board, tileCountX, tileCountY, pieceScale);
        }
        //White Team
        board[0, 0] = SpawnPiece(ChessPieceType.Rook, ChessPieceTeam.White, pieceScale);
        board[1, 0] = SpawnPiece(ChessPieceType.Knight, ChessPieceTeam.White, pieceScale);
        board[2, 0] = SpawnPiece(ChessPieceType.Bishop, ChessPieceTeam.White, pieceScale);
        board[3, 0] = SpawnPiece(ChessPieceType.Queen, ChessPieceTeam.White, pieceScale);
        board[4, 0] = SpawnPiece(ChessPieceType.King, ChessPieceTeam.White, pieceScale);
        board[5, 0] = SpawnPiece(ChessPieceType.Bishop, ChessPieceTeam.White, pieceScale);
        board[6, 0] = SpawnPiece(ChessPieceType.Knight, ChessPieceTeam.White, pieceScale);
        board[7, 0] = SpawnPiece(ChessPieceType.Rook, ChessPieceTeam.White, pieceScale);
        for (int i = 0; i < tileCountX; i++)
        {
            board[i, 1] = SpawnPiece(ChessPieceType.Pawn, ChessPieceTeam.White, pieceScale);
        }

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
