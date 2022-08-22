using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderRedirector : MonoBehaviour
{
    public Action<Collision> Event_OnCollisionEnter;
    public Action<Collision> Event_OnCollisionExit;

    public Action<Collider> Event_OnTriggerEnter;
    public Action<Collider> Event_OnTriggerStay;
    public Action<Collider> Event_OnTriggerExit;

    public void OnTriggerEnter(Collider col)
    {
        Event_OnTriggerEnter.Invoke(col);
    }

    public void OnTriggerStay(Collider col)
    {
        Event_OnTriggerStay.Invoke(col);
    }

    public void OnTriggerExit(Collider col)
    {
        Event_OnTriggerExit.Invoke(col);
    }

    public void OnCollisionEnter(Collision col)
    {
        Event_OnCollisionEnter.Invoke(col);
    }

    public void OnCollisionExit(Collision col)
    {
        Event_OnCollisionExit.Invoke(col);
    }
}
