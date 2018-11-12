using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

public class PlayerController : MonoBehaviour
{
    //move left and right, jump, maybe duck? and 3 attacks
    //attacks are light, heavy, and range. each creates the same attack object and sets some variables in Attack.cs

    private Player p;
    private Rigidbody2D rb;
    private SpriteRenderer sr;

    public float speed;
    public float jumpForce;
    private bool isGrounded;
    public GameObject flame;

    public float maxHp;
    public float hp;
    public float multiplier;
    private GameObject light;
    public float lightSize;

    public Transform[] LPath, HPath, RPath;
    public float LDmg, HDmg, RDmg;

    public float LCooldown, HCooldown, RCooldown;
    private float cooldown;
    private bool canAttack, canFire;

    private Coroutine charge;
    private bool atkCharged;
    public GameObject chargeEffect;
    private ParticleSystem activeCharger;

	void Start ()
    {
        p = ReInput.players.GetPlayer(0);
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        light = transform.GetChild(4).gameObject;

        isGrounded = true;
        canAttack = true;
        canFire = true;
	}
	
	void Update ()
    {
        Move();
        Attack();
        Aim();

        if (hp <= 0)
            Die();

        //lightSize = hp / 1.5f;
        //light.transform.localScale = new Vector2(lightSize, lightSize);
        multiplier = hp / 10;
	}

    void Move()
    {
        if (p.GetAxis("Horizontal") > float.Epsilon)
        {
            rb.velocity = new Vector2(speed, rb.velocity.y);
            sr.flipY = false;
        }
        else if (p.GetAxis("Horizontal") < -float.Epsilon)
        {
            rb.velocity = new Vector2(-speed, rb.velocity.y);
            sr.flipY = true;
        }
        else
            rb.velocity = new Vector2(0, rb.velocity.y);

        if (p.GetAxis("Vertical") > float.Epsilon)
        {
            rb.velocity = new Vector2(rb.velocity.x, speed);
        }
        else if (p.GetAxis("Vertical") < -float.Epsilon)
        {
            rb.velocity = new Vector2(rb.velocity.x, -speed);
        }
        else
            rb.velocity = new Vector2(rb.velocity.x, 0);
    }

    void Attack()
    {
        if (p.GetButtonDown("Atk") && canAttack)
        {
            charge = StartCoroutine(ChargeAttack());
        }
        if (p.GetButtonUp("Atk"))
        {
            StopCoroutine(charge);
            if (activeCharger != null)
                activeCharger.Stop();

            if (atkCharged)
            {
                GameObject f = Instantiate(flame, transform.position, Quaternion.identity);
                f.GetComponent<Attack>().path = new Transform[HPath.Length];
                for (int i = 0; i < HPath.Length; i++)
                    f.GetComponent<Attack>().path[i] = HPath[i];
                f.GetComponent<Attack>().damage = HDmg * multiplier;
                f.GetComponent<Attack>().speed *= 1.5f;
                f = null;
                cooldown = HCooldown;
                StartCoroutine(CooldownAttack());
            }
            else
            {
                GameObject f = Instantiate(flame, transform.position, Quaternion.identity);
                f.GetComponent<Attack>().path = new Transform[LPath.Length];
                for (int i = 0; i < LPath.Length; i++)
                    f.GetComponent<Attack>().path[i] = LPath[i];
                f.GetComponent<Attack>().damage = LDmg * multiplier;
                f = null;
                cooldown = LCooldown;
                StartCoroutine(CooldownAttack());
            }

            atkCharged = false;
        }
        if (p.GetButtonDown("RAtk") && canFire)
        {
            GameObject f = Instantiate(flame, transform.position, Quaternion.identity);
            f.GetComponent<Attack>().path = new Transform[RPath.Length];
            for (int i = 0; i < RPath.Length; i++)
                f.GetComponent<Attack>().path[i] = Instantiate(RPath[i], RPath[i].transform.position, Quaternion.identity);
            f.GetComponent<Attack>().damage = RDmg * multiplier;
            f.GetComponent<Attack>().ranged = true;
            f = null;
            cooldown = RCooldown;
            StartCoroutine(CooldownFire());
        }
    }

    void Aim()
    {
        Vector3 mousepos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 dir = mousepos - transform.position;
        float angle = (Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg);
        for (int i = 0; i < 4; i++)
            transform.GetChild(i).transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
            isGrounded = true;
    }

    private IEnumerator CooldownAttack()
    {
        canAttack = false;
        yield return new WaitForSeconds(cooldown);
        canAttack = true;
    }

    private IEnumerator CooldownFire()
    {
        canFire = false;
        yield return new WaitForSeconds(RCooldown);
        canFire = true;
    }

    void Die()
    {
        sr.color = Color.red;
        this.enabled = false;
    }

    private IEnumerator ChargeAttack()
    {
        atkCharged = false;
        yield return new WaitForSeconds(0.5f);
        GameObject g = Instantiate(chargeEffect, transform.position, Quaternion.identity, transform);
        activeCharger = g.GetComponent<ParticleSystem>();
        yield return new WaitForSeconds(1.5f);
        atkCharged = true;
    }
}
