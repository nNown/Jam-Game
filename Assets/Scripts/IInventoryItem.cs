using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public interface IInventoryItem 
{
    string ObjectTag { get; }
    Sprite Image { get; }
}

public class InventoryEventArgs : EventArgs
{
    public InventoryEventArgs(IInventoryItem item)
    {
        Item = item;
    }
    public IInventoryItem Item;
}

public class LastItemWithTagRemovedEventArgs : EventArgs
{
    public LastItemWithTagRemovedEventArgs(string tag)
    {
        Tag = tag;
    }
    public string Tag;
}
