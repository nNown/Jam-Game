using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class Inventory : MonoBehaviour
{
    private const int SLOTS = 3;
    private List<IInventoryItem> mItems = new List<IInventoryItem>();
    public event EventHandler<InventoryEventArgs> ItemAdded;
    public event EventHandler ItemsRemoved;
    public event EventHandler LastItemRemoved;
    public event EventHandler<LastItemWithTagRemovedEventArgs> LastItemWithTagRemoved;

    public List<IInventoryItem> MItems { get { return mItems; } }
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

    public void RemoveLastItem()
    {
        if (mItems.Count > 0)
        {
            mItems.RemoveAt(mItems.Count - 1);

            if (LastItemRemoved != null)
            {
                LastItemRemoved(this, new EventArgs());
            }
        }
    }

    public void RemoveLastItem(string tag)
    {
        List<IInventoryItem> itemsToDelete = new List<IInventoryItem>();
        if (mItems.Count > 0)
        {
            foreach(IInventoryItem item in mItems.AsEnumerable().Reverse())
            {
                if(item.ObjectTag == tag)
                {
                    itemsToDelete.Add(item);
                    break;
                }
            }
            if (itemsToDelete.Count > 0)
            {
                mItems.Remove(itemsToDelete.First());
                LastItemWithTagRemoved(this, new LastItemWithTagRemovedEventArgs(tag));
            }
        }
    }
}
