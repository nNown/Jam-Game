using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarehouseController : MonoBehaviour
{
    public GameObject itemObj;
    public Inventory inventory;
    GameMaster gm;
    private Vector2 moveVelocity;

    private void Start()
    {
        gm = GameMaster.GM;
    }
    public void AddPaperToInventory()
    {
        IInventoryItem item = itemObj.gameObject.GetComponent<IInventoryItem>();
        if (item != null)
        {
            inventory.AddItem(item);
        }
    }


    public void RemoveItems()
    {
        inventory.RemoveItems();
    }

    public void RemoveLastItem()
    {
        inventory.RemoveItems();
    }
    public void ExitWarehouse()
    {
        gm.playerObject.transform.position = gm.playerObject.transform.position + Vector3.down * 0.2f;
    }
}
