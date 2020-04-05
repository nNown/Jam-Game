using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerSpawner : MonoBehaviour
{
    GameMaster gm;
    public int maxCustomers;
    public GameObject customerPrefab;
    GameObject spawnedCustomer;
    bool canSpawn = true;
    
    void Start()
    {
        gm = GameMaster.GM;

    }

    // Update is called once per frame
    void Update()
    {

        StartCoroutine(SpawnRutine());

    }
    IEnumerator SpawnRutine()
    {
        if (canSpawn && gm.currentCustomersNumber <= maxCustomers)
        {
            canSpawn = false;
            spawnedCustomer = GameObject.Instantiate(customerPrefab, transform.position, transform.rotation);
            spawnedCustomer.GetComponent<NavMeshGameObj>().exitLocation = transform;
            gm.currentCustomersNumber++;
            yield return new WaitForSeconds(5f);
            canSpawn = true;
        }
    }
}
