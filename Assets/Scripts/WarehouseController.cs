using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarehouseController : MonoBehaviour
{
    public GameObject itemObj;
    public Inventory inventory;
    void Start()
    {
        
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
}
