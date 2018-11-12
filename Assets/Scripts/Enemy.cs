using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    //move towards player, when at variable range stop and preform attack

    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private GameObject attackPath;
    private GameObject player;
    public float hp;
    public GameObject explosion;

    public float range;
    public float speed;
    public GameObject flame;

    public Transform[] path;
    public float dmg;
    public float interval;

    private Coroutine atk;

    public float height;
    public bool flying, aiming;
    private Vector2 followPos;

	void Start ()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        attackPath = transform.GetChild(0).gameObject;
        player = GameObject.FindGameObjectWithTag("Player");
	}
	
	void Update ()
    {
        if (flying)
            followPos = player.transform.position;
        else
            followPos = new Vector2(player.transform.position.x, transform.position.y);
        float d = Vector2.Distance(transform.position, followPos);
        if (d > range)
        {
            transform.position = Vector2.MoveTowards(transform.position, followPos, Time.deltaTime * speed);
            if (atk != null)
                StopCoroutine(atk);
        }
        else
        {
            if (atk == null)
                atk = StartCoroutine(AttackPlayer());
        }

        if (hp <= 0)
            Die();

        if (aiming)
        {
            if (player.transform.position.x < transform.position.x)
            {
                sr.flipX = false;
            }
            if (player.transform.position.x > transform.position.x)
            {
                sr.flipX = true;
            }
            
            Vector2 dir = player.transform.position - transform.position;
            float angle = (Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg) + 180;
            attackPath.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
        else
        {
            if (player.transform.position.x < transform.position.x)
            {
                sr.flipX = false;
                attackPath.transform.localScale = new Vector3(-1, 1, 1);
            }
            if (player.transform.position.x > transform.position.x)
            {
                sr.flipX = true;
                attackPath.transform.localScale = new Vector3(1, 1, 1);
            }
        }
	}

    private IEnumerator AttackPlayer()
    {
        while (true)
        {
            GameObject f = Instantiate(flame, transform.position, Quaternion.identity);
            f.GetComponent<Attack>().path = new Transform[path.Length];
            for (int i = 0; i < path.Length; i++)
                f.GetComponent<Attack>().path[i] = path[i];
            f.GetComponent<Attack>().damage = dmg;
            f = null;
            yield return new WaitForSeconds(interval);
        }
    }

    public void Die()
    {
        Instantiate(explosion, transform.position, Quaternion.identity);
        Destroy(this.gameObject);
    }
}
