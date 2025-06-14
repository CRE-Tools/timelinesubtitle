using UnityEngine;

namespace hrs.timelinesubtitle.runtime
{
    [CreateAssetMenu(menuName = "ScriptableObjects/TextWobblyMoods", order = 1)]
    public class TextMood : ScriptableObject
    {
        public float amplitude;
        public float magnitude;
    }
}
