using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Text;

[RequireComponent(typeof(TMP_Text))]
public class EmotionDebugger : MonoBehaviour
{
    [SerializeField] EmotionData data;
    TMP_Text text;

    bool instantValues = true;
    bool meanValues = false;

    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        DrawEmotionData();
    }

    void OnValidate()
    {
        if(data == null)
        {
            return;
        }
        if(text == null)
        {
            text = GetComponent<TMP_Text>();
        }

        DrawEmotionData();
    }

    void DrawEmotionData()
    {
        StringBuilder message = new StringBuilder();

        if(instantValues)
        {
            message.Append("Valence: "+data.valence.ToString()+"\n");
            message.Append("Arousal: "+data.arousal.ToString()+"\n");
        }
        if(meanValues)
        {
            message.Append("Mean Valence: "+data.meanValence.ToString()+"\n");
            message.Append("Mean Arousal: "+data.meanArousal.ToString()+"\n");
        }

        text.text = message.ToString();
    }
}
