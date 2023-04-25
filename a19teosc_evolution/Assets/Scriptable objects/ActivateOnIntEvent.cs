using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateOnIntEvent : MonoBehaviour
{
    [SerializeField] private IntEventSO givenEvent;
    private BoxCollider2D col;

    private void Awake()
    {
        col = GetComponent<BoxCollider2D>();
        col.enabled = false;
        givenEvent.OnEventRaised += ActivateThis;
    }

    private void ActivateThis(int value)
    {
        if (value == 1)
        {
            col.enabled = true;
        }
    }
}
