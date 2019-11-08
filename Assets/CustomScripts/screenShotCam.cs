using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class screenShotCam : MonoBehaviour {

    public string folder;
    public int frameRate;
    public int sizeMultiplier;
    private string realFolder;

	// Use this for initialization
	void Start () {
        Screen.fullScreen = !Screen.fullScreen;
        Time.captureFramerate = frameRate;
        // Find a folder that doesn't exist yet by appending numbers!
        realFolder = folder;
        int count = 1;
        while (System.IO.Directory.Exists(realFolder))
        {
            realFolder = folder + count;
            count++;
        }
        // Create the folder
        System.IO.Directory.CreateDirectory(realFolder);
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown("space"))
        {
            string shot = System.DateTime.Now.ToString() + "-" + Time.frameCount;
            shot = shot.Replace("/", "-");
            shot = shot.Replace(" ", "_");
            shot = shot.Replace(":", "-");
            shot = string.Format("{0}/shot {1}.png", realFolder, shot);

            ScreenCapture.CaptureScreenshot(shot);
        }
	}
}
