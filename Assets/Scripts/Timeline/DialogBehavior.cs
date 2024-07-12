using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class DialogBehavior : PlayableBehaviour
{
    public string content;
    int index;
    float printSpeed;
    public override void OnBehaviourPlay(Playable playable, FrameData info)
    {
        printSpeed = 0.4f;
        UIManager.Instance.SetDialogActive(true);
    }

    public override void PrepareFrame(Playable playable, FrameData info)
    {
        if (index >= content.Length) return;

        printSpeed -= Time.deltaTime;
        if (printSpeed <= 0)
        {
            index++;
            UIManager.Instance.PrintDialog(content.Substring(0, index));
            printSpeed = 0.2f;
        }
    }

    public override void OnBehaviourPause(Playable playable, FrameData info)
    {
        UIManager.Instance.SetDialogActive(false);
    }

}
