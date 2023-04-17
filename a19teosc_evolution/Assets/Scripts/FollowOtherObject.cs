using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowOtherObject : MonoBehaviour
{
    [SerializeField] private GameObject objectToFollow;
    [SerializeField] private float stayAhead = 3f;
    private Transform _objectTransform;
    
    void Start()
    {
        _objectTransform = objectToFollow.transform;
    }

    void Update()
    {
        transform.position = _objectTransform.position + _objectTransform.up * stayAhead;
    }
}
