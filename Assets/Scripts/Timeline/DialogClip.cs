using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class DialogClip : PlayableAsset
{
    public string content;

    public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
    {
        DialogBehavior dialogBehavior = new DialogBehavior();
        dialogBehavior.content = content;
        return ScriptPlayable<DialogBehavior>.Create(graph, dialogBehavior);
    }
}
