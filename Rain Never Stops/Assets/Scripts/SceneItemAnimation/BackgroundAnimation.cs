using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundAnimation : MonoBehaviour
{
    [Header("背景动画")]
    [SerializeField] private Animator bgAnimator;
    [SerializeField] private string[] animationTriggers = { "Crash1", "Crash2", "Crash3", "Crash4" };

    private void OnEnable()
    {
        ItemInteractionManagement.OnProgressChanged += PlayAnimation;
    }

    private void OnDisable()
    {
        ItemInteractionManagement.OnProgressChanged -= PlayAnimation;
    }

    private void PlayAnimation(int count)
    {
        if (count <= animationTriggers.Length)
        {
            string triggerName = animationTriggers[count - 1];
            bgAnimator.SetTrigger(triggerName);
            //Debug.Log("播放第 " + count + " 段动画: " + triggerName);
        }
    }

}
