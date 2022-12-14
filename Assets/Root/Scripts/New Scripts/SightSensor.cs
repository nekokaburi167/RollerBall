using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SightSensor : Sensor
{
    public float fieldOfView = 45f;
    public float viewDistance = 100f;

    private AIController controller;
    private Blackboard bb;
    private SenseMemory senseMemory;
    
    private void Start()
    {
        controller = GetComponent<AIController>();
        sensorType = SensorType.Sight;
        manager.RegisterSensor(this);

        bb = FindObjectOfType<Blackboard>();
        
        senseMemory = GetComponent<SenseMemory>();
    }

    private void Update()
    {
        
    }

    public override void Notify(Trigger trigger)
    {
        Debug.DrawLine(transform.position, trigger.transform.position, Color.magenta);

        if (trigger.CompareTag("Player"))
        {
            //Debug.Log($"I see a {trigger.gameObject.tag}");
            //Debug.Log("Log player position onto Blackboard");
            
            //Log player position onto Blackboard
            bb.playerLastSeenPosition = trigger.transform.position;
            bb.lastSenseTime = Time.time;
        }

        if (senseMemory != null)
        {
            //Debug.Log("Adding sight to list");
            senseMemory.AddToList(trigger.gameObject, 0.6f);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Vector3 frontRayPoint = transform.position + (transform.forward * viewDistance);
        float fieldOfViewInRadius = fieldOfView * 3.14f / 180f;
        Vector3 leftRayPoint = transform.TransformPoint(new Vector3(viewDistance * Mathf.Sin(fieldOfViewInRadius), 0,
            viewDistance * Mathf.Cos(fieldOfViewInRadius)));
        Vector3 rightRayPoint = transform.TransformPoint(new Vector3(-viewDistance * Mathf.Sin(fieldOfViewInRadius), 0,
            viewDistance * Mathf.Cos(fieldOfViewInRadius)));
        
        Debug.DrawLine(transform.position, frontRayPoint + new Vector3(0, 1, 0), Color.green);
        Debug.DrawLine(transform.position, leftRayPoint + new Vector3(0, 1, 0), Color.green);
        Debug.DrawLine(transform.position, rightRayPoint + new Vector3(0, 1, 0) , Color.green);
    }
}
