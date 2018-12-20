using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Events : MonoBehaviour
{
    private Button b1, b2, b3;

    public static int friendship;
    public static int skill;
    public string[] buttonText;
    public string[] positiveResults;
    public string[] negativeResults;
    public Text resultsText;

    public Clearing clearing;

    private PlayerController player;

    private void OnEnable()
    {
        b1 = transform.GetChild(0).GetComponent<Button>();
        b2 = transform.GetChild(1).GetComponent<Button>();
        b3 = transform.GetChild(2).GetComponent<Button>();

        b1.onClick.RemoveAllListeners();
        b2.onClick.RemoveAllListeners();
        b3.onClick.RemoveAllListeners();
        StartCoroutine(SetOptions());

        friendship = 60;
        skill = 60;

        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    private IEnumerator SetOptions()
    {
        int c1 = Random.Range(0, 5);
        int c2 = Random.Range(0, 5);
        while (c2 == c1)
            c2 = Random.Range(0, 5);

        b1.transform.GetChild(0).GetComponent<Text>().text = buttonText[c1];
        b2.transform.GetChild(0).GetComponent<Text>().text = buttonText[c2];
        b3.transform.GetChild(0).GetComponent<Text>().text = buttonText[5];

        SetButtons(c1, b1);
        SetButtons(c2, b2);

        Debug.Log("set");
        yield return null;
    }

    void SetButtons(int choice, Button b)
    {
        if (choice == 0)
            b.onClick.AddListener(Friends);
        if (choice == 1)
            b.onClick.AddListener(Help);
        if (choice == 2)
            b.onClick.AddListener(Hobby);
        if (choice == 3)
            b.onClick.AddListener(Create);
        if (choice == 4)
            b.onClick.AddListener(Therapy);
    }

    //roll percent, static stats are DC. higher number = more difficult

    public void Friends()
    {
        int roll = Random.Range(1, 101);
        if (roll <= friendship)
        {
            player.maxHp += 1;
            friendship += 5;
            StartCoroutine(ShowResults(positiveResults[0]));
        }
        else
        {
            player.maxHp -= 1;
            friendship -= 5;
            StartCoroutine(ShowResults(negativeResults[0]));
        }

        clearing.Stand();
    }

    public void Help()
    {
        int roll = Random.Range(1, 101);
        roll -= (roll / 2);
        if (roll <= friendship)
        {
            friendship += 10;
            StartCoroutine(ShowResults(positiveResults[1]));
        }
        else
        {
            player.maxHp -= 2;
            friendship -= 10;
            StartCoroutine(ShowResults(negativeResults[1]));
        }

        clearing.Stand();
    }

    public void Hobby()
    {
        int roll = Random.Range(1, 101);
        if (roll <= skill)
        {
            player.hp += 1;
            skill += 5;
            StartCoroutine(ShowResults(positiveResults[2]));
        }
        else
        {
            player.hp -= 1;
            skill -= 5;
            StartCoroutine(ShowResults(negativeResults[2]));
        }

        clearing.Stand();
    }

    public void Create()
    {
        int roll = Random.Range(1, 101);
        roll -= (roll / 2);
        if (roll <= skill)
        {
            player.hp += 1;
            player.maxHp += 1;
            skill += 10;
            StartCoroutine(ShowResults(positiveResults[3]));
        }
        else
        {
            player.maxHp -= 2;
            skill -= 10;
            StartCoroutine(ShowResults(negativeResults[3]));
        }

        clearing.Stand();
    }

    public void Therapy()
    {
        player.hp -= 1;
        player.maxHp += 1;
        StartCoroutine(ShowResults(positiveResults[4]));
        clearing.Stand();
    }

    public void Rest()
    {
        player.hp += 2;
        friendship -= 5;
        skill -= 5;
        StartCoroutine(ShowResults(positiveResults[5]));
        clearing.Stand();
    }

    private IEnumerator ShowResults(string result)
    {
        resultsText.text = result;
        yield return new WaitForSeconds(3);
        resultsText.text = "";
    }
}
