using UnityEngine;
using UnityEngine.UI;

public class FPS : MonoBehaviour
{
    public Text fpsText; // FPS를 표시할 텍스트 UI


    void Update()
    {

        
        // FPS 계산
        float fps = 1.0f / Time.unscaledDeltaTime;
        
        // 텍스트 UI에 FPS 표시
        fpsText.text = string.Format("FPS: {0:0.}", fps);
    }
}
