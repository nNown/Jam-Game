using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public float speed;


    private Animator anim;
    private Rigidbody2D rb;
    private Vector2 moveVelocity;
    public GameObject warhouseGui;
    public GameObject floatingTextHelper;
    GameMaster gm;
    ShelfController nearPlayerShelfController;
    List<IInventoryItem> inventoryItemToDelete = new List<IInventoryItem>();
    [System.NonSerialized]
    public int currentHp = 3;
    public int maxHp;
    public event EventHandler<PlayerEventArgs> HpDeplete;
    public event EventHandler<PlayerEventArgs> ScoreAdd;
    public Text textHp;
    public Text textScore;
    

    [System.NonSerialized]
    public int score = 0;

    public Inventory inventory;


    private void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        gm = GameMaster.GM;
        currentHp = maxHp;
        HpDeplete += Event_DeplateHp;
        ScoreAdd += Event_AddScore;
        textHp.text = maxHp.ToString();
        textScore.text = score.ToString();
    }

    private void Update()
    {
        PlayerKeysControl();

    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + moveVelocity * Time.fixedDeltaTime);
    }

    private void PlayerKeysControl()
    {
        Vector2 moveInput = new Vector2();
        if (gm.playerLoses)
        {
            if (Input.GetKeyDown((KeyCode)GameMaster.keysEnum.KeyAction))
            {
                Time.timeScale = 1;
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                gm.RestartObject();
            }
        }
        else if (!gm.playerIsBusy)
        {
            if (!Input.GetKey((KeyCode)GameMaster.keysEnum.KeyUp) && !Input.GetKey((KeyCode)GameMaster.keysEnum.KeyDown) && !Input.GetKey((KeyCode)GameMaster.keysEnum.KeyLeft) && !Input.GetKey((KeyCode)GameMaster.keysEnum.KeyRight))
            {
                anim.SetBool("Idle", true);
            }
            else
            {
                anim.SetBool("Idle", false);
            }

                if (Input.GetKey((KeyCode)GameMaster.keysEnum.KeyUp))
                {
                    moveInput = Vector2.up;
                    anim.SetInteger("Speed", 1);
                }
                else if (Input.GetKey((KeyCode)GameMaster.keysEnum.KeyDown))
                {
                    moveInput = Vector2.down;
                    anim.SetInteger("Speed", 2);
                }
                else if (Input.GetKey((KeyCode)GameMaster.keysEnum.KeyRight))
                {
                    moveInput = Vector2.right;
                    anim.SetInteger("Speed", 3);
                }
                else if (Input.GetKey((KeyCode)GameMaster.keysEnum.KeyLeft))
                {
                    moveInput = Vector2.left;
                    anim.SetInteger("Speed", 4);
                }

                if (Input.GetKeyDown((KeyCode)GameMaster.keysEnum.KeyAction))
                {

                if (gm.nearShelf != null)
                {
                    nearPlayerShelfController = gm.nearShelf.GetComponent<ShelfController>();
                    if (nearPlayerShelfController.CheckIsPlaceOnShelf())
                    {

                        foreach (IInventoryItem item in inventory.MItems)
                        {
                            if (item.ObjectTag == nearPlayerShelfController.shelfProductType.tag)
                            {
                                inventoryItemToDelete.Add(item);
                            }
                        }
                        if (inventoryItemToDelete.Count > 0)
                        {
                            nearPlayerShelfController.PutItemOnShelf();
                            inventory.RemoveLastItem(inventoryItemToDelete.First().ObjectTag);
                            inventoryItemToDelete.Clear();
                        }
                    }
                    }else if (gm.warehouseNerby)
                    {
                        warhouseGui.SetActive(true);
                        warhouseGui.transform.Find("Buttons").GetChild(0).GetComponent<Button>().Select();
                        warhouseGui.transform.Find("Buttons").GetChild(0).GetComponent<Button>().OnSelect(null);
                        gm.playerIsBusy = true;

                    }
                }

                moveVelocity = moveInput.normalized * speed;

            }else
            {
                if (Input.GetKeyDown((KeyCode) GameMaster.keysEnum.KeyRemoveItem))
                {
                    inventory.RemoveLastItem();
                }else if (Input.GetKeyDown((KeyCode)GameMaster.keysEnum.ExitWarehouse))
                {
                warhouseGui.SetActive(false);
                gm.playerIsBusy = false;
                gm.warehouseNerby = false;
            }
        }
       
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        IInventoryItem item = collision.gameObject.GetComponent<IInventoryItem>();
        if (item != null)
        {
            inventory.AddItem(item);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Warehouse"))
        {
            gm.warehouseNerby = true;
        }

        if (collision.gameObject.CompareTag("Shelf"))
        {
            floatingTextHelper.SetActive(true);
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {

        if (collision.gameObject.CompareTag("Shelf"))
        {
            floatingTextHelper.SetActive(false);
        }
    }
    public class PlayerEventArgs : EventArgs
    {
        public PlayerEventArgs(int amount)
        {
            Amount = amount;
        }
        public int Amount;
    }

    public void DepleteHp(int amount)
    {
        currentHp -= amount;
        if (HpDeplete != null)
        {
            HpDeplete(this, new PlayerEventArgs(currentHp));
        }

        if (currentHp <= 0)
        {
            GameEnd();
        }
    }

    public void GameEnd()
    {
        gm.playerLoses = true;
        gm.EndScreen.SetActive(true);
        gm.EndScore.text = score.ToString();
        Time.timeScale = 0f;
    }

    public void AddScore(int amount)
    {
        score += amount;
        if (ScoreAdd != null)
        {
            ScoreAdd(this, new PlayerEventArgs(score));
        }
    }

    private void Event_DeplateHp(object sender, PlayerEventArgs e)
    {
        if(textHp != null && textHp.text != null)
        {
            textHp.text = e.Amount.ToString();
        }
        
    }

    private void Event_AddScore(object sender, PlayerEventArgs e)
    {
        if (textScore != null && textScore.text != null)
        {
            textScore.text = e.Amount.ToString();
        }

    }
}


