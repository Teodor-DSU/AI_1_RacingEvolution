using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PositionTextColor : MonoBehaviour
{
    [SerializeField] private Color firstPlace = Color.yellow;
    [SerializeField] private Color secondPlace = Color.blue;
    [SerializeField] private Color otherwise = Color.red;

    private TMP_Text _text;
    
    void Start()
    {
        _text = GetComponent<TMP_Text>();
    }

    void Update()
    {
        if (_text.text == "1")
        {
            _text.color = firstPlace;
        }
        else if(_text.text == "2")
        {
            _text.color = secondPlace;
        }
        else
        {
            _text.color = otherwise;
        }
    }
}
