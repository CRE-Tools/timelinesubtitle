using UnityEditor;
using UnityEngine;
using hrspecian.timelinesubtitle.runtime;

namespace hrspecian.timelinesubtitle.editor
{
    [CustomEditor(typeof(SubtitleClip))]
    public class Editor_SubtitleClip : Editor
    {
        public override void OnInspectorGUI ()
        {
            DrawDefaultInspector();

            SubtitleClip script = (SubtitleClip)target;

            GUILayout.Space(20f);

            if(GUILayout.Button("Text Mood Editor"))
            {
                Editor_TextMoodsTool tool = EditorWindow.GetWindow<Editor_TextMoodsTool>(
                    title: "Text Mood Editor",
                    focus: true
                    );

                tool.minSize = new Vector2(400f, 400f);

                tool.Show();
            }
        }
    }

}
