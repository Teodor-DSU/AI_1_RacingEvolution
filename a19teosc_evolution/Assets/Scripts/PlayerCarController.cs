using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCarController : MonoBehaviour
{
    [SerializeField] private float drift = 0.8f;
    [SerializeField] private float acceleration = .5f;
    //[SerializeField] private float maxSpeed = 5f;
    [SerializeField] private float turnSpeed = 3.5f;

    private float _accelerationInput = 0f;
    private float _steeringInput = 0f;
    
    private float _rotationAngle = 0f;

    private Rigidbody2D _rb;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        ApplyEngineForce();
        LimitOrthogonalSpeed();
        Steering();
    }

    private void ApplyEngineForce()
    {
        Vector2 engineForceVector = transform.up * _accelerationInput * acceleration;
        
        _rb.AddForce(engineForceVector, ForceMode2D.Force);
    }

    private void Steering()
    {
        _rotationAngle -= _steeringInput * turnSpeed;
        
        _rb.MoveRotation(_rotationAngle);
    }

    private void LimitOrthogonalSpeed()
    {
        Vector2 forwardVelocity = transform.up * Vector2.Dot(_rb.velocity, transform.up);
        Vector2 rightVelocity = transform.right * Vector2.Dot(_rb.velocity, transform.right);

        _rb.velocity = forwardVelocity + rightVelocity * drift;
    }

    public void SetInputVector(Vector2 input)
    {
        _steeringInput = input.x;
        _accelerationInput = input.y;
    }
}
