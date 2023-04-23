using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarLapCounter : MonoBehaviour
{
    private int _passedCheckpointNumber = 0;
    private float _timeAtLastCheckpoint = 0f;
    private int _totalCheckpointsPassed = 0;

    public event Action<CarLapCounter> PassedACheckpoint; 

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Checkpoint"))
        {
            Checkpoint cp = col.GetComponent<Checkpoint>();
            if (_passedCheckpointNumber + 1 == cp.CheckpointNumber)
            {
                _passedCheckpointNumber = cp.CheckpointNumber;
                _totalCheckpointsPassed++;

                _timeAtLastCheckpoint = Time.time;
                
                PassedACheckpoint?.Invoke(this);
            }
        }
    }
}
