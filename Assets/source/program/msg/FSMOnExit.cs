﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSMOnExit : StateMachineBehaviour
{

    public string[] onExitMessage;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        foreach (var msg in onExitMessage)
        {
            // animator.gameObject.SendMessage(msg);  //將訊息（msg）發送給兄弟層
            animator.gameObject.SendMessageUpwards(msg);//向上層發msg（訊息）
        }
    }
}