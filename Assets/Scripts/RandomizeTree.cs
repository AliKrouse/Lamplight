using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomizeTree : MonoBehaviour
{
    private SpriteRenderer sr;
    public Sprite[] trees;
    
	void Start ()
    {
        trees = Resources.LoadAll<Sprite>("tree clumps");
        sr = GetComponent<SpriteRenderer>();
        int randomTree = Random.Range(0, trees.Length);
        sr.sprite = trees[randomTree];
        this.enabled = false;
	}
}
