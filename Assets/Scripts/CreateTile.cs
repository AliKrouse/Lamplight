using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateTile : MonoBehaviour
{
    public GameObject[] tiles;
    private Vector2 spawnPoint;
    
	void Start ()
    {
        spawnPoint = transform.GetChild(0).position;
	}
	
	void Update ()
    {
        if (TileAlreadyExists())
            GetComponent<BoxCollider2D>().enabled = false;
        else
            GetComponent<BoxCollider2D>().enabled = true;
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Instantiate(tiles[RandomTile()], spawnPoint, Quaternion.identity);

            for (int i = 0; i < transform.parent.childCount; i++)
            {
                transform.parent.GetChild(i).gameObject.SetActive(false);
            }
        }
    }

    private int RandomTile()
    {
        return Random.Range(0, tiles.Length);
    }

    private bool TileAlreadyExists()
    {
        GameObject[] forest = GameObject.FindGameObjectsWithTag("Ground");
        bool foundTile = false;
        foreach (GameObject g in forest)
        {
            if (Vector3.Distance(g.transform.position, spawnPoint) < float.Epsilon)
                foundTile = true;
        }
        return foundTile;
    }
}
