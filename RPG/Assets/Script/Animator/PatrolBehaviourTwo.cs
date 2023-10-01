using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PatrolBehaviourTwo : StateMachineBehaviour
{
    float timer;
    List<Transform> Points = new List<Transform>();
    NavMeshAgent agent;

    Transform player;
    float cheseRange = 10;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timer = 0;
        Transform pointsObject = GameObject.FindGameObjectWithTag("PointsTwo").transform;
        foreach (Transform t in pointsObject)
        {
            Points.Add(t);
        }

        agent = animator.GetComponent<NavMeshAgent>();
        agent.SetDestination(Points[0].position);

        player = GameObject.FindGameObjectWithTag("Player").transform;
    }


    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (agent.remainingDistance <= agent.stoppingDistance)
        {
            agent.SetDestination(Points[Random.Range(0, Points.Count)].position);
        }

        timer += Time.deltaTime;
        if (timer > 10)
        {
            animator.SetBool("IsPotroller", false);
        }

        float distanse = Vector3.Distance(animator.transform.position, player.position);
        if (distanse < cheseRange)
        {
            animator.SetBool("IsChese", true);
        }

    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        agent.SetDestination(agent.transform.position);
    }
}
