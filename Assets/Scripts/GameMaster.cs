using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    
    public enum keysEnum
    {
        KeyUp = KeyCode.W,
        KeyDown = KeyCode.S,
        KeyLeft = KeyCode.A,
        KeyRight = KeyCode.D,
        KeyRemoveItem = KeyCode.F,
        KeyAction = KeyCode.F
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
        DontDestroyOnLoad(this);
    }

}
