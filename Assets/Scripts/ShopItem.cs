using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopItem : MonoBehaviour, IInventoryItem
{
    public string ObjectTag => gameObject.tag;

    public Sprite _Image = null;
    public Sprite Image => _Image;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
