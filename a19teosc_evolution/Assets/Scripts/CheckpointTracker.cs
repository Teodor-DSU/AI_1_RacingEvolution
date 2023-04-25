using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointTracker : MonoBehaviour
{
    [SerializeField] private List<Checkpoint> checkpoints;

    public Transform GetNextCheckpointPosition(int index)
    {
        int next = index % checkpoints.Count == 0 ? 1: index + 1;

        return checkpoints[next].transform;
    }
}
