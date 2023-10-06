using UnityEngine;
using UnityEngine.Playables;

[System.Serializable]
public class TextPlayableAsset : PlayableAsset
{
    public ExposedReference<GameObject> charaObj;

    public override Playable CreatePlayable(PlayableGraph graph, GameObject go)
    {
        var behaviour = new TextPlayableBehaviour();
        behaviour.charaObject = charaObj.Resolve(graph.GetResolver());
        return ScriptPlayable<TextPlayableBehaviour>.Create(graph, behaviour);
    }
}
