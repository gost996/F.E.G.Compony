using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LineManager : MonoBehaviour
{
    RectTransform pannelRect;
    Text lineNum;
    public float pannelHeight;

    public GameObject textBoxes;
    List<TextBox> textBoxList;

    TextBox[][] lineList;

    float firstLineHeight = 457.5f;
    float lineHeightGap = 32.5f;

    private static LineManager _instance = null;

    public static LineManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = (LineManager)FindObjectOfType(typeof(LineManager));
                if (_instance == null)
                {
                    Debug.Log("There's no active ManagerClass object");
                }
            }
            return _instance;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        pannelRect = GetComponent<RectTransform>();
        pannelHeight = pannelRect.rect.height;

        lineNum = transform.GetChild(0).GetComponent<Text>();
        lineNum.GetComponent<RectTransform>().sizeDelta = new Vector2(lineNum.GetComponent<RectTransform>().rect.width, pannelHeight);


        textBoxList = new List<TextBox>();
        for(int i = 0; i < textBoxes.transform.childCount; i++)
        {
            textBoxList.Add(textBoxes.transform.GetChild(i).transform.GetComponent<TextBox>());
        }
        Quicksort(textBoxList, 0, textBoxList.Count - 1);
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
        lineList = new TextBox[lineCount][];
        
        for (int i = 1; i < lineCount; i++)
        {
            lineNum.text += i + "\n";   
            
            for(int j = 0; j < textBoxList.Count; j++)
            {
                List<TextBox> temp = new List<TextBox>();

                if (textBoxList[j].boxPosY < firstLineHeight - lineHeightGap * i)
                {
                    lineList[i] = new TextBox[j];
                    lineList[i] = temp.ToArray();

                    temp.Clear();
                    textBoxList.RemoveRange(0, j);
                    break;
                }
                else if (textBoxList.Count == 1)
                {
                    Debug.Log(textBoxList[j] + " in lineList=> " + i);
                    temp.Add(textBoxList[0]);
                    lineList[i] = temp.ToArray();
                    textBoxList.RemoveAt(0);
                    break;
                }
                else
                {
                    Debug.Log(textBoxList[j] + " in lineList=> " + i);
                    temp.Add(textBoxList[j]);
                }
            }
        }
    }

    void SetInitTextLine()
    {

    }    
 
    public static int Partition(List<TextBox> array, int left, int right)
    {
        int mid = (left + right) / 2;
        Swap(array, left, mid);

        TextBox pivot = array[left];
        int i = left, j = right;

        while (i < j)
        {
            while (pivot.boxPosY > array[j].boxPosY)
            {
                j--;
            }

            while (i < j && pivot.boxPosY <= array[i].boxPosY)
            {
                i++;
            }
            Swap(array, i, j);
        }
        array[left] = array[i];
        array[i] = pivot;
        return i;
    }

    public static void Swap(List<TextBox> array, int a, int b)
    {
        TextBox temp = array[b];
        array[b] = array[a];
        array[a] = temp;
    }

    public static void Quicksort(List<TextBox> array, int left, int right)
    {
        if (left >= right)
        {
            return;
        }

        int pi = Partition(array, left, right);

        Quicksort(array, left, pi - 1);
        Quicksort(array, pi + 1, right);
    }
}
