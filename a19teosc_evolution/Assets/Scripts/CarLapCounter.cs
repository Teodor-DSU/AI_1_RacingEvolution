using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CarLapCounter : MonoBehaviour
{
    private int _passedCheckpointNumber = 0;
    private float _timeAtLastCheckpoint = 0f;
    //private int _totalCheckpointsPassed = 0;
    private int _lapsCompleted = 0;
    private bool _finished = false;
    private int _carPosition = 0;
    private int totalLaps = 2;

    public int PassedCheckpointNumber
    {
        get { return _passedCheckpointNumber; }
        set { _passedCheckpointNumber = value; }
    }
    
    public float TimeAtLastCheckpoint
    {
        get { return _timeAtLastCheckpoint; }
        set { _timeAtLastCheckpoint = value; }
    }
    
    public int CarPosition
    {
        get { return _carPosition; }
        set { _carPosition = value; }
    }

    [SerializeField] private TMP_Text positionText;
    [SerializeField] private TMP_Text victoryText;
    [SerializeField] private string victoryMessage = "Victory!";
    [SerializeField] private Color messageColor = Color.yellow;
    [SerializeField] private BoolSO isRaceOver;
    [SerializeField] private IntSO lapAmount;

    public event Action<CarLapCounter> PassedACheckpoint;

    private void Start()
    {
        totalLaps = lapAmount.Int;
        victoryText.gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Checkpoint"))
        {
            if (isRaceOver.Bool)
                return;
            
            Checkpoint cp = col.GetComponent<Checkpoint>();
            if (_passedCheckpointNumber + 1 == cp.CheckpointNumber)
            {
                _passedCheckpointNumber = cp.CheckpointNumber;
                //_totalCheckpointsPassed++;

                _timeAtLastCheckpoint = Time.time;

                if (cp.IsFinishLine)
                {
                    _passedCheckpointNumber = 0;
                    _lapsCompleted++;

                    if (_lapsCompleted >= totalLaps)
                    {
                        _finished = true;
                        isRaceOver.Bool = true;
                        victoryText.gameObject.SetActive(true);
                        victoryText.color = messageColor;
                        victoryText.text = victoryMessage;
                    }
                }
                
                PassedACheckpoint?.Invoke(this);

                if(positionText)
                    positionText.text = _carPosition.ToString();
            }
        }
    }
}
