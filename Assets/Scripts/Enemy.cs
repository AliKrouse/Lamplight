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
    public bool flying;
    private Vector2 followPos;

    public List<Footstep> footsteps;

    private Animator anim;

	void Start ()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        attackPath = transform.GetChild(0).gameObject;
        player = GameObject.FindGameObjectWithTag("Player");

        anim = GetComponent<Animator>();
	}
	
	void Update ()
    {
        //if (flying)
        //    followPos = player.transform.position;
        //else
        //    followPos = new Vector2(player.transform.position.x, transform.position.y);
        if (flying)
        {
            followPos = player.transform.position;
        }
        else
        {
            if (Vector2.Distance(player.transform.position, transform.position) < 6)
            {
                followPos = player.transform.position;
            }
            else
            {
                if (followPos == null)
                    SeekFootstep();
                if (Vector2.Distance(followPos, transform.position) <= float.Epsilon)
                {
                    SeekFootstep();
                }
            }
        }
        Debug.DrawLine(transform.position, followPos, Color.red);

        float d = Vector2.Distance(transform.position, player.transform.position);
        if (d > range)
        {
            transform.position = Vector2.MoveTowards(transform.position, followPos, Time.deltaTime * speed);
            if (atk != null)
                StopCoroutine(atk);

            anim.SetBool("moving", true);
        }
        else
        {
            if (atk == null)
                atk = StartCoroutine(AttackPlayer());

            anim.SetBool("moving", false);
        }

        if (hp <= 0)
            Die();

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

    private IEnumerator AttackPlayer()
    {
        while (true)
        {
            anim.SetTrigger("attack");
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

    void SeekFootstep()
    {
        float lastDistance = 100;
        int currentLowest = 0;
        for (int i = 0; i < footsteps.Count; i++)
        {
            if (footsteps[i] != null)
            {
                if (Vector2.Distance(footsteps[i].gameObject.transform.position, transform.position) < lastDistance)
                {
                    lastDistance = Vector2.Distance(footsteps[i].gameObject.transform.position, transform.position);
                    currentLowest = i;
                }
            }
        }
        followPos = footsteps[currentLowest].gameObject.transform.position;
        footsteps.Remove(footsteps[currentLowest]);
    }
}
