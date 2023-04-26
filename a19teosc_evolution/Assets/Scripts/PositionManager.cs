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
        }
    }

    private void ACarPassedACheckpoint(CarLapCounter carLapCounter)
    {
        CLCounters = CLCounters.OrderByDescending(s => s.PassedCheckpointNumber).
            ThenBy(s => s.TimeAtLastCheckpoint).ToList();

        int positionOfCar = CLCounters.IndexOf(carLapCounter) + 1;

        carLapCounter.CarPosition = positionOfCar;
    }
}
