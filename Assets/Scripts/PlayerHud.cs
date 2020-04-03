using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Linq;

public class PlayerHud : MonoBehaviour
{
    public Inventory Inventory;
    public Transform inventoryPanel;
    GameMaster gm;
    void Start()
    {
        gm = GameMaster.GM;
        Inventory.ItemAdded += InventoryScript_ItemAdded;
        Inventory.ItemsRemoved += InventoryScript_ItemsRemoved;
        Inventory.LastItemRemoved += InventoryScript_LastItemRemoved;
        Inventory.LastItemWithTagRemoved += InventoryScript_LastItemWithTagRemoved;
    }

    private void InventoryScript_ItemAdded(object sender, InventoryEventArgs e)
    {
        foreach(Transform slot in inventoryPanel)
        {
            Image image = slot.GetComponent<Image>();
            if (!image.enabled)
            {
                image.enabled = true;
                image.sprite = e.Item.Image;
                slot.tag = e.Item.ObjectTag;

                break;
            }
        }
    }

    private void InventoryScript_ItemsRemoved(object sender, EventArgs e)
    {
        foreach (Transform slot in inventoryPanel)
        {
            Image image = slot.GetComponent<Image>();
            image.sprite = null;
            image.enabled = false;
        }
    }

    private void InventoryScript_LastItemRemoved(object sender, EventArgs e)
    {
        List<Image> slotsList = new List<Image>();
        foreach (Transform slot in inventoryPanel)
        {
            slotsList.Add(slot.GetComponent<Image>());
        }
        foreach (Image slot in slotsList.AsEnumerable().Reverse())
        {
            if (slot.sprite != null && slot.enabled)
            {
                slot.sprite = null;
                slot.enabled = false;
                break;
            }

        }
    }

    private void InventoryScript_LastItemWithTagRemoved(object sender, LastItemWithTagRemovedEventArgs e)
    {
        foreach (Transform slot in inventoryPanel)
        {
            if (slot.GetComponent<Image>().sprite != null && slot.GetComponent<Image>().enabled && slot.CompareTag(e.Tag))
            {
                slot.GetComponent<Image>().sprite = null;
                slot.GetComponent<Image>().enabled = false;
                break;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
