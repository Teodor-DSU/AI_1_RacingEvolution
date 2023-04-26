using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;
using Random = UnityEngine.Random;

public class DriverAgent : Agent
{
    private PlayerCarController carController;
    private CarLapCounter carLaps;

    [SerializeField] private IntEventSO computerCheckpointEvent;
    [SerializeField] private Transform startingPoint;
    [SerializeField] private CheckpointTracker CPTracker;

    private void Awake()
    {
        carController = GetComponent<PlayerCarController>();
        carLaps = GetComponent<CarLapCounter>();
        computerCheckpointEvent.OnEventRaised += GiveTakeReward;
    }

    public override void OnEpisodeBegin()
    {
        transform.position = startingPoint.position + new Vector3(Random.Range(-0.5f, 0.5f), Random.Range(-0.3f, 0.3f), 0);
        transform.forward = startingPoint.forward;
        carController.Halt();
        carLaps.ResetTracking();
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        Vector3 nextCP = CPTracker.GetNextCheckpointPosition(carLaps.PassedCheckpointNumber).forward;
        float nextDirection = Vector3.Dot(transform.forward, nextCP);
        sensor.AddObservation(nextDirection);
    }

    public override void OnActionReceived(ActionBuffers actions)
    {
        float turnAmount = 0f;
        float forwardAmount = 0f;

        switch (actions.DiscreteActions[0])
        {
            case 0: turnAmount = 0f;
                break;
            case 1: turnAmount = +1f;
                break;
            case 2: turnAmount = -1f;
                break;
        }
        switch (actions.DiscreteActions[1])
        {
            case 0: forwardAmount = 0f;
                break;
            case 1: forwardAmount = +1f;
                break;
            case 2: forwardAmount = -1f;
                break;
        }

        Vector2 input = new Vector2(turnAmount, forwardAmount);
        
        carController.SetInputVector(input);
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        int turnAction = 0;
        if (Input.GetKey(KeyCode.RightArrow)) turnAction = 1;
        if (Input.GetKey(KeyCode.LeftArrow)) turnAction = 2;
        
        int forwardAction = 0;
        if (Input.GetKey(KeyCode.UpArrow)) forwardAction = 1;
        if (Input.GetKey(KeyCode.DownArrow)) forwardAction = 2;

        ActionSegment<int> discreteActions = actionsOut.DiscreteActions;
        discreteActions[0] = turnAction;
        discreteActions[1] = forwardAction;
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Track Edge"))
        {
            Debug.Log("DAMN!");
            AddReward(-1f);
        }
    }

    private void OnCollisionStay2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Track Edge"))
        {
            AddReward(-0.1f);
        }
    }

    private void GiveTakeReward(int value)
    {
        AddReward(value);
        Debug.Log("Reward: " + value);
    }
}
