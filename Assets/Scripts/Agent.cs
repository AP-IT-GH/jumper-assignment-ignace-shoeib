using UnityEngine;
using Unity.MLAgents.Actuators;

public class Agent : Unity.MLAgents.Agent
{
    Rigidbody rBody;
    public bool isGrounded = true;
    public override void OnEpisodeBegin()
    {
        isGrounded = true;
        transform.localPosition = new Vector3(0, 0.5f, 0);
    }

    public override void OnActionReceived(ActionBuffers actionBuffers)
    {
        AddReward(0.001f);
        if (isGrounded && actionBuffers.DiscreteActions[0] == 1)
        {
            isGrounded = false;
            rBody.velocity = new Vector3(0, 10, 0);
            AddReward(-0.030f);
        }
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
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 3)
        {
            isGrounded = true;
        }
        if (other.gameObject.CompareTag("Bonus"))
        {
            AddReward(0.1f);
            Destroy(other.gameObject);
        }
    }
    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Obstacle"))
        {
            AddReward(-10.0f);
            Destroy(other.gameObject);
            EndEpisode();
        }
    }
}
