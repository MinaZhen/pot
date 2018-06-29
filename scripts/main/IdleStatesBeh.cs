using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleStatesBeh : StateMachineBehaviour {

    private int id = 0;
    
    override public void OnStateMachineEnter(Animator animator, int stateMachinePathHash)    {

        if ((animator.GetInteger("break") == 99) || (animator.GetLayerWeight(1) == 1f) || (animator.GetLayerWeight(2) == 1f)) id = 0;
        id++;
        if (id > 30) id = 0;
        
        animator.SetInteger("break", id);
    }
}
