using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour
{
    public static GameMaster GM;
    public bool playerIsBusy;
    public GameObject playerObject;
    public enum keysEnum
    {
        KeyUp = KeyCode.W,
        KeyDown = KeyCode.S,
        KeyLeft = KeyCode.A,
        KeyRight = KeyCode.D,
        KeyRemoveItem = KeyCode.F,
        KeyAction = KeyCode.F
    }
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
