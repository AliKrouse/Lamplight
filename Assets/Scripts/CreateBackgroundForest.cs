using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateBackgroundForest : MonoBehaviour
{
    private GameObject player;
    
	void Start ()
    {
        player = GameObject.FindGameObjectWithTag("Player");
	}
	
	void Update ()
    {
        transform.position = RoundedPosition();
	}

    private Vector3 RoundedPosition()
    {
        return new Vector3(Mathf.RoundToInt(player.transform.position.x / 20) * 20, Mathf.RoundToInt(player.transform.position.y / 20) * 20, 50);
    }
}
