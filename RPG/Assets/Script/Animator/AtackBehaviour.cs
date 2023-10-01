using UnityEngine;

public class AtackBehaviour : StateMachineBehaviour
{
    Transform player;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.transform.LookAt(player);
        float destanse = Vector3.Distance(animator.transform.position, player.position);

        if (destanse > 3)
            animator.SetBool("IsAtack", false);

        if (destanse > 15)
            animator.SetBool("IsChese", false);
    }
   
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }
}