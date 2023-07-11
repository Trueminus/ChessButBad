using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Networking.Transport;
using UnityEngine;



public class Chessboard : MonoBehaviour
{
    [Header("Board Setup")]
    [SerializeField] private Material tileMaterial;
    [SerializeField] private int TILE_COUNT_X = 8;
    [SerializeField] private int TILE_COUNT_Y = 8;
    [SerializeField] private float tileSize = 1.0f;
    [SerializeField] private float yOffset = 0.2f;
    [SerializeField] private Vector3 boardCenter = Vector3.zero;
    [SerializeField] private float deadSize = 0.3f, deadSpacing, dragOffset = 1.5f;
    [SerializeField] private GameObject victoryScreen;
    [SerializeField] private Vector3 pieceScale = new Vector3(1,1,1);
    [Header("MODULE: DO NOT LEAVE BLANK")]
    [SerializeField] private ChessPieceModule module;

    [Header("Prefabs & Materials")]
    [SerializeField] private GameObject[] prefabs;
    [SerializeField] private Material[] teamMaterials;

    public static ChessPiece[,] chessPieces;
    private ChessPiece currentlySelected;
    private List<Vector2Int> availableMoves = new List<Vector2Int>();
    private List<Vector2Int> availableMovesRanged = new List<Vector2Int>();

    private List<ChessPiece> deadWhite = new List<ChessPiece>();
    private List<ChessPiece> deadBlack = new List<ChessPiece>();
    
    private SpecialMove specialMove;
    private List<Vector2Int[]> moveList = new List<Vector2Int[]>();


    private GameObject[,] tiles;
    private Camera currentCamera;
    private Vector2Int currentHover;

    private Vector3 bounds;
    private bool isWhiteTurn;

    //For multiplayer
    //private int playerCount = -1;
    private int currentTeam = -1;
     

    private void Awake()
    {
        if(GameManager.Instance.module != null)
        {
            module = GameManager.Instance.module;
        }

        if(!GameManager.Instance.isMultiplayer)
        {
            GameManager.Instance.netHandler.currentTeam = 0;
        }


        GenerateBoard(tileSize, TILE_COUNT_X, TILE_COUNT_Y);

        FillBoard();

        PositionAllPieces();

        isWhiteTurn = true;

        RegisterEvents();
    }
    private void Update()
    {
        if(!currentCamera)
        {
            currentCamera = Camera.main;
            return;
        }

        RaycastHit info;
        Ray ray = currentCamera.ScreenPointToRay(Input.mousePosition);
        if(Physics.Raycast(ray, out info, 100, LayerMask.GetMask("Tile","Hover","Highlight","HighlightRanged")))
        {
            Vector2Int hitPosition = LookUpTileIndex(info.transform.gameObject);

            if (currentHover == -Vector2Int.one) //No hover yet
            {
                currentHover = hitPosition;
                tiles[hitPosition.x, hitPosition.y].layer = LayerMask.NameToLayer("Hover");
            }

            if (currentHover != hitPosition) //New hover
            {
                tiles[currentHover.x, currentHover.y].layer = (ContainsValidMove(ref availableMoves, currentHover)) ? LayerMask.NameToLayer("Highlight") : LayerMask.NameToLayer("Tile");
                currentHover = hitPosition;
                tiles[hitPosition.x, hitPosition.y].layer = LayerMask.NameToLayer("Hover");
                DisplayPieceInfo.Instance.DisplayInfo(hitPosition.x, hitPosition.y, chessPieces);
            }

            //If holding down mouse
            if(Input.GetMouseButtonDown(0))
            {
                if(chessPieces[hitPosition.x,hitPosition.y] != null)
                {
                    //Is it the correct turn
                    if (((chessPieces[hitPosition.x,hitPosition.y].team == (int)ChessPieceTeam.White && isWhiteTurn && (GameManager.Instance.netHandler.currentTeam == 0)) || (chessPieces[hitPosition.x, hitPosition.y].team == ChessPieceTeam.Black && !isWhiteTurn && (GameManager.Instance.netHandler.currentTeam == 1))) && ((chessPieces[hitPosition.x, hitPosition.y]).canMove))
                    {
                        currentlySelected = chessPieces[hitPosition.x, hitPosition.y];

                        //Create a list of available moves
                        availableMoves = currentlySelected.GetAvailableMoves(ref chessPieces, TILE_COUNT_X, TILE_COUNT_Y);
                        //Create a list of special moves
                        specialMove = currentlySelected.GetSpecialMove(ref chessPieces, ref moveList, ref availableMoves);
                        //Create a list of ranged moves
                        availableMovesRanged = currentlySelected.GetRangedMoves(ref chessPieces, TILE_COUNT_X, TILE_COUNT_Y);

                        //Highlight those tiles
                        HighlightTiles();
                    }
                }
            }
            //If releasing mouse
            if(Input.GetMouseButtonUp(0) && currentlySelected != null)
            {
                Vector2Int previousPosition = new Vector2Int(currentlySelected.currentX, currentlySelected.currentY);

                if (ContainsValidMove(ref availableMoves, new Vector2Int(hitPosition.x, hitPosition.y)))
                {
                    MoveTo(previousPosition.x, previousPosition.y, hitPosition.x, hitPosition.y);
                    if(GameManager.Instance.isMultiplayer)
                    {
                        NetMakeMove mm = new NetMakeMove();
                        mm.originalX = previousPosition.x;
                        mm.originalY = previousPosition.y;
                        mm.destinationX = hitPosition.x;
                        mm.destinationY = hitPosition.y;
                        mm.isRanged = 0;
                        mm.teamId = currentTeam;
                        Client.Instance.SendToServer(mm);

                    }
                }
                else if(ContainsValidMove(ref availableMovesRanged, new Vector2Int(hitPosition.x,hitPosition.y)))
                {
                    //Check if it's a ranged attack instead
                    if (RangedAttack(currentlySelected, hitPosition.x, hitPosition.y))
                    {
                        Debug.Log("Fired Gun");
                    }

                    if (GameManager.Instance.isMultiplayer)
                    {
                        NetMakeMove mm = new NetMakeMove();
                        mm.originalX = previousPosition.x;
                        mm.originalY = previousPosition.y;
                        mm.destinationX = hitPosition.x;
                        mm.destinationY = hitPosition.y;
                        mm.isRanged = 1;
                        mm.teamId = currentTeam;
                        Client.Instance.SendToServer(mm);

                    }

                    currentlySelected.SetPosition(GetTileCenter(previousPosition.x, previousPosition.y));
                    currentlySelected = null;
                    UnhighlightTiles();

                }
                else
                {
                    currentlySelected.SetPosition(GetTileCenter(previousPosition.x, previousPosition.y));
                    currentlySelected = null;
                    UnhighlightTiles();
                }


            }
        }

        else
        {
            if(currentHover != -Vector2Int.one)
            {
                tiles[currentHover.x, currentHover.y].layer = (ContainsValidMove(ref availableMoves, currentHover)) ? LayerMask.NameToLayer("Highlight") : LayerMask.NameToLayer("Tile");
                currentHover = -Vector2Int.one;   
            }

            if(currentlySelected != null && Input.GetMouseButtonUp(0))
            {
                currentlySelected.SetPosition(GetTileCenter(currentlySelected.currentX, currentlySelected.currentY));
                currentlySelected = null;
                UnhighlightTiles();
            }
        }

        //Still dragging
        if(currentlySelected)
        {
            Plane horizontalPlane = new Plane(Vector3.up, Vector3.up * yOffset);
            float distance = 0f;
            if (horizontalPlane.Raycast(ray, out distance))
            {
                currentlySelected.SetPosition(ray.GetPoint(distance) + Vector3.up  * dragOffset);
            }
        }
    }

    //Board Generation
    private void GenerateBoard(float tileSize, int tileCountX, int tileCountY)
    {
        yOffset += transform.position.y;
        bounds = new Vector3((tileCountX/2) * tileSize, 0, (tileCountX/2) * tileSize) + boardCenter;

        tiles = new GameObject[tileCountX, tileCountY];

        for (int x = 0; x < tileCountX; x++)
        {
            for (int y = 0; y < tileCountY; y++)
            {
                tiles[x, y] = GenerateTile(tileSize,x,y);
            }
        }
    }
    private GameObject GenerateTile(float tileSize, int x, int y)
    {
        GameObject tileObject = new GameObject($"X:{x},Y:{y}");
        tileObject.transform.parent = transform;

        Mesh mesh = new Mesh();
        tileObject.AddComponent<MeshFilter>().mesh = mesh;
        tileObject.AddComponent<MeshRenderer>().material = tileMaterial;


        Vector3[] vertices = new Vector3[4];
        vertices[0] = new Vector3(x * tileSize,yOffset, y * tileSize) - bounds;
        vertices[1] = new Vector3(x * tileSize, yOffset, (y+1) * tileSize) - bounds;
        vertices[2] = new Vector3((x+1) * tileSize, yOffset, y * tileSize) - bounds;
        vertices[3] = new Vector3((x+1) * tileSize, yOffset, (y+1) * tileSize) - bounds;

        int[] tris = new int[] { 0, 1, 2, 1, 3, 2 };

        mesh.vertices = vertices;
        mesh.triangles = tris;

        mesh.RecalculateNormals();

        tileObject.AddComponent<BoxCollider>();

        tileObject.layer = LayerMask.NameToLayer("Tile");

        return tileObject;
    }

    //Spawning Pieces
    private void FillBoard()
    {

        chessPieces = new ChessPiece[TILE_COUNT_X, TILE_COUNT_Y];
        
        module.FillBoard(ref chessPieces, TILE_COUNT_X, TILE_COUNT_Y, pieceScale);
       /*

        //White Team
        chessPieces[0, 0] = SpawnPiece(ChessPieceType.Rook, ChessPieceTeam.White);
        chessPieces[1, 0] = SpawnPiece(ChessPieceType.Knight, ChessPieceTeam.White);
        chessPieces[2, 0] = SpawnPiece(ChessPieceType.Bishop, ChessPieceTeam.White);
        chessPieces[3, 0] = SpawnPiece(ChessPieceType.Queen, ChessPieceTeam.White);
        chessPieces[4, 0] = SpawnPiece(ChessPieceType.King, ChessPieceTeam.White);
        chessPieces[5, 0] = SpawnPiece(ChessPieceType.Bishop, ChessPieceTeam.White);
        chessPieces[6, 0] = SpawnPiece(ChessPieceType.Knight, ChessPieceTeam.White);
        chessPieces[7, 0] = SpawnPiece(ChessPieceType.Rook, ChessPieceTeam.White);
        for (int i = 0; i < TILE_COUNT_X; i++)
        {
            chessPieces[i, 1] = SpawnPiece(ChessPieceType.Pawn, ChessPieceTeam.White);
        }

        //Black Team
        chessPieces[0, 7] = SpawnPiece(ChessPieceType.Rook, ChessPieceTeam.Black);
        chessPieces[1, 7] = SpawnPiece(ChessPieceType.Knight, ChessPieceTeam.Black);
        chessPieces[2, 7] = SpawnPiece(ChessPieceType.Bishop, ChessPieceTeam.Black);
        chessPieces[3, 7] = SpawnPiece(ChessPieceType.Queen, ChessPieceTeam.Black);
        chessPieces[4, 7] = SpawnPiece(ChessPieceType.King, ChessPieceTeam.Black);
        chessPieces[5, 7] = SpawnPiece(ChessPieceType.Bishop, ChessPieceTeam.Black);
        chessPieces[6, 7] = SpawnPiece(ChessPieceType.Knight, ChessPieceTeam.Black);
        chessPieces[7, 7] = SpawnPiece(ChessPieceType.Rook, ChessPieceTeam.Black);
        for (int i = 0; i < TILE_COUNT_X; i++)
        {
            chessPieces[i, 6] = SpawnPiece(ChessPieceType.Pawn, ChessPieceTeam.Black);
        }

        */
    }
    
    private ChessPiece SpawnPiece(ChessPieceType type, ChessPieceTeam team)
    {
        ChessPiece piece = Instantiate(prefabs[(int)type - 1], transform).GetComponent<ChessPiece>();

        piece.SetScale(pieceScale, true);

        if(team == ChessPieceTeam.Black)
        {
            piece.transform.Rotate(0f, 0f, 180f);
        }

        piece.type = type;
        piece.team = team;
        piece.GetComponent<MeshRenderer>().material = teamMaterials[(int)team];

        return piece;
    }
    
    //Positioning
    private void PositionAllPieces()
    {
        for (int x = 0; x < TILE_COUNT_X; x++)
        {
            for (int y = 0; y < TILE_COUNT_Y; y++)
            {
                if (chessPieces[x,y] != null)
                {
                    PositionPiece(x, y, true);
                }
            }
        }
    }
    private void PositionPiece(int x, int y, bool force = false)
    {
        chessPieces[x, y].currentX = x;
        chessPieces[x, y].currentY = y;

        chessPieces[x, y].SetPosition(GetTileCenter(x, y), force);
    }
    private Vector3 GetTileCenter(int x, int y)
    {
        return new Vector3(x * tileSize, yOffset+0.5f, y * tileSize) - bounds + new Vector3(tileSize / 2, 0, tileSize / 2);
    }

    //Highlight Tiles
    private void HighlightTiles()
    {
        for (int i = 0; i < availableMoves.Count; i++)
        {
            tiles[availableMoves[i].x, availableMoves[i].y].layer = LayerMask.NameToLayer("Highlight");
        }
        for (int i = 0; i < availableMovesRanged.Count; i++)
        {
            tiles[availableMovesRanged[i].x, availableMovesRanged[i].y].layer = LayerMask.NameToLayer("HighlightRanged");
        }
    }
    private void UnhighlightTiles()
    {
        for (int i = 0; i < availableMoves.Count; i++)
        {
            tiles[availableMoves[i].x, availableMoves[i].y].layer = LayerMask.NameToLayer("Tile");
        }
        for (int i = 0; i < availableMovesRanged.Count; i++)
        {
            tiles[availableMovesRanged[i].x, availableMovesRanged[i].y].layer = LayerMask.NameToLayer("Tile");
        }
        availableMoves.Clear();
        availableMovesRanged.Clear();
    }

    //Special Moves
    private void ProcessSpecialMove()
    {
        //Debug.Log("Thinking about promoting!");
        if(specialMove == SpecialMove.EnPassant)
        {
            var newMove = moveList[moveList.Count - 1];
            ChessPiece myPawn = chessPieces[newMove[1].x, newMove[1].y];
            var targetPawnPosition = moveList[moveList.Count - 2];
            ChessPiece enemyPawn = chessPieces[targetPawnPosition[1].x, targetPawnPosition[1].y];

            if(myPawn.currentX == enemyPawn.currentX)
            {
                if(myPawn.currentY == enemyPawn.currentY -1 || myPawn.currentY == enemyPawn.currentY + 1)
                {
                    if(enemyPawn.team == ChessPieceTeam.White)
                    {
                        deadWhite.Add(enemyPawn);
                        enemyPawn.SetScale(Vector3.one * deadSize);
                        enemyPawn.SetPosition(new Vector3(8 * tileSize, yOffset, -1 * tileSize) - bounds + new Vector3(tileSize / 2, 0, tileSize / 2) + (Vector3.forward * deadSpacing) * deadWhite.Count);
                    }
                    if(enemyPawn.team == ChessPieceTeam.Black)
                    {
                        deadBlack.Add(enemyPawn);
                        enemyPawn.SetScale(Vector3.one * deadSize);
                        enemyPawn.SetPosition(new Vector3(-1 * tileSize, yOffset, 8 * tileSize) - bounds + new Vector3(tileSize / 2, 0, tileSize / 2) + (Vector3.back * deadSpacing) * deadBlack.Count);
                    }
                    chessPieces[enemyPawn.currentX, enemyPawn.currentY] = null;
                }
            }    
        }
        if(specialMove == SpecialMove.Promotion)
        {
            Vector2Int[] lastMove = moveList[moveList.Count - 1];
            ChessPiece pawn = chessPieces[lastMove[1].x, lastMove[1].y];

            if(pawn.type == ChessPieceType.Pawn)
            {
                if(pawn.team == ChessPieceTeam.White && lastMove[1].y == (TILE_COUNT_Y - 1))
                {
                    ChessPiece queen = SpawnPiece(ChessPieceType.Queen, ChessPieceTeam.White);
                    queen.transform.position = chessPieces[lastMove[1].x, lastMove[1].y].transform.position;
                    Destroy(chessPieces[lastMove[1].x, lastMove[1].y].gameObject);
                    chessPieces[lastMove[1].x, lastMove[1].y] = queen;
                    PositionPiece(lastMove[1].x, lastMove[1].y);
                }
                else if(pawn.team == ChessPieceTeam.Black && lastMove[1].y == 0)
                {
                    ChessPiece queen = SpawnPiece(ChessPieceType.Queen, ChessPieceTeam.Black);
                    queen.transform.position = chessPieces[lastMove[1].x, lastMove[1].y].transform.position;
                    Destroy(chessPieces[lastMove[1].x, lastMove[1].y].gameObject);
                    chessPieces[lastMove[1].x, lastMove[1].y] = queen;
                    PositionPiece(lastMove[1].x, lastMove[1].y);
                }
            }
        }
        if(specialMove == SpecialMove.Castling)
        {
            Vector2Int[] lastMove = moveList[moveList.Count - 1];


            if(lastMove[1].x == 2) //Left Rook
            {
                if(lastMove[1].y == 0) //White Side
                {
                    ChessPiece rook = chessPieces[0, 0];
                    chessPieces[3, 0] = rook;
                    PositionPiece(3, 0);
                    chessPieces[0, 0] = null;
                }
                else if(lastMove[1].y == 7) //Black Side
                {
                    ChessPiece rook = chessPieces[0,7];
                    chessPieces[3,7] = rook;
                    PositionPiece(3, 7);
                    chessPieces[0, 7] = null;
                }
            }
            else if (lastMove[1].x == 6)//Right Rook
            {
                if (lastMove[1].y == 0) //White Side
                {
                    ChessPiece rook = chessPieces[7, 0];
                    chessPieces[5, 0] = rook;
                    PositionPiece(5, 0);
                    chessPieces[7, 0] = null;
                }
                else if (lastMove[1].y == 7) //Black Side
                {
                    ChessPiece rook = chessPieces[7, 7];
                    chessPieces[5, 7] = rook;
                    PositionPiece(5, 7);
                    chessPieces[7, 7] = null;
                }
            }
        }
        if(specialMove == SpecialMove.Freeze)
        {
            var newMove = moveList[moveList.Count - 1];
            ChessPiece freezer = chessPieces[newMove[1].x, newMove[1].y];
            Vector2Int oldPosition = new Vector2Int(newMove[0].x, newMove[0].y);

            //Unfreeze around previous position
            if (oldPosition.y + 1 < TILE_COUNT_Y)
            {
                if (chessPieces[oldPosition.x, oldPosition.y + 1] != null)
                {
                    chessPieces[oldPosition.x, oldPosition.y + 1].canMove = true;
                }
            }
            if (oldPosition.y - 1 >= 0)
            {
                if (chessPieces[oldPosition.x, oldPosition.y - 1] != null)
                {
                    chessPieces[oldPosition.x, oldPosition.y - 1].canMove = true;
                }
            }
            if (oldPosition.x + 1 < TILE_COUNT_X)
            {
                if (chessPieces[oldPosition.x + 1, oldPosition.y] != null)
                {
                    chessPieces[oldPosition.x + 1, oldPosition.y].canMove = true;
                }
                if (oldPosition.y + 1 < TILE_COUNT_Y)
                {
                    if (chessPieces[oldPosition.x + 1, oldPosition.y + 1] != null)
                    {
                        chessPieces[oldPosition.x + 1, oldPosition.y + 1].canMove = true;
                    }
                }
                if (oldPosition.y - 1 <= 0)
                {
                    if (chessPieces[oldPosition.x + 1, oldPosition.y - 1] != null)
                    {
                        chessPieces[oldPosition.x + 1, oldPosition.y - 1].canMove = true;
                    }
                }
            }
            if (oldPosition.x - 1 >= 0)
            {
                if (chessPieces[oldPosition.x - 1, oldPosition.y] != null)
                {
                    chessPieces[oldPosition.x - 1, oldPosition.y].canMove = true;
                }
                if (oldPosition.y + 1 < TILE_COUNT_Y)
                {
                    if (chessPieces[oldPosition.x - 1, oldPosition.y + 1] != null)
                    {
                        chessPieces[oldPosition.x - 1, oldPosition.y + 1].canMove = true;
                    }
                }
                if (oldPosition.y - 1 <= 0)
                {
                    if (chessPieces[oldPosition.x - 1, oldPosition.y - 1] != null)
                    {
                        chessPieces[oldPosition.x - 1, oldPosition.y - 1].canMove = true;
                    }
                }
            }
            //Freeze around new position
            if (freezer.currentY + 1 < TILE_COUNT_Y)
            {
                if(chessPieces[freezer.currentX, freezer.currentY + 1] != null)
                {
                   chessPieces[freezer.currentX, freezer.currentY + 1].canMove = false;
                }
            }
            if(freezer.currentY - 1 >= 0)
            {
                if (chessPieces[freezer.currentX, freezer.currentY - 1] != null)
                {
                    chessPieces[freezer.currentX, freezer.currentY - 1].canMove = false;
                }
            }
            if(freezer.currentX + 1 < TILE_COUNT_X)
            {
                if (chessPieces[freezer.currentX + 1, freezer.currentY] != null)
                {
                    chessPieces[freezer.currentX + 1, freezer.currentY].canMove = false;
                }
                if (freezer.currentY + 1 < TILE_COUNT_Y)
                {
                    if (chessPieces[freezer.currentX + 1, freezer.currentY + 1] != null)
                    {
                        chessPieces[freezer.currentX + 1, freezer.currentY + 1].canMove = false;
                    }
                }
                if(freezer.currentY - 1 <= 0)
                {
                    if (chessPieces[freezer.currentX + 1, freezer.currentY - 1] != null)
                    {
                        chessPieces[freezer.currentX + 1, freezer.currentY - 1].canMove = false;
                    }
                }
            }
            if(freezer.currentX - 1 >= 0)
            {
                if (chessPieces[freezer.currentX - 1, freezer.currentY] != null)
                {
                    chessPieces[freezer.currentX - 1, freezer.currentY].canMove = false;
                }
                if (freezer.currentY + 1 < TILE_COUNT_Y)
                {
                    if (chessPieces[freezer.currentX - 1, freezer.currentY + 1] != null)
                    {
                        chessPieces[freezer.currentX - 1, freezer.currentY + 1].canMove = false;
                    }
                }
                if (freezer.currentY - 1 <= 0)
                {
                    if (chessPieces[freezer.currentX - 1, freezer.currentY - 1] != null)
                    {
                        chessPieces[freezer.currentX - 1, freezer.currentY - 1].canMove = false;
                    }
                }
            }

        }
        if(specialMove == SpecialMove.Drain)
        {

        }
    } //Handle special moves, any ability that moves the piece in question
    private bool RangedAttack(ChessPiece piece, int x, int y)
    {
        if (!ContainsValidMove(ref availableMovesRanged, new Vector2Int(x, y)))
        {
            //Debug.Log("Failed to Fire Gun");
            return false;
        }

        //Error Catch: No ranged attack assigned
        if (piece.ranged == RangedAttackType.None)
        {
            Debug.LogWarning("No Ranged Attack Assigned");
            return false;
        }
        //Gun: Capture targeted piece
        if (piece.ranged == RangedAttackType.Gun)
        {
            if (chessPieces[x, y] != null)
            {
                if (chessPieces[x, y].team != piece.team)
                {
                    CapturePiece(chessPieces[x, y], piece);
                    piece.ammunition--;
                }
                else
                {
                    return false;
                }
            }
        }
        //Spawn: Spawn predetermined piece
        if (piece.ranged == RangedAttackType.Spawn)
        {
            if (piece.spawnedPiece != null && piece.ammunition > 0) //Has a piece to spawn
            {
                if (chessPieces[x, y] == null) //Space is free
                {
                    ChessPiece newPiece = ForceSpawnPiece(piece.spawnedPiece, piece.team);
                    chessPieces[x, y] = newPiece;
                    PositionPiece(x, y, true);
                    piece.ammunition--;
                }
            }
        }
        //Convert: Swap the team of the targeted piece
        if (piece.ranged == RangedAttackType.Convert)
        {
            if (chessPieces[x, y] != null)
            {
                if (chessPieces[x, y].type == ChessPieceType.Pawn)
                {
                    SwapTeam(chessPieces[x, y]);
                }
            }
        }
        //Sacrifice: Capture the moving piece, use for setup in CapturePiece
        if(piece.ranged == RangedAttackType.Sacrifice)
        {
            CapturePiece(chessPieces[piece.currentX,piece.currentY], piece);
        }
        //Bombify: Set the targeted piece's WhenCaptured to Bomb
        if(piece.ranged == RangedAttackType.Bombify)
        {
            if(piece.ammunition > 0)
            {
                if (chessPieces[x, y] != null)
                {
                    chessPieces[x, y].range = piece.range;
                    chessPieces[x, y].whenCaptured = OnCapture.Bomb;
                    piece.ammunition--;
                }
            }

        }

        EndTurn();
        UnhighlightTiles();
        return true;
    } //Handle ranged moves, any ability that doesn't move the piece in question

    //Operation
    private Vector2Int LookUpTileIndex(GameObject hitInfo)
    {
        for (int x = 0; x < TILE_COUNT_X; x++)
        {
            for (int y = 0; y < TILE_COUNT_Y; y++)
            {
                if(tiles[x,y] == hitInfo)
                {
                    return new Vector2Int(x, y);
                }
            }
        }
        return -Vector2Int.one; // -1,-1
    }
    private void MoveTo(int originalX, int originalY, int x, int y)
    {
        ChessPiece piece = chessPieces[originalX, originalY];

        Vector2Int previousPosition = new Vector2Int(originalX, originalY);

        //Is space free
        if (chessPieces[x,y] != null)
        {
            ChessPiece otherPiece = chessPieces[x, y];

            if (piece.team == otherPiece.team)
            {
                return;
            }
            //Can capture

            CapturePiece(otherPiece, piece);

            /*
            if (otherPiece.team == ChessPieceTeam.White)
            {
                if (otherPiece.type == ChessPieceType.King)
                {
                    CheckMate(1);
                }

                deadWhite.Add(otherPiece);
                otherPiece.SetScale(Vector3.one * deadSize);
                otherPiece.SetPosition(new Vector3(8 * tileSize, yOffset, -1 * tileSize) - bounds + new Vector3(tileSize / 2, 0, tileSize / 2) + (Vector3.forward * deadSpacing) * deadWhite.Count);
            }
            else if (otherPiece.team == ChessPieceTeam.Black)
            {
                if (otherPiece.type == ChessPieceType.King)
                {
                    CheckMate(0);
                }

                deadBlack.Add(otherPiece);
                otherPiece.SetScale(Vector3.one * deadSize);
                otherPiece.SetPosition(new Vector3(-1 * tileSize, yOffset, 8 * tileSize) - bounds + new Vector3(tileSize / 2, 0, tileSize / 2) + (Vector3.back * deadSpacing) * deadBlack.Count);
            }
            */
        }

        chessPieces[x, y] = piece;
        chessPieces[previousPosition.x, previousPosition.y] = null;
        piece.moved = true;

        PositionPiece(x, y);

        //isWhiteTurn = !isWhiteTurn;
        EndTurn();
        moveList.Add(new Vector2Int[] { previousPosition, new Vector2Int(x, y) });
        Vector2Int[] lastMove = moveList[moveList.Count - 1];
        Debug.Log($"Moved {piece.team} {piece.name} from: ({lastMove[0].x},{lastMove[0].y}) to: ({lastMove[1].x},{lastMove[1].y}) ");

        ProcessSpecialMove();

        currentlySelected = null;
        UnhighlightTiles();

        return;
    }
    private bool ContainsValidMove(ref List<Vector2Int> moves, Vector2 pos)
    {
        for (int i = 0; i < moves.Count; i++)
        {
            if (moves[i].x == pos.x && moves[i].y == pos.y)
            {
                return true;
            }
        }
        return false;
    }
    public void CapturePiece(ChessPiece capturedPiece, ChessPiece attacker)
    {
        if (capturedPiece.whenCaptured == OnCapture.None)
        {
            if (capturedPiece.team == ChessPieceTeam.White)
            {
                deadWhite.Add(capturedPiece);
                capturedPiece.SetScale(Vector3.one * deadSize);
                capturedPiece.SetPosition(new Vector3(8 * tileSize, yOffset, -1 * tileSize) - bounds + new Vector3(tileSize / 2, 0, tileSize / 2) + (Vector3.forward * deadSpacing) * deadWhite.Count);
                chessPieces[capturedPiece.currentX, capturedPiece.currentY] = null;
            }
            if (capturedPiece.team == ChessPieceTeam.Black)
            {
                deadBlack.Add(capturedPiece);
                capturedPiece.SetScale(Vector3.one * deadSize);
                capturedPiece.SetPosition(new Vector3(-1 * tileSize, yOffset, 8 * tileSize) - bounds + new Vector3(tileSize / 2, 0, tileSize / 2) + (Vector3.back * deadSpacing) * deadBlack.Count);
                chessPieces[capturedPiece.currentX, capturedPiece.currentY] = null;
            }

            if (capturedPiece.type == ChessPieceType.King)
            {
                CheckMate((capturedPiece.team == 0) ? 1 : 0);
            }
        }
        if (capturedPiece.whenCaptured == OnCapture.Trap)
        {
            if (attacker.whenCaptured != OnCapture.Trap)
            {
                CapturePiece(attacker, capturedPiece);
            }
        }
        if (capturedPiece.whenCaptured == OnCapture.SpawnNew)
        {
            for (int i = 0; i < capturedPiece.ammunition; i++)
            {
                Vector2Int newSpace = GetARandomFreeSpace(capturedPiece, capturedPiece.range);
                ChessPiece newPiece = ForceSpawnPiece(capturedPiece.spawnedPiece, capturedPiece.team);
                Debug.Log("X: " + newSpace.x + "Y: " + newSpace.y);
                if (!ForceMoveTo(newPiece, newSpace.x, newSpace.y))
                {
                    Debug.Log("Yeah it couldn't move!");
                }
            }

            capturedPiece.whenCaptured = OnCapture.None;
            CapturePiece(capturedPiece, attacker);
        }
        if (capturedPiece.whenCaptured == OnCapture.Bomb)
        {
            capturedPiece.whenCaptured = OnCapture.None;
            Debug.Log("Bomb went off!");
            List<Vector2Int> inRange = GetAllPieces(capturedPiece, capturedPiece.range);

            foreach(Vector2Int i in inRange)
            {
                Debug.Log("Trying to blow something up!");
                if(chessPieces[i.x,i.y] != null)
                {
                    Debug.Log("Found someone to blow up!");
                    if (chessPieces[i.x, i.y].whenCaptured == OnCapture.Trap)
                    {
                        chessPieces[i.x, i.y].whenCaptured = OnCapture.None;
                    }
                    CapturePiece(chessPieces[i.x, i.y], capturedPiece);
                }

            }

            attacker.whenCaptured = OnCapture.None;
            CapturePiece(attacker, capturedPiece);
        }
        if (capturedPiece.whenCaptured == OnCapture.Survive)
        {
            Vector2Int newSpace = GetARandomFreeSpace(capturedPiece, capturedPiece.range);
            ForceMoveTo(capturedPiece, newSpace.x, newSpace.y);
            capturedPiece.ammunition--;
            if(capturedPiece.ammunition <= 0)
            {
                capturedPiece.whenCaptured = OnCapture.None;
            }
        }


    } //Consolidate piece capturing to avoid repeated code


    //Checkmate
    private void CheckMate(int team)
    {
        DisplayVictory(team);
    }
    private void DisplayVictory(int winner)
    {
        victoryScreen.SetActive(true);
        victoryScreen.transform.GetChild(winner).gameObject.SetActive(true);
    }
    public void OnResetButton()
    {
        //Reset UI
        victoryScreen.transform.GetChild(0).gameObject.SetActive(false);
        victoryScreen.transform.GetChild(1).gameObject.SetActive(false);
        victoryScreen.SetActive(false);

        //Field Reset
        currentlySelected = null;
        availableMoves.Clear();
        moveList.Clear();

        //Clean up array
        for (int x = 0; x < TILE_COUNT_X; x++)
        {
            for (int y = 0; y < TILE_COUNT_Y; y++)
            {
                if (chessPieces[x,y] != null)
                {
                    Destroy(chessPieces[x, y].gameObject);
                }
                chessPieces[x, y] = null;
            }
        }
        for (int i = 0; i < deadWhite.Count; i++)
        {
            Destroy(deadWhite[i].gameObject);
        }
        for (int i = 0; i < deadBlack.Count; i++)
        {
            Destroy(deadBlack[i].gameObject);
        }
        deadWhite.Clear();
        deadBlack.Clear();


        //Reset Board
        FillBoard();
        PositionAllPieces();
        isWhiteTurn = true;
    }
    public void OnExitButton()
    {
        Application.Quit();
    }



    // FOR LATER
    public void SwapTeam(ChessPiece piece)
    {
        piece.team = (piece.team == ChessPieceTeam.White) ? ChessPieceTeam.Black : ChessPieceTeam.White;

        piece.GetComponent<MeshRenderer>().material = teamMaterials[(int)piece.team];

        if (piece.team == ChessPieceTeam.Black)
        {
            piece.transform.Rotate(0f, 0f, 180f);
        }

    } //For some kind of shenanigans later
    private ChessPiece ForceSpawnPiece(GameObject piecePrefab, ChessPieceTeam team)
    {
        ChessPiece piece = Instantiate(piecePrefab, transform).GetComponent<ChessPiece>();

        piece.SetScale(pieceScale, true);

        if (team == ChessPieceTeam.Black)
        {
            piece.transform.Rotate(0f, 0f, 180f);
        }

        piece.type = ChessPieceType.None;
        piece.team = team;
        piece.GetComponent<MeshRenderer>().material = teamMaterials[(int)team];

        return piece;
    } //Spawn a piece that's outside the prefab array

    public List<Vector2Int> GetAllFreeSpaces(ChessPiece piece, int range)
    {
        List<Vector2Int> freeSpaces = new List<Vector2Int>();

        for (int x = piece.currentX - range; x < piece.currentX + range; x++)
        {
            for (int y = piece.currentY - range; y < piece.currentY + range; y++)
            {
                if((x >= 0 && x < TILE_COUNT_X) && (y >= 0 && y < TILE_COUNT_Y))
                {
                    if(chessPieces[x,y] == null)
                    {
                        freeSpaces.Add(new Vector2Int(x, y));
                    }
                }

            }
        }

        return freeSpaces;
    }
    public List<Vector2Int> GetAllPieces(ChessPiece piece, int range)
    {
        List<Vector2Int> spaces = new List<Vector2Int>();

        for (int x = piece.currentX - range; x < piece.currentX + range; x++)
        {
            for (int y = piece.currentY - range; y < piece.currentY + range; y++)
            {
                if ((x >= 0 && x < TILE_COUNT_X) && (y >= 0 && y < TILE_COUNT_Y))
                {
                    if (chessPieces[x, y] != null)
                    {
                        Debug.Log("Found: " + chessPieces[x, y].team + " " + chessPieces[x, y].name + " at: " + x + ", " + y);
                        spaces.Add(new Vector2Int(x, y));
                    }
                }

            }
        }

        return spaces;
    }
    public Vector2Int GetARandomFreeSpace(ChessPiece piece, int range)
    {
        System.Random rnd = new System.Random();

        List<Vector2Int> freeSpaces = new List<Vector2Int>();
        Vector2Int freeSpace = new Vector2Int();

        for (int x = piece.currentX - range; x < piece.currentX + range; x++)
        {
            for (int y = piece.currentY - range; y < piece.currentY + range; y++)
            {
                if ((x >= 0 && x < TILE_COUNT_X) && (y >= 0 && y < TILE_COUNT_Y))
                {
                    if (chessPieces[x, y] == null)
                    {
                        freeSpaces.Add(new Vector2Int(x, y));
                    }
                }

            }
        }

        if(freeSpaces.Count > 1)
        {
            int index = rnd.Next(0, freeSpaces.Count);
            freeSpace = freeSpaces[index];
        }
        else
        {
            if(freeSpaces.Count == 1)
            {
                freeSpace = freeSpaces[0];
            }
            else
            {
                freeSpace = GetARandomFreeSpace(piece, range++);
            }
            
        }

        return freeSpace;
    }
    public Vector2Int GetARandomPiece(ChessPiece piece, int range)
    {
        System.Random rnd = new System.Random();

        List<Vector2Int> spaces = new List<Vector2Int>();
        Vector2Int space = new Vector2Int();

        for (int x = piece.currentX - range; x < piece.currentX + range; x++)
        {
            for (int y = piece.currentY - range; y < piece.currentY + range; y++)
            {
                if ((x >= 0 && x < TILE_COUNT_X) && (y >= 0 && y < TILE_COUNT_Y))
                {
                    if (chessPieces[x, y] == null && (piece.currentX != x && piece.currentY != y))
                    {
                        spaces.Add(new Vector2Int(x, y));
                    }
                }

            }
        }

        if (spaces.Count > 1)
        {
            int index = rnd.Next(0, spaces.Count);
            space = spaces[index];
        }
        else
        {
            if(spaces.Count == 1)
            {
                space = spaces[0];
            }
            else
            {
                space = GetARandomPiece(piece, range++);
            }

        }


        return space;
    }
    private bool ForceMoveTo(ChessPiece piece, int x, int y, bool DoesTheMoveCount = false)
    {
        if (chessPieces[x, y] != null)
        {
            ChessPiece otherPiece = chessPieces[x, y];
            if (piece.team == otherPiece.team)
            {
                return false;
            }
            //Can capture
            CapturePiece(otherPiece, piece);
        }
        chessPieces[x, y] = piece;
        if(DoesTheMoveCount)
        {
            Vector2Int previousPosition = new Vector2Int(piece.currentX, piece.currentY);
            isWhiteTurn = !isWhiteTurn;
            chessPieces[previousPosition.x, previousPosition.y] = null;
            piece.moved = true;
            moveList.Add(new Vector2Int[] { previousPosition, new Vector2Int(x, y) });
            Vector2Int[] lastMove = moveList[moveList.Count - 1];
            Debug.Log($"Moved {piece.team} {piece.name} from: ({lastMove[0].x},{lastMove[0].y}) to: ({lastMove[1].x},{lastMove[1].y}) ");
        }
        PositionPiece(x, y, !DoesTheMoveCount);

        return true;
    }

   

    public void EndTurn()
    {
        if(GameManager.Instance.isMultiplayer)
        {
            //multiplayer turns
            isWhiteTurn = !isWhiteTurn;
        }
        else
        {
            //singleplayer turns
            isWhiteTurn = !isWhiteTurn;

            if(GameManager.Instance.netHandler.currentTeam == 0)
            {
                GameManager.Instance.netHandler.currentTeam = 1;
            }
            else
            {
                GameManager.Instance.netHandler.currentTeam = 0;
            }
        }
    }
    
    #region
    private void RegisterEvents()
    {
        NetUtility.S_MAKE_MOVE += OnMakeMoveServer;

        NetUtility.C_MAKE_MOVE += OnMakeMoveClient; 
    }
    private void UnregisterEvents()
    {
        NetUtility.S_MAKE_MOVE -= OnMakeMoveServer;

        NetUtility.C_MAKE_MOVE -= OnMakeMoveClient;
    }

    private void OnMakeMoveServer(NetMessage msg, NetworkConnection cnn)
    {
        NetMakeMove mm = msg as NetMakeMove;

        //Receive and broadcast
        Server.Instance.Broadcast(mm);
    }
    private void OnMakeMoveClient(NetMessage msg)
    {
        NetMakeMove mm = msg as NetMakeMove;

        Debug.Log($"MM: {mm.teamId} : {mm.originalX} {mm.originalY} -> {mm.destinationX} {mm.destinationY}. Is Ranged: {mm.isRanged}");

        //Only move the piece if it's not on your team.
        if(mm.teamId != GameManager.Instance.netHandler.currentTeam)
        {
            //Which piece are we moving?
            ChessPiece target = chessPieces[mm.originalX, mm.originalY];
            
            //Fill in the available move lists, in case of special or ranged moves
            //Create a list of available moves
            availableMoves = target.GetAvailableMoves(ref chessPieces, TILE_COUNT_X, TILE_COUNT_Y);
            //Create a list of special moves
            specialMove = target.GetSpecialMove(ref chessPieces, ref moveList, ref availableMoves);
            //Create a list of ranged moves
            availableMovesRanged = target.GetRangedMoves(ref chessPieces, TILE_COUNT_X, TILE_COUNT_Y);

            if(mm.isRanged == 0)
            {
                MoveTo(mm.originalX, mm.originalY, mm.destinationX, mm.destinationY);
            }
            else
            {
                //Handle Ranged moves
                RangedAttack(chessPieces[mm.originalX, mm.originalY], mm.destinationX, mm.destinationY);
            }
        }
    }
    /*
    private void OnWelcomeServer(NetMessage msg, NetworkConnection cnn)
    {
        //Client has connected, assign a team and respond
        NetWelcome nw = msg as NetWelcome;

        nw.AssignedTeam = ++playerCount;

        Server.Instance.SendToClient(cnn, nw);
    }
    private void OnWelcomeClient(NetMessage msg)
    {
        //Receive the connection message
        NetWelcome nw = msg as NetWelcome;

        currentTeam = nw.AssignedTeam;

        Debug.Log($"Client assigned team: {nw.AssignedTeam}");
    }
    */
    #endregion
    
}
