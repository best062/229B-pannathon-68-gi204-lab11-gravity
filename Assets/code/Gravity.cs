using System;
using System.Collections.Generic;
using UnityEngine;

public class Gravity : MonoBehaviour
{
    Rigidbody rb;
    const float G = (6.674f * (10 ^ -11));
    public static List<Gravity> otherObjectsList;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        if (otherObjectsList == null)
        {
            otherObjectsList = new List<Gravity>();
        }
        
        otherObjectsList.Add(this);
    }

    private void FixedUpdate()
    {
        foreach (Gravity obj in otherObjectsList)
        {
            //do not attract itself!
            if (obj != this)
            {
                Attract(obj);
            }
        }
    }

    void Attract(Gravity Other)
    {
        Rigidbody otherRb = Other.rb;
        Vector3 direction = rb.position - otherRb.position;

        //get distance between 2 obj = r
        float distance = direction.magnitude;

        //if 2 obj at same position = nothing
        if (distance == 0f) { return; }

        //F = G(M1 * M2) / R^2
        float forceMagnitude = ((G * (rb.mass * otherRb.mass)) / Mathf.Pow(distance, 2));

        Vector3 garvityForce = forceMagnitude * direction.normalized;
        otherRb.AddForce(garvityForce);
    }
}
