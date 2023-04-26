using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LapText : MonoBehaviour
{
    [SerializeField] private IntSO totalLapAmount;
    [SerializeField] private IntSO playerLapsDone;

    private TMP_Text _text;
    
    void Start()
    {
        _text = GetComponent<TMP_Text>();
    }

    void Update()
    {
        _text.text = $"Laps: {playerLapsDone.Int + 1.ToString()} / {totalLapAmount.Int.ToString()}";
    }
}
