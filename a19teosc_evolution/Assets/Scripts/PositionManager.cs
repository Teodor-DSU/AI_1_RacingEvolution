using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PositionManager : MonoBehaviour
{
    public List<CarLapCounter> CLCounters = new List<CarLapCounter>();
    
    void Start()
    {
        CarLapCounter[] CLCountersArray = FindObjectsOfType<CarLapCounter>();

        CLCounters = CLCountersArray.ToList<CarLapCounter>();

        foreach (CarLapCounter car in CLCounters)
        {
            car.PassedACheckpoint += ACarPassedACheckpoint;
            Debug.Log("Another one");
        }
    }

    private void ACarPassedACheckpoint(CarLapCounter carLapCounter)
    {
        Debug.Log("bites the dust");
            /* CLCounters = CLCounters.OrderByDescending(s => s.LapsCompleted).
                 ThenBy(s => s.PassedCheckpointNumber).
                 ThenBy(s => s.TimeAtLastCheckpoint).ToList();
            
            CLCounters.Sort((a, b) => (a.LapsCompleted + a.PassedCheckpointNumber * 0.001f).
                CompareTo(b.LapsCompleted + b.PassedCheckpointNumber * 0.001f));*/
        
        CLCounters = CLCounters.OrderByDescending(s => s.TotalCheckpointsPassed).
             ThenBy(s => s.TimeAtLastCheckpoint).ToList();

        foreach (var car in CLCounters)
        {
            int positionOfCar = CLCounters.IndexOf(car) + 1;
             
            car.CarPosition = positionOfCar;
            car.PositionContainer = positionOfCar;
        }
    }
}
