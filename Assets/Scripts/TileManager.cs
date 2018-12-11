using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour
{
    public float dangerLevel;
    public float dlIncrease;

    private GameObject[] enemyConfigs;
    private List<GameObject> availableConfigs;
    
	void Start ()
    {
        enemyConfigs = Resources.LoadAll<GameObject>("Enemy Tiles");
        dangerLevel = 1;
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

    void SetAvailableConfigs()
    {
        availableConfigs.Clear();
        for (int i = 0; i < enemyConfigs.Length; i++)
        {
            if (enemyConfigs[i].GetComponent<TileTags>().dangerLevel <= dangerLevel)
                availableConfigs.Add(enemyConfigs[i]);
        }
    }

    public void CreateEnemies(Vector2 spawnPoint)
    {
        SetAvailableConfigs();
        int randomConfig = Random.Range(0, availableConfigs.Count);
        Instantiate(availableConfigs[randomConfig], spawnPoint, Quaternion.identity);
    }
}
