using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Events : MonoBehaviour
{
    private Button b1, b2, b3;
    private Text t;

    public static int friendship;
    public static int skill;
    public string[] buttonText;

    public Clearing clearing;

    private void OnEnable()
    {
        b1 = transform.GetChild(0).GetComponent<Button>();
        b2 = transform.GetChild(1).GetComponent<Button>();
        b3 = transform.GetChild(2).GetComponent<Button>();
        t = transform.GetChild(3).GetChild(0).GetComponent<Text>();

        b1.onClick.RemoveAllListeners();
        b2.onClick.RemoveAllListeners();
        b3.onClick.RemoveAllListeners();
        StartCoroutine(SetOptions());
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
        clearing.Continue();
    }

    public void Help()
    {
        clearing.Continue();
    }

    public void Hobby()
    {
        clearing.Continue();
    }

    public void Create()
    {
        clearing.Continue();
    }

    public void Therapy()
    {
        clearing.Continue();
    }

    public void Rest()
    {
        clearing.Continue();
    }
}
