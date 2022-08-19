using UnityEngine;

public class BridgetEmotion2Color : MonoBehaviour
{
    [SerializeField] EmotionData emotionData;
    [SerializeField] BridgetManager manager;

    float lastTime;

    void Start()
    {
        lastTime = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        float valence = (emotionData.valence + 1)/2;
        float arousal = (emotionData.arousal + 1)/2;

        float hueHalfRange = arousal;

        float deltaTime = Time.time - lastTime;
        if( lastTime == 0 || deltaTime > Mathf.Lerp(0.5f, 0f, arousal) )
        {
            for(int i = 0; i<manager.GridSize.x; i++)
            {
                for(int j = 0; j<manager.GridSize.y; j++)
                {
                    Color previousColor = manager.GetColor(i, j);

                    float baseHue;
                    float baseS;
                    float baseV;  
                    Color.RGBToHSV(previousColor, out baseHue, out baseS, out baseV);

                    float saturation = valence + Random.Range(-0.01f, 0.01f); 
                    float value = valence + Random.Range(-0.01f, 0.01f); 
                    float hue = baseHue + Random.Range(-hueHalfRange, +hueHalfRange);

                    while(hue > 1)
                    {
                        hue -= 1;
                    }
                    while(hue < 0)
                    {
                        hue += 1;
                    }

                    saturation = Mathf.Clamp(saturation, 0f, 1f);
                    value = Mathf.Clamp(value, 0f, 1f);

                    Color color = Color.HSVToRGB(hue, saturation, value);

                    manager.SetColor(i, j, color);
                }
            }

            lastTime = Time.time;
        }
            
    }
}
