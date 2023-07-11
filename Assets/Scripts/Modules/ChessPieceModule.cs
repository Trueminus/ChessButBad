using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChessPieceModule : MonoBehaviour
{
    [SerializeField] public GameObject[] prefabs;
    [SerializeField] public Material[] teamMaterials;
    public GameObject board;

    public virtual bool FillBoard(ref ChessPiece[,] board, int tileCountX, int tileCountY, Vector3 pieceScale)
    {
        Debug.LogError("No Module Assigned OR Incompatable Module Assigned!");
        return false;
    }

    public virtual ChessPiece SpawnPiece(ChessPieceType type, ChessPieceTeam team, Vector3 scale)
    {
        board = GameObject.FindGameObjectWithTag("Board");

        if(board == null)
        {
            Debug.LogError("Somehow there's no board!");
            return null;
        }

        ChessPiece piece = Instantiate(prefabs[(int)type - 1], board.transform).GetComponent<ChessPiece>();

        piece.SetScale(scale, true);

        if (team == ChessPieceTeam.Black)
        {
            piece.transform.Rotate(0f, 0f, 180f);
        }

        piece.type = type;
        piece.team = team;
        piece.GetComponent<MeshRenderer>().material = teamMaterials[(int)team];

        return piece;
    }


}
