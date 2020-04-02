using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Inventory : MonoBehaviour
{
    private const int SLOTS = 3;
    private List<IInventoryItem> mItems = new List<IInventoryItem>();
    public event EventHandler<InventoryEventArgs> ItemAdded;
    public event EventHandler ItemsRemoved;

    public void AddItem(IInventoryItem item)
    {
        if(mItems.Count < SLOTS)
        {
                mItems.Add(item);

                if (ItemAdded != null)
                {
                    ItemAdded(this, new InventoryEventArgs(item));
                }
        }
    }

    public void RemoveItems()
    {
        if (mItems.Count > 0)
        {
            mItems.Clear();

            if (ItemsRemoved != null)
            {
                ItemsRemoved(this, new EventArgs());
            }
        }
    }
}
