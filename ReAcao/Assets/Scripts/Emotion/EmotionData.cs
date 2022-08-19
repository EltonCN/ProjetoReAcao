using UnityEngine;

[CreateAssetMenu(menuName="ReAção/EmotionData")]
public class EmotionData : ScriptableObject
{
    public float valence;
    public float arousal;

    public float meanValence;
    public float meanArousal;
}
