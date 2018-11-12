using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public GameObject followPoint;
    public float speed;
    public float backWall;
    
	void Start ()
    {
		
	}
	
	void Update ()
    {
        Vector3 fp = new Vector3(followPoint.transform.position.x, 0, -10);
        transform.position = Vector3.MoveTowards(transform.position, fp, Time.deltaTime * speed);
    }
}
