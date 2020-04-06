﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameMaster : MonoBehaviour
{
    public static GameMaster GM;
    [System.NonSerialized]
    public bool playerIsBusy;
    public GameObject playerObject;
    public PlayerController playerController;
    [System.NonSerialized]
    public int currentCustomersNumber = 0;
    [System.NonSerialized]
    public bool playerLoses = false;
    public GameObject cashGameObject;
    public WarehouseController warehouseController;
    [System.NonSerialized]
    public bool warehouseNerby = false;
    public GameObject EndScreen;
    public Text EndScore;
    public Text TimeLeftText;
    public float time = 5;




    public enum keysEnum
    {
        KeyUp = KeyCode.W,
        KeyDown = KeyCode.S,
        KeyLeft = KeyCode.A,
        KeyRight = KeyCode.D,
        KeyRemoveItem = KeyCode.F,
        KeyAction = KeyCode.F,
        ExitWarehouse = KeyCode.Escape
    }

    public enum customerState
    {
        Shopping = 0,
        HasFoundProduct = 1,
        HasNotFoundProduct = 2
    }
    [System.NonSerialized]
    public GameObject nearShelf;
    void Awake()
    {
        if (GM != null)
        {
            GameObject.Destroy(GM);
        }
        else
        {
            GM = this;
        }
       // DontDestroyOnLoad(this);
    }

    private void Update()
    {
        time -= Time.deltaTime;
        TimeLeftText.text = ((int)time).ToString();
        if (time <= 0)
        {
            playerController.GameEnd();
        }
    }
}
