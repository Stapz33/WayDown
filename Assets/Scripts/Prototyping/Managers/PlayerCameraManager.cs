using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCameraManager : MonoBehaviour
{
    public static PlayerCameraManager Singleton { get; private set; }

    #region CAMERA_MOVEMENTS
    private float i_CameraSizeToReach = 0;
    private Vector3 v_PositionToMoveTo = new Vector3();
    private bool b_CameraNeedToMove = false;
    #endregion

    public void Awake()
    {
        if (Singleton != null)
        {
            Destroy(this);
        }
        else
        {
            Singleton = this;
        }
    }

    public void Update()
    {

        // MOVES
        if (b_CameraNeedToMove)
        {
            transform.position = Vector3.MoveTowards(transform.position, v_PositionToMoveTo, Time.deltaTime * 1200);
            if (Camera.main.orthographicSize > i_CameraSizeToReach)
            {
                Camera.main.orthographicSize -= Time.deltaTime * 100;
                if (Camera.main.orthographicSize <= i_CameraSizeToReach)
                {
                    Camera.main.orthographicSize = i_CameraSizeToReach;
                }
            }
            if (Camera.main.orthographicSize < i_CameraSizeToReach)
            {
                Camera.main.orthographicSize += Time.deltaTime * 100;
                if (Camera.main.orthographicSize >= i_CameraSizeToReach)
                {
                    Camera.main.orthographicSize = i_CameraSizeToReach;
                }
            }
            if (Camera.main.orthographicSize == i_CameraSizeToReach && transform.position == v_PositionToMoveTo)
            {
                b_CameraNeedToMove = false;
            }
        }

    }
}
