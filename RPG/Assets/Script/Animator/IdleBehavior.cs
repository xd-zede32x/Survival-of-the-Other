using UnityEngine;
using UnityEngine.AI;

public class IdleBehavior : StateMachineBehaviour
{
    float timer;
    Transform player;
    float cheseRange = 10;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timer = 0;
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timer += Time.deltaTime;
        if (timer > 6)
        {
            animator.SetBool("IsPotroller", true);
        }

        float distanse = Vector3.Distance(animator.transform.position, player.position);
        if (distanse < cheseRange)
        {
            animator.SetBool("IsChese", true);
        }
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
    }
}