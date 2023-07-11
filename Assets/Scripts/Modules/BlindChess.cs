using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlindChess : BasicChess
{
    public override ChessPiece SpawnPiece(ChessPieceType type, ChessPieceTeam team, Vector3 scale)
    {
        ChessPiece piece = Instantiate(prefabs[(int)type - 1], transform).GetComponent<ChessPiece>();

        piece.SetScale(scale, true);

        piece.type = type;
        piece.team = team;
        piece.GetComponent<MeshRenderer>().material = teamMaterials[0];

        return piece;
    }
}
