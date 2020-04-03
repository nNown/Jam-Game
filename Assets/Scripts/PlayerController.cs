using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

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

    public Inventory inventory;

    private void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        gm = GameMaster.GM;
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

        if (!Input.GetKey((KeyCode)GameMaster.keysEnum.KeyUp) && !Input.GetKey((KeyCode)GameMaster.keysEnum.KeyDown) && !Input.GetKey((KeyCode)GameMaster.keysEnum.KeyLeft) && !Input.GetKey((KeyCode)GameMaster.keysEnum.KeyRight) )
        {
            anim.SetBool("Idle", true);
        }
        else
        {
            anim.SetBool("Idle", false);
        }

        if (!gm.playerIsBusy)
        {
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

            if(Input.GetKeyDown((KeyCode)GameMaster.keysEnum.KeyAction) && gm.nearShelf != null)
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

            }

            moveVelocity = moveInput.normalized * speed;
        }
        else
        {
            if (Input.GetKeyDown((KeyCode)GameMaster.keysEnum.KeyRemoveItem))
            {
                inventory.RemoveLastItem();
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
            warhouseGui.SetActive(true);
            warhouseGui.transform.Find("Buttons").GetChild(0).GetComponent<Button>().Select();
            warhouseGui.transform.Find("Buttons").GetChild(0).GetComponent<Button>().OnSelect(null);
            gm.playerIsBusy = true;

        }

        if (collision.gameObject.CompareTag("Shelf"))
        {
            floatingTextHelper.SetActive(true);
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Warehouse"))
        {
            warhouseGui.SetActive(false);
            gm.playerIsBusy = false;
        }

        if (collision.gameObject.CompareTag("Shelf"))
        {
            floatingTextHelper.SetActive(false);
        }
    }

}
