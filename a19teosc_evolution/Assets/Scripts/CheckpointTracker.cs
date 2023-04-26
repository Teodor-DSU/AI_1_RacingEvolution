using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointTracker : MonoBehaviour
{
    [SerializeField] private List<Checkpoint> checkpoints;

    public Transform GetNextCheckpointPosition(int index)
    {
        int next = index % checkpoints.Count == 0 ? 0: index;

        return checkpoints[next].transform;
    }

    public int GetCheckpointAmount()
    {
        return checkpoints.Count;
    }
}