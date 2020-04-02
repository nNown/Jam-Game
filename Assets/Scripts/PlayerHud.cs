using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class PlayerHud : MonoBehaviour
{
    public Inventory Inventory;
    public Transform inventoryPanel;
    void Start()
    {
        Inventory.ItemAdded += InventoryScript_ItemAdded;
        Inventory.ItemsRemoved += InventoryScript_ItemsRemoved;
    }

    private void InventoryScript_ItemAdded(object sender, InventoryEventArgs e)
    {
        //Transform inventoryPanel = transform.Find("InventoryPanel");

        foreach(Transform slot in inventoryPanel)
        {
            Image image = slot.GetComponent<Image>();
            if (!image.enabled)
            {
                image.enabled = true;
                image.sprite = e.Item.Image;

                break;
            }
        }
    }

    private void InventoryScript_ItemsRemoved(object sender, EventArgs e)
    {
        //Transform inventoryPanel = transform.Find("InventoryPanel");

        foreach (Transform slot in inventoryPanel)
        {
            Image image = slot.GetComponent<Image>();
            image.sprite = null;
            image.enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
