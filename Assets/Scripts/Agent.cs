using UnityEngine;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;

public class Agent : Unity.MLAgents.Agent
{
    Rigidbody rBody;
    private bool isGrounded = true;
    public GameObject Obstacle;
    private GameObject activeObstacle;
    public GameObject Bonus;
    private GameObject activeBonus;
    public override void OnEpisodeBegin()
    {
        activeObstacle = Instantiate(Obstacle);
        activeObstacle.transform.SetParent(transform.parent);
        activeObstacle.transform.localPosition = new Vector3(-20, 0.5f, 0);
        activeObstacle.GetComponent<Obstacle>().speed = Random.Range(5f, 10f);

        activeBonus = Instantiate(Bonus);
        activeBonus.transform.SetParent(transform.parent);
        activeBonus.transform.localPosition = new Vector3(-10, 4.3f, 0);
        activeBonus.GetComponent<Bonus>().speed = Random.Range(5f, 10f);
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(transform.localPosition);
    }

    public override void OnActionReceived(ActionBuffers actionBuffers)
    {
        if (activeBonus == null & GetCumulativeReward()==0f)
        {
            SetReward(0.2f);

        }
        if (activeObstacle == null)
        {
            AddReward(0.8f);
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
        if (collision.gameObject.CompareTag("Bonus"))
        {
            Destroy(activeBonus);
        }
        
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            Destroy(activeObstacle);
            Destroy(activeBonus);
            EndEpisode();
        }
    }
}
