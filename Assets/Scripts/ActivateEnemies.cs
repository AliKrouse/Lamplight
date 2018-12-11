using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateEnemies : MonoBehaviour
{
    private Enemy[] enemies;
    
	void Start ()
    {
        for (int i = 0; i < transform.childCount; i++)
            enemies[i] = transform.GetChild(i).GetComponent<Enemy>();

        foreach (Enemy e in enemies)
            e.enabled = false;
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            foreach (Enemy e in enemies)
                e.enabled = true;

            GetComponent<BoxCollider2D>().enabled = false;
        }
    }
}
