using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomCreator : MonoBehaviour
{
    [SerializeField]
    private int maxPaths = 4;

    public int rooms = 0;

    public GameObject room;

    [SerializeField]
    private bool isStart;

    private void Start()
    {
        //if (isStart)
        //{
        //    Generate();
        //}
    }

    private void OnEnable()
    {
        Generate();
    }

    public void Generate()
    {
        print(rooms.ToString());

        //GetComponent<CircleCollider2D>().radius = Random.Range(1, 3);

        if (rooms <= 0)
            return;

        for(int i = 0; i < Random.Range(1, maxPaths+1); i++)
        {
            if (rooms <= 0)
                break;
            
            Instantiate(room, transform.position, Quaternion.identity, transform);
            rooms--;

            print("2");
        }

        if(rooms > 0)
        {
            for(int i = 0; i < rooms; i++)
            {
                transform.GetChild(Random.Range(0, transform.childCount)).GetComponent<RoomCreator>().rooms += 1;
                print("3");
            }

            rooms = 0;
        }

        foreach(Transform child in transform)
        {
            //child.GetComponent<SpringJoint2D>().connectedBody = GetComponent<Rigidbody2D>();
            child.GetComponent<RoomCreator>().enabled = true;
            print("4");
        }

        print("5");
    }
}
