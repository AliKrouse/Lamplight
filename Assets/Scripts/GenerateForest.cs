using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateForest : MonoBehaviour
{
    public GameObject[] tiles;
    public GameObject[] forest;

    public List<GameObject> availableTiles;

    private GameObject player;

    private GameObject lastTile;

    private GameObject top, bottom, left, right;

    public float dangerLevel;
    public float dlSpeed;

	void Start ()
    {
        tiles = Resources.LoadAll<GameObject>("forest tiles");
        forest = GameObject.FindGameObjectsWithTag("Ground");
        player = GameObject.FindGameObjectWithTag("Player");
        dangerLevel = 1;

        lastTile = CenterTile();

        CreateForest();
	}

    private void Update()
    {
        forest = GameObject.FindGameObjectsWithTag("Ground");

        if (CenterTile() != lastTile)
        {
            CreateForest();
            lastTile = CenterTile();
        }

        if (dangerLevel < 6)
            dangerLevel += Time.deltaTime * dlSpeed;
    }

    void CreateForest()
    {
        foreach (GameObject f in forest)
        {
            if (f != null)
            {
                float d = Vector2.Distance(f.transform.position, player.transform.position);
                if (d >= 20)
                    Destroy(f.gameObject);
            }
        }

        int numberCreated = 0;
        
        SetAvailableTiles("top");
        if (Vector2.Distance(player.transform.position, t()) >= 20)
        {
            top = Instantiate(availableTiles[RandomTile()], t(), Quaternion.identity, transform);
            numberCreated++;
        }

        SetAvailableTiles("left");
        if (Vector2.Distance(player.transform.position, l()) >= 20)
        {
            left = Instantiate(availableTiles[RandomTile()], l(), Quaternion.identity, transform);
            numberCreated++;
        }

        SetAvailableTiles("right");
        if (Vector2.Distance(player.transform.position, r()) >= 20)
        {
            right = Instantiate(availableTiles[RandomTile()], r(), Quaternion.identity, transform);
            numberCreated++;
        }

        SetAvailableTiles("bottom");
        if (Vector2.Distance(player.transform.position, b()) >= 20)
        {
            bottom = Instantiate(availableTiles[RandomTile()], b(), Quaternion.identity, transform);
            numberCreated++;
        }

        SetAvailableCorner("tl");
        if (Vector2.Distance(player.transform.position, tl()) >= 20)
        {
            Instantiate(availableTiles[RandomTile()], tl(), Quaternion.identity, transform);
            numberCreated++;
        }

        SetAvailableCorner("tr");
        if (Vector2.Distance(player.transform.position, tr()) >= 20)
        {
            Instantiate(availableTiles[RandomTile()], tr(), Quaternion.identity, transform);
            numberCreated++;
        }

        SetAvailableCorner("bl");
        if (Vector2.Distance(player.transform.position, bl()) >= 20)
        {
            Instantiate(availableTiles[RandomTile()], bl(), Quaternion.identity, transform);
            numberCreated++;
        }

        SetAvailableCorner("br");
        if (Vector2.Distance(player.transform.position, br()) >= 20)
        {
            Instantiate(availableTiles[RandomTile()], br(), Quaternion.identity, transform);
            numberCreated++;
        }

        forest = new GameObject[0];
        forest = GameObject.FindGameObjectsWithTag("Ground");

        Debug.Log("created " + numberCreated + " tiles");
    }

    void SetAvailableTiles(string tile)
    {
        availableTiles.Clear();

        if (tile == "top")
        {
            if (CenterTile().GetComponent<TileTags>().exitT)
            {
                for (int i = 0; i < tiles.Length; i++)
                {
                    if (tiles[i].GetComponent<TileTags>().exitB && tiles[i].GetComponent<TileTags>().dangerLevel < dangerLevel)
                        availableTiles.Add(tiles[i]);
                }
            }
            else
                availableTiles.Add(tiles[tiles.Length - 1]);
        }
        if (tile == "left")
        {
            if (CenterTile().GetComponent<TileTags>().exitL)
            {
                for (int i = 0; i < tiles.Length; i++)
                {
                    if (tiles[i].GetComponent<TileTags>().exitR && tiles[i].GetComponent<TileTags>().dangerLevel < dangerLevel)
                        availableTiles.Add(tiles[i]);
                }
            }
            else
                availableTiles.Add(tiles[tiles.Length - 1]);
        }
        if (tile == "right")
        {
            if (CenterTile().GetComponent<TileTags>().exitR)
            {
                for (int i = 0; i < tiles.Length; i++)
                {
                    if (tiles[i].GetComponent<TileTags>().exitL && tiles[i].GetComponent<TileTags>().dangerLevel < dangerLevel)
                        availableTiles.Add(tiles[i]);
                }
            }
            else
                availableTiles.Add(tiles[tiles.Length - 1]);
        }
        if (tile == "bottom")
        {
            if (CenterTile().GetComponent<TileTags>().exitB)
            {
                for (int i = 0; i < tiles.Length; i++)
                {
                    if (tiles[i].GetComponent<TileTags>().exitT && tiles[i].GetComponent<TileTags>().dangerLevel < dangerLevel)
                        availableTiles.Add(tiles[i]);
                }
            }
            else
                availableTiles.Add(tiles[tiles.Length - 1]);
        }
    }

    void SetAvailableCorner(string tile)
    {
        availableTiles.Clear();

        if (tile == "tl")
        {
            if (top.GetComponent<TileTags>().exitL && left.GetComponent<TileTags>().exitT)
            {
                for (int i = 0; i < tiles.Length; i++)
                {
                    if (tiles[i].GetComponent<TileTags>().exitR && tiles[i].GetComponent<TileTags>().exitB && tiles[i].GetComponent<TileTags>().dangerLevel < dangerLevel)
                        availableTiles.Add(tiles[i]);
                }
            }
            else if (top.GetComponent<TileTags>().exitL && !left.GetComponent<TileTags>().exitT)
            {
                for (int i = 0; i < tiles.Length; i++)
                {
                    if (tiles[i].GetComponent<TileTags>().exitR && !tiles[i].GetComponent<TileTags>().exitB && tiles[i].GetComponent<TileTags>().dangerLevel < dangerLevel)
                        availableTiles.Add(tiles[i]);
                }
            }
            else if (!top.GetComponent<TileTags>().exitL && left.GetComponent<TileTags>().exitT)
            {
                for (int i = 0; i < tiles.Length; i++)
                {
                    if (!tiles[i].GetComponent<TileTags>().exitR && tiles[i].GetComponent<TileTags>().exitB && tiles[i].GetComponent<TileTags>().dangerLevel < dangerLevel)
                        availableTiles.Add(tiles[i]);
                }
            }
            else
                availableTiles.Add(tiles[tiles.Length - 1]);
        }
        if (tile == "tr")
        {
            if (top.GetComponent<TileTags>().exitR && right.GetComponent<TileTags>().exitT)
            {
                for (int i = 0; i < tiles.Length; i++)
                {
                    if (tiles[i].GetComponent<TileTags>().exitL && tiles[i].GetComponent<TileTags>().exitB && tiles[i].GetComponent<TileTags>().dangerLevel < dangerLevel)
                        availableTiles.Add(tiles[i]);
                }
            }
            else if (top.GetComponent<TileTags>().exitR && !right.GetComponent<TileTags>().exitT)
            {
                for (int i = 0; i < tiles.Length; i++)
                {
                    if (tiles[i].GetComponent<TileTags>().exitL && !tiles[i].GetComponent<TileTags>().exitB && tiles[i].GetComponent<TileTags>().dangerLevel < dangerLevel)
                        availableTiles.Add(tiles[i]);
                }
            }
            else if (!top.GetComponent<TileTags>().exitR && right.GetComponent<TileTags>().exitT)
            {
                for (int i = 0; i < tiles.Length; i++)
                {
                    if (!tiles[i].GetComponent<TileTags>().exitL && tiles[i].GetComponent<TileTags>().exitB && tiles[i].GetComponent<TileTags>().dangerLevel < dangerLevel)
                        availableTiles.Add(tiles[i]);
                }
            }
            else
                availableTiles.Add(tiles[tiles.Length - 1]);
        }
        if (tile == "bl")
        {
            if (bottom.GetComponent<TileTags>().exitL && left.GetComponent<TileTags>().exitB)
            {
                for (int i = 0; i < tiles.Length; i++)
                {
                    if (tiles[i].GetComponent<TileTags>().exitR && tiles[i].GetComponent<TileTags>().exitT && tiles[i].GetComponent<TileTags>().dangerLevel < dangerLevel)
                        availableTiles.Add(tiles[i]);
                }
            }
            else if (bottom.GetComponent<TileTags>().exitL && !left.GetComponent<TileTags>().exitB)
            {
                for (int i = 0; i < tiles.Length; i++)
                {
                    if (tiles[i].GetComponent<TileTags>().exitR && !tiles[i].GetComponent<TileTags>().exitT && tiles[i].GetComponent<TileTags>().dangerLevel < dangerLevel)
                        availableTiles.Add(tiles[i]);
                }
            }
            else if (!bottom.GetComponent<TileTags>().exitL && left.GetComponent<TileTags>().exitB)
            {
                for (int i = 0; i < tiles.Length; i++)
                {
                    if (!tiles[i].GetComponent<TileTags>().exitR && tiles[i].GetComponent<TileTags>().exitT && tiles[i].GetComponent<TileTags>().dangerLevel < dangerLevel)
                        availableTiles.Add(tiles[i]);
                }
            }
            else
                availableTiles.Add(tiles[tiles.Length - 1]);
        }
        if (tile == "br")
        {
            if (bottom.GetComponent<TileTags>().exitR && right.GetComponent<TileTags>().exitB)
            {
                for (int i = 0; i < tiles.Length; i++)
                {
                    if (tiles[i].GetComponent<TileTags>().exitL && tiles[i].GetComponent<TileTags>().exitT && tiles[i].GetComponent<TileTags>().dangerLevel < dangerLevel)
                        availableTiles.Add(tiles[i]);
                }
            }
            else if (bottom.GetComponent<TileTags>().exitR && !right.GetComponent<TileTags>().exitB)
            {
                for (int i = 0; i < tiles.Length; i++)
                {
                    if (tiles[i].GetComponent<TileTags>().exitL && !tiles[i].GetComponent<TileTags>().exitT && tiles[i].GetComponent<TileTags>().dangerLevel < dangerLevel)
                        availableTiles.Add(tiles[i]);
                }
            }
            else if (!bottom.GetComponent<TileTags>().exitR && right.GetComponent<TileTags>().exitB)
            {
                for (int i = 0; i < tiles.Length; i++)
                {
                    if (!tiles[i].GetComponent<TileTags>().exitL && tiles[i].GetComponent<TileTags>().exitT && tiles[i].GetComponent<TileTags>().dangerLevel < dangerLevel)
                        availableTiles.Add(tiles[i]);
                }
            }
            else
                availableTiles.Add(tiles[tiles.Length - 1]);
        }
    }

    int RandomTile()
    {
        return Random.Range(0, availableTiles.Count);
    }

    GameObject CenterTile()
    {
        float lowestD = 100;
        int closestTile = 0;
        for (int i = 0; i < forest.Length; i++)
        {
            float d = Vector2.Distance(player.transform.position, forest[i].transform.position);
            if (d < lowestD)
            {
                lowestD = d;
                closestTile = i;
            }
        }
        return forest[closestTile];
    }

    Vector2 tl()
    {
        return new Vector2(CenterTile().transform.position.x - 20, CenterTile().transform.position.y + 20);
    }

    Vector2 t()
    {
        return new Vector2(CenterTile().transform.position.x, CenterTile().transform.position.y + 20);
    }

    Vector2 tr()
    {
        return new Vector2(CenterTile().transform.position.x + 20, CenterTile().transform.position.y + 20);
    }

    Vector2 l()
    {
        return new Vector2(CenterTile().transform.position.x - 20, CenterTile().transform.position.y);
    }

    Vector2 r()
    {
        return new Vector2(CenterTile().transform.position.x + 20, CenterTile().transform.position.y);
    }

    Vector2 bl()
    {
        return new Vector2(CenterTile().transform.position.x - 20, CenterTile().transform.position.y - 20);
    }

    Vector2 b()
    {
        return new Vector2(CenterTile().transform.position.x, CenterTile().transform.position.y - 20);
    }

    Vector2 br()
    {
        return new Vector2(CenterTile().transform.position.x + 20, CenterTile().transform.position.y - 20);
    }
}
