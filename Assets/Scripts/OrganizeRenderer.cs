using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrganizeRenderer : MonoBehaviour
{
    private SpriteRenderer sr;
    
	void Start ()
    {
        sr = GetComponent<SpriteRenderer>();
	}
	
	void Update ()
    {
        sr.sortingOrder = Mathf.RoundToInt(transform.position.y * 10) * -1;
	}
}
