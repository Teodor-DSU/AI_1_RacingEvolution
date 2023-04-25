using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Dependencies.Sqlite;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerCarController : MonoBehaviour
{
    [SerializeField] private float drift = 0.8f;
    [SerializeField] private float acceleration = .5f;
    [SerializeField] private float maxSpeed = 5f;
    [Range(0f, 1f)][SerializeField] private float reverseSpeedMultiplier = 0.5f;
    [SerializeField] private float turnSpeed = 3.5f;
    [SerializeField] private float turnLimiterForSpeed = 8f;
    [SerializeField] private float frictionWhenSlowingDown = 3f;

    private float _accelerationInput = 0f;
    private float _steeringInput = 0f;
    
    private float _rotationAngle = 0f;
    private float _velocityLimit = 0f;

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
        _velocityLimit = Vector2.Dot(transform.up, _rb.velocity);

        if (_velocityLimit > maxSpeed && _accelerationInput > 0)
            return;
        
        if (_velocityLimit < -maxSpeed * reverseSpeedMultiplier && _accelerationInput < 0)
            return;
        
        if (_rb.velocity.sqrMagnitude > maxSpeed * maxSpeed && _accelerationInput > 0)
            return;

        if (_accelerationInput == 0f)
        {
            _rb.drag = Mathf.Lerp(_rb.drag, 3.0f, Time.fixedDeltaTime * frictionWhenSlowingDown);
        }
        else
            _rb.drag = 0f;
        
        Vector2 engineForceVector = transform.up * _accelerationInput * acceleration;
        
        _rb.AddForce(engineForceVector, ForceMode2D.Force);
    }

    private void Steering()
    {
        float minSpeedToTurn = _rb.velocity.magnitude / turnLimiterForSpeed;
        minSpeedToTurn = Mathf.Clamp01(minSpeedToTurn);
        
        _rotationAngle -= _steeringInput * turnSpeed * minSpeedToTurn;
        
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

    public void Halt()
    {
        _steeringInput = 0f;
        _accelerationInput = 0f;
        _rotationAngle = 0f;
        
        _rb.velocity = Vector2.zero;
    }
}
