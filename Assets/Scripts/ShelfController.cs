using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class ShelfController : MonoBehaviour
{
    public GameObject shelfProductType;
    public int quantity;
    public GameObject itemsPanel;
    GameMaster gm;
    List<Image> producstsOnShelf = new List<Image>();

    void Start()
    {
        gm = GameMaster.GM;
        for(int i = 0; i < quantity; i++)
        {
            GameObject newSelected = Instantiate(shelfProductType, itemsPanel.transform.position, itemsPanel.transform.rotation) as GameObject;
            newSelected.transform.SetParent(itemsPanel.transform);
            newSelected.GetComponent<Image>().enabled = false;
            producstsOnShelf.Add(newSelected.GetComponent<Image>());
        }


    }

    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            gm.nearShelf = gameObject;
        }
        
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            gm.nearShelf = gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            gm.nearShelf = null;
        }
    }

    public bool CheckIsPlaceOnShelf()
    {
        foreach (Image child in producstsOnShelf)
        {
            if (!child.enabled)
            {
                return true;
            }
        }
        return false;
    }

    public void PutItemOnShelf()
    {
        foreach (Image child in producstsOnShelf)
        {
            if (!child.enabled)
            {
                child.enabled = true;
                break;
            }
        }
    }

    public bool RemoveItemFromShelf()
    {
        List<Transform> childs = new List<Transform>();
        foreach (Transform child in itemsPanel.transform)
        {
            childs.Add(child);
        }
        foreach(Transform child in childs.AsEnumerable().Reverse())
        {
            if (child.GetComponent<Image>().enabled)
            {
                child.GetComponent<Image>().enabled = false;
                return true;
            }
        }
        return false;
    }
}
