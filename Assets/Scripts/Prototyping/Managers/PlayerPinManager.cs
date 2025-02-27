﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerPinManager : MonoBehaviour
{
    public static PlayerPinManager Singleton { get; private set; }
    private GameObject g_PlayerPinUI = null;
    private bool b_IsFollowingMouse = false;

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

        g_PlayerPinUI = transform.GetChild(0).gameObject;
    }

    void Update()
    {
        if (b_IsFollowingMouse)
        {
            Vector3 newPos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x + 35, Input.mousePosition.y + 35, Mathf.Abs(Camera.main.transform.position.z - transform.position.z)));
            newPos.z = transform.position.z;
            transform.position = newPos;
        } 
    }

    public void UpdatePlayerPinUI(bool Active)
    {
        if (Active)
        {
            g_PlayerPinUI.SetActive(true);
        }
        else
        {
            g_PlayerPinUI.SetActive(false);
        }
    }

    public void ExamineAdress()
    {
        MainUIManager.Singleton.SetupDialogueSystem();
        UpdatePlayerPinUI(false);
    }

    public void MoveToAdress()
    {
        b_IsFollowingMouse = true;
        transform.GetComponent<Button>().enabled = false;
        UpdatePlayerPinUI(false);
    }

    public void GoToAdress(Vector3 Pos)
    {
        b_IsFollowingMouse = false;
        transform.position = Pos;
        transform.GetComponent<Button>().enabled = true;
    }
}
