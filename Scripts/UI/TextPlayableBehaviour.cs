using TMPro;
using UnityEngine;
using UnityEngine.Playables;

// A behaviour that is attached to a playable
public class TextPlayableBehaviour : PlayableBehaviour
{
    public GameObject charaObject;
    private string text;

    // Called when the owning graph starts playing
    public override void OnGraphStart(Playable playable) {
        if (MainUI.GetInstance().DisplayChange())
        {
            charaObject = GameObject.Find("SectionText");
            this.text = this.charaObject.GetComponent<TextMeshProUGUI>().text;
            this.charaObject.GetComponent<TextMeshProUGUI>().text = "";
        }
    }

    // Called when the owning graph stops playing
    public override void OnGraphStop(Playable playable) {
        if (this.charaObject != null && !MainUI.GetInstance().DisplayChange())
        {
            this.charaObject.GetComponent<TextMeshProUGUI>().text = this.text;
        }
    }

    // Called when the state of the playable is set to Play
    public override void OnBehaviourPlay(Playable playable, FrameData info) {

    }

    // Called when the state of the playable is set to Paused
    public override void OnBehaviourPause(Playable playable, FrameData info) {

    }

    // Called each frame while the state is set to Play
    public override void ProcessFrame(Playable playable, FrameData info, object playerData)
    {
        // PlayableTrackのClip上でシークバーが移動するたびに呼ばれ続ける（PrepareFrameの後）
        if (charaObject == null) { return; }
        var percent = (float)playable.GetTime() / (float)playable.GetDuration();

        this.charaObject.GetComponent<TextMeshProUGUI>().text =
        this.text.Substring(0, (int)Mathf.Round(this.text.Length * percent));
    }
}
