using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Footstep : MonoBehaviour
{
    private void Start()
    {
        Enemy[] enemies = FindObjectsOfType<Enemy>();
        foreach (Enemy e in enemies)
        {
            if (!e.flying)
                e.footsteps.Add(this);
        }

        StartCoroutine(despawn());
    }

    private IEnumerator despawn()
    {
        yield return new WaitForSeconds(5);
        Destroy(this.gameObject);
    }
}
