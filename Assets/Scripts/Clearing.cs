using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clearing : MonoBehaviour
{
    private Transform playerPoint, leavingPoint;
    bool Continuing;
    private PlayerController player;

    public GameObject lastForest;
    public GameObject forest, clearing;

    public float minLength, maxLength;
    public static int level;

    public GameObject ui;

	void Start ()
    {
        playerPoint = transform.GetChild(0);
        leavingPoint = transform.GetChild(1);
	}
	
	void Update ()
    {
        if (Continuing)
        {
            player.gameObject.transform.position = Vector2.MoveTowards(player.gameObject.transform.position, leavingPoint.position, Time.deltaTime * player.speed);
        }

        //if (Input.GetKeyDown(KeyCode.Space))
        //    Continue();
    }

    public void Continue()
    {
        Continuing = true;
        Debug.Log("continuing");

        Destroy(lastForest);

        GameObject f = Instantiate(forest, transform.position, Quaternion.identity);
        float length = Random.Range(minLength, maxLength);
        f.GetComponent<SpriteRenderer>().size = new Vector2(length, 9);
        f.GetComponent<BoxCollider2D>().size = new Vector2(length - 10, 9);
        f.GetComponent<CapsuleCollider2D>().offset = new Vector2(-length / 2, 0);
        float xPos = transform.position.x + 9 + (length / 2);
        f.transform.position = new Vector2(xPos, 0.98f);
        f.GetComponent<Forest>().lastClearing = this.gameObject;
        float percent = Random.Range(1, 100);
        int level = 1;
        if (percent <= 40)
            level = 1;
        if (percent > 40 && percent <= 80)
            level = 2;
        if (percent > 80)
            level = 3;
        f.GetComponent<Forest>().dangerLevel = level;
        Debug.Log("created level " + level + " forest");

        GameObject c = Instantiate(clearing, transform.position, Quaternion.identity);
        float xPos2 = f.transform.position.x + (length / 2) + 9;
        c.transform.position = new Vector2(xPos2, 0.98f);
        c.GetComponent<Clearing>().lastForest = f;
        c.GetComponent<Clearing>().ui = ui;

        ui.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            lastForest.GetComponent<Forest>().StopAllCoroutines();
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
            foreach (GameObject e in enemies)
                e.GetComponent<Enemy>().Die();

            ui.SetActive(true);
            ui.GetComponent<Events>().clearing = this;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            player = collision.GetComponent<PlayerController>();
            player.enabled = false;
            if (!Continuing)
                collision.gameObject.transform.position = Vector2.MoveTowards(collision.transform.position, playerPoint.position, Time.deltaTime * player.speed);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerController player = collision.GetComponent<PlayerController>();
            player.enabled = true;
        }
    }
}
