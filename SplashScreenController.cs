using System.Collections;
using UnityEngine;
using UnityEngine.UI;
public class SplashScreenController : MonoBehaviour
{
    public Image SplashScreenVideo;
    public int SplashScreenEndingInSeconds = 0;

    private void Start()
    {
        if(SplashScreenVideo == null)
            SplashScreenVideo = GetComponent<Image>();
        
        StartCoroutine(FadeIn());
        StartCoroutine(WaitForVideoToEnd());
    }

    private void Update()
    {
        if(Input.GetKey(KeyCode.Space))
            SkipScene();
    }

    private void SkipScene()
    {
        GlobalSceneManager.Instance.UpdateSceneState(GlobalSceneManager.SceneState.MAINMENU);
    }

    private IEnumerator FadeIn()
    {
        for (float i = 2; i >= 0; i -= Time.deltaTime) 
        {
            // set color with i as alpha
            var c = new Color(0, 0, 0, i);
            SplashScreenVideo.color = c;
            yield return null;
        }
    }

    private IEnumerator FadeOut()
    {
        for (float i = 0; i <= 1; i += Time.deltaTime) 
        {
            // set color with i as alpha
            var c = new Color(0, 0, 0, i);
            SplashScreenVideo.color = c;
            yield return null;
        }
        SkipScene();
    }

    private IEnumerator WaitForVideoToEnd()
    {
        yield return new WaitForSeconds(SplashScreenEndingInSeconds);
        StartCoroutine(FadeOut());
    }
}
