using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuasarPulse : Reaction
{
    public HitboxHandler hitboxHandler;
    public Animator animator;
    public override void Trigger(Transform position)
    {
        hitboxHandler.Rect(0, transform, 4, 100);
        hitboxHandler.Rect(180, transform, 4, 100);
        GeneralFunctions.instance.ShakeCamera(1f, 13.33f, 13.33f);
        animator.SetTrigger("play");
    }
}
