using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalThief : MonoBehaviour
{
    [SerializeField]
    private GameObject[] waypoints;
    private int curwaypoint = 0;
    public float speed = 3;
    private int curh, bfhurth;
    private int havespeedupyet = 0;

    private void Start()
    {
        curh = this.GetComponentInChildren<health>().curH;
        bfhurth = curh;

    }

    private void Update()
    {
        curh = this.GetComponentInChildren<health>().curH;
        Vector3 endPosition = waypoints[curwaypoint].transform.position;
        // 2 
        transform.position = Vector3.MoveTowards(transform.position, endPosition, Time.deltaTime * speed);
        // 3 
        if (transform.position.Equals(endPosition))
        {
            if (curwaypoint < waypoints.Length - 1)
            {
                curwaypoint++;
            }
            else
            {
                curwaypoint = 0;
            }
        }

        if (bfhurth != curh)
        {
            Debug.Log("hurt");
            if (havespeedupyet == 0)
            {
                Debug.Log("hurt2");
                havespeedupyet = 1;
                StartCoroutine(speedupcolddown());
            }
            bfhurth = curh;
        }
    }


    IEnumerator speedupcolddown()
    {
        float orginspeed = speed;
        speed *= 2;
        yield return new WaitForSeconds(2);
        speed = orginspeed;
        havespeedupyet = 0;
    }
}
