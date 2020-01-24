using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextBox : MonoBehaviour
{
    RectTransform _textBoxTf;
    Text _text;

    [HideInInspector]
    public float boxPosY;

    // Start is called before the first frame update
    void Start()
    {
        _textBoxTf = GetComponent<RectTransform>();
        _text = GetComponent<Text>();
        _textBoxTf.sizeDelta = new Vector2(_text.preferredWidth, _text.preferredHeight);

        boxPosY = _textBoxTf.anchoredPosition.y;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
