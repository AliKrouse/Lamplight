using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    //attack follows a path set at runtime with a set speed
    //attack deals damage set at runtime

    public Transform[] path;
    private int i;
    public float speed;
    public float damage;
    public bool ranged;
    
	void Start ()
    {
		
	}
	
	void Update ()
    {
        if (i < path.Length)
        {
            Transform point = path[i];
            transform.position = Vector2.MoveTowards(transform.position, point.position, Time.deltaTime * speed);
            //Debug.DrawLine(transform.position, point.position, Color.red);
            if (Vector2.Distance(transform.position, point.position) < float.Epsilon)
            {
                i++;
            }
        }
        else
            StartCoroutine(Die());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (gameObject.CompareTag("Player"))
        {
            if (collision.gameObject.CompareTag("Enemy"))
                collision.GetComponent<Enemy>().hp -= damage;
        }
        if (gameObject.CompareTag("Enemy"))
        {
            if (collision.gameObject.CompareTag("Player"))
                collision.GetComponent<PlayerController>().hp -= damage;
        }
    }

    private IEnumerator Die()
    {
        gameObject.GetComponent<ParticleSystem>().Stop();
        gameObject.GetComponent<CircleCollider2D>().enabled = false;
        yield return new WaitForSeconds(1);
        if (ranged)
            Destroy(path[0].gameObject);
        Destroy(this.gameObject);
    }
}
