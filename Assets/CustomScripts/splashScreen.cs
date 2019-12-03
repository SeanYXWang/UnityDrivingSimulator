using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/*
Using this as a reference https://www.instructables.com/id/2-Starting-to-Make-a-Game-Making-a-Splashscreen-in/
Initially copying but will change this as I need. 
*/


public class splashScreen : MonoBehaviour
{
    public Image [] SplashImage;
    public Text Speedo_text;
    public Image cross;
    public Image Speedo_img; 
    public string NextScene;
    public float TimeToFadeIn;
    public float TimeTillFadeOut;
    public float TimeToFadeOut;
    public float TimeTillNextScene;
    public bool splashScreenActive;

    void setAlphas()
    {
        Speedo_text.canvasRenderer.SetAlpha(0.0f);
        Speedo_img.canvasRenderer.SetAlpha(0.0f);
        foreach(Image simage in SplashImage)
        {
            simage.canvasRenderer.SetAlpha(0.0f);
        }
        cross.canvasRenderer.SetAlpha(1.0f);
    }

    void Start()
    {
        //StartCoroutine(init());
    }

    void Update()
    {
        if(Input.GetKeyDown("space"))
        {
            StartCoroutine(init());
        }
    }

    IEnumerator init()
    {
        
        //make all components on canvas vanish
        setAlphas();
        splashScreenActive = true;

        yield return new WaitForSeconds(TimeTillNextScene);
        //slowly fade each component in and out for the speed signs
        foreach(Image simage in SplashImage)
        {
            FadeOut(cross);
            FadeIn(simage);
            yield return new WaitForSeconds(TimeTillFadeOut);
            FadeOut(simage);
            FadeIn(cross);
            yield return new WaitForSeconds(TimeTillNextScene);
        }


        FadeOut(cross);
        splashScreenActive = false; 
        //just leave the speedo and text behdind
        Speedo_img.CrossFadeAlpha(1.0f, TimeToFadeIn, false);
        Speedo_text.CrossFadeAlpha(1.0f, TimeToFadeIn, false);
    }

    void FadeIn(Image simage)
    {
        simage.canvasRenderer.SetAlpha(1.0f);
    }

    void FadeOut(Image simage)
    {
        simage.canvasRenderer.SetAlpha(0.0f);
    }
}
