using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LapText : MonoBehaviour
{
    [SerializeField] private IntSO totalLapAmount;
    [SerializeField] private IntSO playerLapsDone;

    private int _writtenPlayerLaps = 0;
    private TMP_Text _text;
    
    void Start()
    {
        _text = GetComponent<TMP_Text>();
    }

    void Update()
    {
        _writtenPlayerLaps = playerLapsDone.Int + 1;
        _text.text = $"Laps: {_writtenPlayerLaps.ToString()} / {totalLapAmount.Int.ToString()}";
    }
}
