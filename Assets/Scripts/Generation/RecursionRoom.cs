using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class RecursionRoom : MonoBehaviour
{
    public int remainingRooms = 0;

    private void Start()
    {
        GetComponent<CircleCollider2D>().radius = Random.Range(1, 3f);
        if(GetComponent<SpringJoint2D>()!=null)
            GetComponent<SpringJoint2D>().distance = Random.Range(6, 8f);

        if (remainingRooms > 0)
            Generate();
    }

    private void Generate()
    {
        if(remainingRooms > 0)
        {
            for (int i = 0; i < Random.Range(1, 5); i++)
            {
                if (remainingRooms <= 0)
                    break;
                else
                {
                    Instantiate(Resources.Load("Prefabs/RecRoom") as GameObject, transform.position, Quaternion.identity, transform);
                    remainingRooms--;
                }
            }
            
            if (remainingRooms > 0)
            {
                for (int i = 0; i < remainingRooms; i++)
                {
                    transform.GetChild(Random.Range(0, transform.childCount)).GetComponent<RecursionRoom>().remainingRooms += 1;
                }
                remainingRooms = 0;
            }

            if (transform.childCount > 0)
            {
                foreach (Transform child in transform)
                {
                    child.GetComponent<SpringJoint2D>().connectedBody = gameObject.GetComponent<Rigidbody2D>();
                    child.GetComponent<RecursionRoom>().Generate();
                }
            }
        }
    }
}
