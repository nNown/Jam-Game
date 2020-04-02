using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float speed;
    KeyCode[] keys = {KeyCode.W, KeyCode.S, KeyCode.A, KeyCode.D};

    private Animator anim;
    private Rigidbody2D rb;
    private Vector2 moveVelocity;
    public GameObject warhouseGui;
    public GameObject floatingTextHelper;

    public Inventory inventory;

    private void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
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
        if (anim.GetBool("Death") == true) return;

        Vector2 moveInput = new Vector2();

        if (!Input.GetKey(keys[0]) && !Input.GetKey(keys[1]) && !Input.GetKey(keys[2]) && !Input.GetKey(keys[3]))
        {
            anim.SetBool("Idle", true);
        }
        else
        {
            anim.SetBool("Idle", false);
        }

        if (Input.GetKey(keys[0]))
        {
            moveInput = Vector2.up;
            anim.SetInteger("Speed", 1);
        }
        else if (Input.GetKey(keys[1]))
        {
            moveInput = Vector2.down;
            anim.SetInteger("Speed", 2);
        }
        else if (Input.GetKey(keys[3]))
        {
            moveInput = Vector2.right;
            anim.SetInteger("Speed", 3);
        }
        else if (Input.GetKey(keys[2]))
        {
            moveInput = Vector2.left;
            anim.SetInteger("Speed", 4);
        }

        moveVelocity = moveInput.normalized * speed;
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
        }

        if (collision.gameObject.CompareTag("Shelf"))
        {
            floatingTextHelper.SetActive(false);
        }
    }
}
