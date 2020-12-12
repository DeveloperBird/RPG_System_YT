using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationHelper : MonoBehaviour {

    public Animator anim;

    public void SetAttackFalse()
    {
        anim.SetBool("isAttacking", false);
    }
}
