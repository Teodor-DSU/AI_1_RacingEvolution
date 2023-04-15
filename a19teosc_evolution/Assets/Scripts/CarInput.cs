using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarInput : MonoBehaviour
{
    private PlayerCarController _carController;

    private void Awake()
    {
        _carController = GetComponent<PlayerCarController>();
    }

    void Update()
    {
        Vector2 inputVector = Vector2.zero;

        inputVector.x = Input.GetAxis("Horizontal");
        inputVector.y = Input.GetAxis("Vertical");
        
        _carController.SetInputVector(inputVector);
    }
}
