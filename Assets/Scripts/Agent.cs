using UnityEngine;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;

public class Agent : Unity.MLAgents.Agent
{
    Rigidbody rBody;
    private bool isGrounded = true;
    public GameObject Obstacle;
    private GameObject activeObstacle;
    public override void OnEpisodeBegin()
    {
        activeObstacle = Instantiate(Obstacle);
        activeObstacle.transform.SetParent(transform.parent);
        activeObstacle.transform.localPosition = new Vector3(-15, 0.5f, 0);
        activeObstacle.GetComponent<Obstacle>().speed = Random.Range(5f, 10f);
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(transform.localPosition);
    }

    public override void OnActionReceived(ActionBuffers actionBuffers)
    {

        if (activeObstacle == null)
        {
            SetReward(1f);
            EndEpisode();
        }
        if(isGrounded)
            rBody.AddForce(0, actionBuffers.DiscreteActions[0] * 150, 0);
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
    }
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == 3)
        {
            isGrounded = false;
        }
    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer != 3)
        {
            Destroy(activeObstacle);
            EndEpisode();
        }
    }
}
