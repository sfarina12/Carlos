using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TMPro;
using UnityEngine;

public class randomQuote : MonoBehaviour
{
    TextMeshProUGUI text;
    List<string> quotes;

    void Start()
    {
        text = transform.GetComponent<TextMeshProUGUI>();

        quotes = File.ReadAllLines(Application.streamingAssetsPath + "\\quotes.txt").ToList();

        text.text = quotes[Random.RandomRange(0,quotes.Count-1)];
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.T)) text.text = quotes[Random.RandomRange(0, quotes.Count - 1)];
    }
}
