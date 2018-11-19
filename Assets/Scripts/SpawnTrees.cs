using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnTrees : MonoBehaviour
{
    private SpriteRenderer sr;
    private RandomizeTree rt;
    private OrganizeRenderer or;

    private GameObject player;

    private bool active;

	void Start ()
    {
        sr = GetComponent<SpriteRenderer>();
        rt = GetComponent<RandomizeTree>();
        or = GetComponent<OrganizeRenderer>();
        player = GameObject.FindGameObjectWithTag("Player");
	}

    private void Update()
    {
        if (Vector2.Distance(transform.position, player.transform.position) < 12 && !active)
        {
            sr.enabled = true;
            rt.enabled = true;
            or.enabled = true;
            active = true;
        }
        if (Vector2.Distance(transform.position, player.transform.position) >= 12 && active)
        {
            sr.enabled = false;
            rt.enabled = false;
            or.enabled = false;
            active = false;
        }
    }
}
