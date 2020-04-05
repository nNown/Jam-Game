using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System.Linq;

public class NavMeshGameObj : MonoBehaviour
{
    NavMeshAgent navMeshAgent;
    GameMaster gm;
    Dictionary<string, List<PlaceOfPickUpInfo>> PlaceOfPickUpDict = new Dictionary<string, List<PlaceOfPickUpInfo>>();
    Vector3 whereToGo;
    List<string> productsTagsList;
    System.Random random;
    string randomTag;
    public CustomerController customerController;
    [System.NonSerialized] public int customerState;
    public int tries;
    int triesLeft;
    public Transform exitLocation;
    int randListElement = 0;
    PlaceOfPickUpInfo placeTemp;
    [System.NonSerialized]
    public bool goHome;



    void Start()
    {
        random = new System.Random();
        InitializePlaceOfPickUpDict();
        gm = GameMaster.GM;
        navMeshAgent = GetComponent<NavMeshAgent>();
        SetNextDirection();
        customerState = (int)GameMaster.customerState.Shopping;
        triesLeft = tries;
        placeTemp = gameObject.AddComponent<PlaceOfPickUpInfo>();
    }

    private void SetNextDirection()
    {
        Vector3 nextDest;
        if(customerState == (int)GameMaster.customerState.HasNotFoundProduct)
        {
            whereToGo = exitLocation.position;
        }else if (customerState == (int)GameMaster.customerState.HasFoundProduct)
        {
            if (goHome)
            {
                whereToGo = exitLocation.position;
            }
            else
            {
                whereToGo = gm.cashGameObject.transform.position;
            }  
        }
        else
        {
            while (true)
            {
                randomTag = productsTagsList[random.Next(productsTagsList.Count())];
                randListElement = random.Next(PlaceOfPickUpDict[randomTag].Count);
                nextDest = PlaceOfPickUpDict[randomTag][randListElement].transform.position;
                if (nextDest != whereToGo)
                {
                    whereToGo = PlaceOfPickUpDict[randomTag][randListElement].transform.position;
                    break;
                }
            }
        }


        
    }

    private void InitializePlaceOfPickUpDict()
    {
        GameObject[] placesOfPickUp = GameObject.FindGameObjectsWithTag("ShelfPlaceOfPickUp");
        
        for (int i = 0; i < placesOfPickUp.Length; i++)
        {
            placeTemp = placesOfPickUp[i].GetComponent<PlaceOfPickUpInfo>();
            if (!PlaceOfPickUpDict.ContainsKey(placeTemp.ProductTypeObject.tag))
            {
                PlaceOfPickUpDict.Add(placeTemp.ProductTypeObject.tag, new List<PlaceOfPickUpInfo> { placeTemp });
            }
            else
            {
                PlaceOfPickUpDict[placeTemp.ProductTypeObject.tag].Add(placeTemp);
            }
        }
        productsTagsList = PlaceOfPickUpDict.Keys.ToList();
    }

    void Update()
    {
        if (!navMeshAgent.isStopped)
        {
            GoToTarget();
        }

    }
    void GoToTarget()
    {
        navMeshAgent.SetDestination(whereToGo);
        if (!navMeshAgent.pathPending)
        {
            if (navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance)
            {
                if (!navMeshAgent.hasPath || navMeshAgent.velocity.sqrMagnitude == 0f)
                {
                    StartCoroutine(WaitForStuff());
                }
            }
        }
    }
    IEnumerator WaitForStuff()
    {

        if (customerState == (int)GameMaster.customerState.Shopping)
        {
            navMeshAgent.isStopped = true;
            if (customerController.GetItemFromShelf())
            {
                customerState = (int)GameMaster.customerState.HasFoundProduct;
            }
            else
            {
                triesLeft--;
                if(triesLeft <= 0)
                {
                    customerState = (int)GameMaster.customerState.HasNotFoundProduct;
                }
            }
            yield return new WaitForSeconds(4f);
            navMeshAgent.isStopped = false;
        }
        SetNextDirection();
    }
}
