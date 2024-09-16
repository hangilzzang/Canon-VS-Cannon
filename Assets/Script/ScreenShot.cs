using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenShot : MonoBehaviour
{
    public void TakeScreenshot()
    {
        // "screenshot.png"로 저장할 파일명과 화면 해상도를 설정
        ScreenCapture.CaptureScreenshot("screenshot.png");
    }

    // Update 함수 내에서 특정 키를 눌렀을 때 스크린샷을 찍도록 설정
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S)) // S 키를 누르면 스크린샷
        {
            TakeScreenshot();
        }
    }
}
