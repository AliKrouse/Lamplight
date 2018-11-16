using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnTrees : MonoBehaviour
{
    public GameObject[] trees;

    public bool tl, t, tr, l, r, bl, b, br, c;
    private float minY, maxY, minX, maxX;

	void Start ()
    {
        if (tl)
        {
            minY = transform.position.y + 3.3f;
            maxY = transform.position.y + 10;
            minX = transform.position.x - 10;
            maxX = transform.position.x - 3.3f;
            Spawn();
        }
        if (t)
        {
            minY = transform.position.y + 3.3f;
            maxY = transform.position.y + 10;
            minX = transform.position.y - 3.3f;
            maxX = transform.position.x + 3.3f;
            Spawn();
        }
        if (tr)
        {
            minY = transform.position.y + 3.3f;
            maxY = transform.position.y + 10;
            minX = transform.position.x + 3.3f;
            maxX = transform.position.x + 10;
            Spawn();
        }
        if (l)
        {
            minY = transform.position.y - 3.3f;
            maxY = transform.position.y + 3.3f;
            minX = transform.position.x - 10;
            maxX = transform.position.x - 3.3f;
            Spawn();
        }
        if (r)
        {
            minY = transform.position.y - 3.3f;
            maxY = transform.position.y + 3.3f;
            minX = transform.position.x + 3.3f;
            maxX = transform.position.x + 10;
            Spawn();
        }
        if (bl)
        {
            minY = transform.position.y - 10;
            maxY = transform.position.y - 3.3f;
            minX = transform.position.x - 10;
            maxX = transform.position.x - 3.3f;
            Spawn();
        }
        if (b)
        {
            minY = transform.position.y - 10;
            maxY = transform.position.y - 3.3f;
            minX = transform.position.x - 3.3f;
            maxX = transform.position.x + 3.3f;
            Spawn();
        }
        if (br)
        {
            minY = transform.position.y - 10;
            maxY = transform.position.y - 3.3f;
            minX = transform.position.x + 3.3f;
            maxX = transform.position.x + 10;
            Spawn();
        }
        if (c)
        {
            minY = transform.position.y - 3.3f;
            maxY = transform.position.y + 3.3f;
            minX = transform.position.x - 3.3f;
            maxX = transform.position.x + 3.3f;
            Spawn();
        }
	}

    private void Spawn()
    {
        for (int i = 0; i < 20; i++)
        {
            float yPos = Random.Range(minY, maxY);
            float xpos = Random.Range(minX, maxX);
            int randomTree = Random.Range(0, trees.Length);
            Instantiate(trees[randomTree], new Vector2(xpos, yPos), Quaternion.identity, transform);
        }
    }
}
