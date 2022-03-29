using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;

public class Agent : Unity.MLAgents.Agent
{
    Rigidbody rBody;
    public override void OnEpisodeBegin()
    {

    }

    public override void CollectObservations(VectorSensor sensor)
    {

    }

    public override void OnActionReceived(ActionBuffers actionBuffers)
    {
        rBody.AddForce(actionBuffers.ContinuousActions[0] * 10, 0, actionBuffers.ContinuousActions[1] * 10);
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        var discreteActionsout = actionsOut.DiscreteActions;
        if (Input.GetKey(KeyCode.Space) == true)
            discreteActionsout[0] = 1;
    }
    void Start()
    {
        rBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
