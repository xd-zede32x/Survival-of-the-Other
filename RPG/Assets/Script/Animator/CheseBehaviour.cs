using UnityEngine;
using UnityEngine.AI;

public class CheseBehaviour : StateMachineBehaviour
{
    NavMeshAgent agent;
    Transform player;
    float atackRange = 2;
    float chaseRange = 10;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        agent = animator.GetComponent<NavMeshAgent>();
        agent.speed = 4;

        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        agent.SetDestination(player.position);
        float destanse = Vector3.Distance(animator.transform.position, player.position);
        if (destanse < atackRange) 
            animator.SetBool("IsAtack", true);
        
        if (destanse > 10)
          animator.SetBool("IsChese", false);
        
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        agent.SetDestination(agent.transform.position);
        agent.speed = 2;
    }

}
