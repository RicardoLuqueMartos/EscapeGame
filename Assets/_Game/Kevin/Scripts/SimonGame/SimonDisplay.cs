using TMPro;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SimonDisplay : MonoBehaviour
{
    public TextMeshPro displayText; // ⚠️ C’est du TextMeshPro (pas UGUI)

    public int numberOfWords = 4;
    public float delay = 1.5f;

    private string[] colorNames = { "Red", "Yellow", "Green", "Blue" };
    private Dictionary<string, Color> colorMap;

    void Start()
    {
        //InitColorMap();
        //StartCoroutine(DisplayRandomWords());
    }

    void InitColorMap()
    {
        colorMap = new Dictionary<string, Color>
        {
            { "Red", Color.red },
            { "Yellow", Color.yellow },
            { "Green", Color.green },
            { "Blue", Color.blue }
        };
    }

    IEnumerator DisplayRandomWords()
    {
        for (int i = 0; i < numberOfWords; i++)
        {
            string word = colorNames[Random.Range(0, colorNames.Length)];
            string colorName = colorNames[Random.Range(0, colorNames.Length)];
            Color color = colorMap[colorName];

            displayText.text = word;
            displayText.color = color;

            yield return new WaitForSeconds(delay);

            displayText.text = "";
            yield return new WaitForSeconds(0.5f);
        }

        displayText.text = "À toi de jouer !";
        displayText.color = Color.white;
    }
}
