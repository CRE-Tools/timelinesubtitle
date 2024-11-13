using TMPro;
using UnityEngine;
using UnityEngine.Playables;

namespace hrspecian.timelinesubtitle.runtime
{
    public class SubtitleTrackMixer : PlayableBehaviour
    {
        public override void ProcessFrame(Playable playable, FrameData info, object playerData)
        {
            TextMeshProUGUI text = playerData as TextMeshProUGUI;
            string currentText = "";
            float currentAlpha = 0f;
            TextMood textMood = null;

            if (!text) { return; }

            int inputCount = playable.GetInputCount();
            for (int i = 0; i < inputCount; i++)
            {
                float inputWeight = playable.GetInputWeight(i);

                if (inputWeight > 0f)
                {
                    ScriptPlayable<SubtitleBehaviour> inputPlayable = (ScriptPlayable<SubtitleBehaviour>)playable.GetInput(i);

                    SubtitleBehaviour input = inputPlayable.GetBehaviour();
                    currentText = input.subtitleText;
                    currentAlpha = inputWeight;

                    textMood = input.textMood;
                }
            }

            ShowText(text, currentText, currentAlpha);

            if (textMood != null) 
                ShowTextMood(text, textMood);
        }

        private void ShowText(TextMeshProUGUI text, string currentText, float currentAlpha)
        {
            text.text = currentText;

            Color color = text.color;
            color.a = currentAlpha;

            text.color = color;
        }

        private void ShowTextMood(TextMeshProUGUI text, TextMood textMood)
        {
            text.ForceMeshUpdate();
            var textInfo = text.textInfo;

            for (int i = 0; i < textInfo.characterCount; ++i)
            {
                var charInfo = textInfo.characterInfo[i];

                if (!charInfo.isVisible) continue;

                var verts = textInfo.meshInfo[charInfo.materialReferenceIndex].vertices;

                for (int j = 0; j < 4; ++j)
                {
                    var orig = verts[charInfo.vertexIndex + j];
                    verts[charInfo.vertexIndex + j] =
                        orig + new Vector3(0, Mathf.Sin(
                            Time.time * 2f + orig.x * textMood.amplitude) * textMood.magnitude, 0);
                }
            }

            for (int i = 0; i < textInfo.meshInfo.Length; ++i)
            {
                var meshInfo = textInfo.meshInfo[i];
                meshInfo.mesh.vertices = meshInfo.vertices;
                text.UpdateGeometry(meshInfo.mesh, i);
            }
        }
    }
}