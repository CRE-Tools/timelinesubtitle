using UnityEngine;
using UnityEngine.Playables;

namespace hrs.timelinesubtitle.runtime
{
    public class SubtitleClip : PlayableAsset
    {
        public string subtitleText;
        public TextMood textMood;

        public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
        {
            var playable = ScriptPlayable<SubtitleBehaviour>.Create(graph);

            SubtitleBehaviour subtitleBehaviour = playable.GetBehaviour();
            subtitleBehaviour.subtitleText = subtitleText;
            subtitleBehaviour.textMood = textMood;

            return playable;
        }
    }
}
