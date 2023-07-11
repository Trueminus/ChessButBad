using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public enum CameraAngle
{ 
    white = 0,
    black = 1,
    waiting = 2
}
public class CameraManager : MonoBehaviour
{
    public bool camerasFound = false, camerasSearching = false;
    [SerializeField] public GameObject[] cameraAngles;
    public void ChangeCamera(CameraAngle angleIndex)
    {
        for (int i = 0; i < cameraAngles.Length; i++)
        {
            cameraAngles[i].SetActive(false);
        }
        cameraAngles[(int)angleIndex].SetActive(true);
    }
    public void SetBaseCamera()
    {
        ChangeCamera((GameManager.Instance.netHandler.currentTeam == 0) ? CameraAngle.white : CameraAngle.black);
    }

    public void FindAllCameras()
    {
        Debug.Log("Looking for those cams");

        cameraAngles = GameObject.FindGameObjectsWithTag("Camera");
    }

    private void Update()
    {
        if(camerasSearching && !camerasFound)
        {
            FindAllCameras();
            if(cameraAngles.Length > 0)
            {
                camerasSearching = false;
                camerasFound = true;

                SetBaseCamera();
            }
        }
    }
}
