using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ChessPieceType
{
    None = 0,
    Pawn = 1,
    Rook = 2,
    Knight = 3,
    Bishop = 4,
    Queen = 5,
    King = 6,
    Extra1 = 7,
    Extra2 = 8,
    Extra3 = 9,
    Extra4 = 10
}

public enum ChessPieceTeam
{
    White = 0,
    Black = 1
}

public enum SpecialMove
{
    None = 0,
    EnPassant,
    Castling,
    Promotion,
    Freeze,
    Drain
}

public enum RangedAttackType
{
    None = 0,
    Gun,
    Spawn,
    Convert,
    Sacrifice,
    Bombify

}

public enum OnCapture
{
    None = 0,
    Trap,
    SpawnNew,
    Bomb,
    Survive,

}


public class ChessPiece : MonoBehaviour
{
    public int ammunition, range, size;
    public string pieceName = "Unnamed Placeholder";
    public GameObject spawnedPiece = null;
    public ChessPieceType type;
    public ChessPieceTeam team; //White = 0, Black = 1;
    public RangedAttackType ranged = RangedAttackType.None;
    public OnCapture whenCaptured = OnCapture.None;
    public int currentX, currentY;
    public bool isTrap = false, canMove = true, moved = false, large = false;

    private Vector3 desiredPosition, desiredScale = Vector3.one;



    private void Update()
    {
        transform.position = Vector3.Lerp(transform.position, desiredPosition, Time.deltaTime * 10);
        transform.localScale = Vector3.Lerp(transform.localScale, desiredScale, Time.deltaTime * 10);
    }


    public virtual void SetPosition(Vector3 position, bool force = false)
    {
        desiredPosition = position;

        if(force)
        {
            transform.position = desiredPosition;
        }
    }
    public virtual void SetScale(Vector3 scale, bool force = false)
    {
        desiredScale = scale;

        if (force)
        {
            transform.localScale = desiredScale;
        }
    }


    public virtual List<Vector2Int> GetAvailableMoves(ref ChessPiece[,] board, int tileCountX, int tileCountY)
    {
        List<Vector2Int> r = new List<Vector2Int>();

        return r;
    }

    public virtual SpecialMove GetSpecialMove(ref ChessPiece[,] board, ref List<Vector2Int[]> moveList, ref List<Vector2Int> availableMoves)
    {
        return SpecialMove.None;
    }

    public virtual List<Vector2Int> GetRangedMoves(ref ChessPiece[,] board, int tileCountX, int tileCountY)
    {
        List<Vector2Int> r = new List<Vector2Int>();

        return r;
    }

}
