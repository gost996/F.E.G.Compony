using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LineManager : MonoBehaviour
{
    RectTransform pannelRect;
    Text lineNum;

    public float pannelHeight;


    // Start is called before the first frame update
    void Start()
    {
        pannelRect = GetComponent<RectTransform>();
        pannelHeight = pannelRect.rect.height;

        lineNum = transform.GetChild(0).GetComponent<Text>();
        lineNum.GetComponent<RectTransform>().sizeDelta = new Vector2(lineNum.GetComponent<RectTransform>().rect.width, pannelHeight);

        CreateLine();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void CreateLine()
    {
        lineNum.text = "\n";

        int lineCount = (int)(pannelHeight / 33.3);
        Debug.Log(lineCount);
        for (int i = 1; i <= lineCount; i++)
        {
            lineNum.text += i + "\n";
        }
        
    }
}
