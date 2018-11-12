using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Forest : MonoBehaviour
{
    public GameObject lastClearing;
    public int dangerLevel;
    public GameObject[] enemies;
    public float minI, maxI;
    private GameObject enemyToSpawn;
    private Vector2 spawnPoint;
    public GameObject cam;
    
	void Awake ()
    {
        cam = Camera.main.gameObject;
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (lastClearing != null)
                Destroy(lastClearing);

            GetComponent<CapsuleCollider2D>().enabled = true;
            StartCoroutine(SpawnEnemies());
        }
    }

    private IEnumerator SpawnEnemies()
    {
        while (true)
        {
            float interval = Random.Range(minI, maxI);
            yield return new WaitForSeconds(interval);

            ChooseEnemy();
            CreateSpawnPoint();

            Instantiate(enemyToSpawn, spawnPoint, Quaternion.identity);

            //Debug.Log("spawned " + enemyToSpawn.name + " after " + interval + " seconds");
        }
    }

    private void ChooseEnemy()
    {
        int maxEnemy = 0;
        if (dangerLevel == 1)
            maxEnemy = 1;
        if (dangerLevel == 3)
            maxEnemy = 4;
        if (dangerLevel == 4)
            maxEnemy = 5;

        int enemyChoice = Random.Range(0, maxEnemy + 1);
        enemyToSpawn = enemies[enemyChoice];
    }

    private void CreateSpawnPoint()
    {
        if (enemyToSpawn != enemies[1])
        {
            float coinFlip = Random.Range(0f, 1f);
            if (coinFlip < 0.5)
                spawnPoint = new Vector2(cam.transform.position.x - 11, enemyToSpawn.GetComponent<Enemy>().height);
            if (coinFlip >= 0.5)
                spawnPoint = new Vector2(cam.transform.position.x + 11, enemyToSpawn.GetComponent<Enemy>().height);
        }
        else
        {
            float xPos = Random.Range(cam.transform.position.x - 11, cam.transform.position.x + 11);
            float ypos = 0;
            float xDif = xPos - cam.transform.position.x;
            if (Mathf.Abs(xDif) > 9.5)
                ypos = Random.Range(-2, 5);
            else
                ypos = 6;

            spawnPoint = new Vector2(xPos, ypos);
        }
    }
}
