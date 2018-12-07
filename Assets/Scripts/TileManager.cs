using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour
{
    public static float dangerLevel;
    public float dlIncrease;
    
	void Start ()
    {
    }

    void Update()
    {
        if (dangerLevel < 6)
            dangerLevel += Time.deltaTime * dlIncrease;

        GameObject[] forest = GameObject.FindGameObjectsWithTag("Ground");
        foreach (GameObject g in forest)
        {
            if (Vector2.Distance(transform.position, g.transform.position) > 20)
            {
                Destroy(g);
            }
        }
    }
}
