using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleBehaviour : StateMachineBehaviour
{

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (PlayerCombat.Instance.inputReceived)
        {
            animator.SetTrigger("AttackOne");
            PlayerCombat.Instance.InputManager();
            PlayerCombat.Instance.inputReceived = false;
        }
    }


}
