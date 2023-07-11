using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public enum peiceNames
{
    Pawn = 1
}

public class DisplayPieceInfo : MonoBehaviour
{
    public static DisplayPieceInfo Instance { get; set; }

    public GameObject infoHolder, infoOutline;

    public TMP_Text pieceNameField, pieceDescriptionField, pieceCoordinateField;
    public Image pieceDiagramField;

    public string[] descriptions;
    public Image[] images;

    public bool displaying = false;

    private void Awake()
    {
        Instance = this;
    }

    public void DisplayInfo(int x, int y, ChessPiece[,] board)
    {
        if(board[x,y] != null)
        {
            //Debug.Log($"{board[x,y].team} {board[x,y].pieceName} found at {x},{y}");

            pieceNameField.text = $"{board[x, y].team} {board[x,y].pieceName}";
            pieceCoordinateField.text = $"{x},{y}";
        }

        switch (board[x, y].pieceName)
        {
            case "Pawn":
                break;
            default:
                Debug.Log("Somehow not a piece.");
                break;
        }

    }


    public void ToggleInfo()
    {
        infoHolder.SetActive(!infoHolder.activeInHierarchy);
        infoOutline.SetActive(!infoOutline.activeInHierarchy);
    }
    public void EnableInfo()
    {
        infoHolder.SetActive(true);
        infoOutline.SetActive(true);
    }
    public void DisableInfo()
    {
        infoHolder.SetActive(false);
        infoOutline.SetActive(false);
    }

}
