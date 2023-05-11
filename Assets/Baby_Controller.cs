using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;

public class Baby_Controller : Agent
{

    [SerializeField] private Transform targetTransform;

    public float episodeTimer = 10f;

    void Update()
    {
        episodeTimer -= Time.deltaTime;

        if (episodeTimer <= 0f)
        {
            EndEpisode();

        }
    }

    public override void OnEpisodeBegin()
    {
        transform.position = Vector3.zero;
        SetReward(1f);
        episodeTimer = 10f;
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(transform.position);
        //sensor.AddObservation(targetTransform.position);
    }


    //public override void OnActionReceived(float[] vectorAction)
    //{
    //    float movex = vectorAction[0];
    //    transform.localPosition += new Vector3(movex,0,0);
    //}

    //public override void Heuristic(float[] actionsOut)
    //{

    //    actionsOut[0] = Input.GetAxis("Horizontal");

    //}

    public override void OnActionReceived(ActionBuffers actions)
    {
        float movex = actions.ContinuousActions[0];
        transform.localPosition += new Vector3(movex, 0, 0);
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        ActionSegment<float> continuousActions = actionsOut.ContinuousActions;
        continuousActions[0] = Input.GetAxisRaw("Horizontal");
    }


    private void OnTriggerEnter(Collider other)
    {
        SetReward(-1f);
        EndEpisode();
    }

}
