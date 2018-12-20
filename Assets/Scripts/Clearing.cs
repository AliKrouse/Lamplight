using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clearing : MonoBehaviour
{
    private GameObject interact;
    private GameObject player;
    public Events events;

	void Start ()
    {
        interact = transform.GetChild(0).gameObject;
        player = GameObject.FindGameObjectWithTag("Player");
        events = GameObject.FindGameObjectWithTag("GameController").GetComponent<Events>();
	}
	
	void Update ()
    {
        if (Input.GetKeyDown(KeyCode.E) && interact.activeSelf)
        {
            Sit();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            interact.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            interact.SetActive(false);
        }
    }

    private void Sit()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject e in enemies)
        {
            e.GetComponent<Enemy>().Die();
        }

        interact.SetActive(false);
        player.GetComponent<PlayerController>().enabled = false;
        events.enabled = true;
        events.clearing = this;

        for (int i = 0; i < 3; i++)
        {
            events.gameObject.transform.GetChild(i).gameObject.SetActive(true);
        }
    }

    public void Stand()
    {
        interact.SetActive(false);
        player.GetComponent<PlayerController>().enabled = true;
        events.enabled = false;
        this.enabled = false;

        for (int i = 0; i < 3; i++)
        {
            events.gameObject.transform.GetChild(i).gameObject.SetActive(false);
        }
    }
}
