using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    [SerializeField] private bool isFinishLine = false;
    [SerializeField] private int checkpointNumber = 1;

    public int CheckpointNumber
    {
        get { return checkpointNumber;}
        set { checkpointNumber = value; }
    }
}
