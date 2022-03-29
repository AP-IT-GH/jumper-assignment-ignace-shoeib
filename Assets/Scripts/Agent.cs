using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;

public class Agent : Unity.MLAgents.Agent
{
    Rigidbody rBody;
    private bool isGrounded = true;
    public override void OnEpisodeBegin()
    {

    }

    public override void CollectObservations(VectorSensor sensor)
    {

    }

    public override void OnActionReceived(ActionBuffers actionBuffers)
    {
        rBody.AddForce(0, actionBuffers.DiscreteActions[0] * 100, 0);
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        if (isGrounded)
        {
            var discreteActionsout = actionsOut.DiscreteActions;
            if (Input.GetKey(KeyCode.Space) == true)
                discreteActionsout[0] = 1;
        }
    }
    void Start()
    {
        rBody = GetComponent<Rigidbody>();
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 3)
        {
            isGrounded = true;
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == 3)
        {
            isGrounded = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
