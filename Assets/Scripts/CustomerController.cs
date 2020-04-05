using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CustomerController : MonoBehaviour
{
    GameObject nearShelf;
    ShelfController nearShelfController;
    public NavMeshGameObj navMeshGameObj;
    GameMaster gm;
    Quaternion rotation;
    
    void Start()
    {
        gameObject.transform.Rotate(Vector3.left,-90f);
        gm = GameMaster.GM;
        rotation = transform.rotation;
    }


    void Update()
    {
     
    }
    private void LateUpdate()
    {
        transform.rotation = Quaternion.identity;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Shelf"))
        {
            nearShelf = collision.gameObject;
            nearShelfController = nearShelf.GetComponent<ShelfController>();
        }
        else if (collision.CompareTag("Exit") && (navMeshGameObj.customerState == (int)GameMaster.customerState.HasNotFoundProduct || navMeshGameObj.customerState == (int)GameMaster.customerState.HasFoundProduct) )
        {
            Destroy(gameObject.transform.parent.gameObject);
        }


    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Shelf"))
        {
            nearShelf = null;
            nearShelfController = null;
        }
    }

    private void OnDestroy()
    {
        gm.currentCustomersNumber--;
        if (navMeshGameObj.customerState == (int)GameMaster.customerState.HasFoundProduct)
        {
            gm.playerController.AddScore(100);
        }
        if (navMeshGameObj.customerState == (int)GameMaster.customerState.HasNotFoundProduct)
        {
            gm.playerController.DepleteHp(1);
        }
    }

    public bool GetItemFromShelf()
    {
        if (nearShelfController != null)
        {
            return nearShelfController.RemoveItemFromShelf();
        }
        else
        {
            return false;
        }

    }
}
