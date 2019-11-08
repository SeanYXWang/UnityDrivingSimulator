using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using System.IO;
using System;
using LSL;

public class cam_report : MonoBehaviour
{
    // create stream info and outlet
    private liblsl.StreamInfo info;
    // liblsl.StreamOutlet outlet;
    private liblsl.XMLElement chns;
    // create outlet for the stream
    private liblsl.StreamOutlet outlet;
    // save the car pose data in float
    //private float[] rowDataTempFloat;

    private RCC_Settings RCCSettings { get { return RCC_Settings.Instance; } }
    double gasInput;
    double breakInput;
    double steerInput;

    public GameObject car; 
    private StringBuilder sb = new StringBuilder();
    private string filename;
    private string filePath;
    // Start is called before the first frame update
    void Start()
    {
        filePath = getPath();
        string[] rowDataTemp = new string[7];
        rowDataTemp[0] = "Timestamp";
        rowDataTemp[1] = "x_POS";
        rowDataTemp[2] = "y_POS";
        rowDataTemp[3] = "z_POS";
        rowDataTemp[4] = "x_ANGLE";
        rowDataTemp[5] = "y_ANGLE";
        rowDataTemp[6] = "z_ANGLE";
        sb.AppendLine(string.Join(",", rowDataTemp));
        InvokeRepeating("ReportPose", 1.0f, 0.01f);

        gasInput = 0.0;
        breakInput = 0.0;
        steerInput = 0.0;

        // create stream info and outlet
        info = new liblsl.StreamInfo("UnityCamPos2", "CamPos", 10, 100, liblsl.channel_format_t.cf_float32, "CamPos_sourceID");
        // chns = info.desc().append_child("channels");

        outlet = new liblsl.StreamOutlet(info);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ReportPose()
    {
        string[] rowDataTemp = new string[7];
        rowDataTemp[0] = DateTime.UtcNow.ToString("hh.mm.ss.ffffff");
        rowDataTemp[1] = car.transform.position.x.ToString();
        rowDataTemp[2] = car.transform.position.y.ToString();
        rowDataTemp[3] = car.transform.position.z.ToString();
        rowDataTemp[4] = car.transform.eulerAngles.x.ToString();
        rowDataTemp[5] = car.transform.eulerAngles.y.ToString();
        rowDataTemp[6] = car.transform.eulerAngles.z.ToString();
        sb.AppendLine(string.Join(",", rowDataTemp));


        Inputs();
        //TODO: edit here for Lab Stream Layer code. 
        //Debug.Log("gas, break, steer: " + gasInput + "," + breakInput + "," + steerInput);

        float [] rowDataTempFloat = new float[10];
        rowDataTempFloat[0] = (float)(new TimeSpan(DateTime.Now.Ticks)).TotalMilliseconds;
        rowDataTempFloat[1] = car.transform.position.x;
        rowDataTempFloat[2] = car.transform.position.y;
        rowDataTempFloat[3] = car.transform.position.z;
        rowDataTempFloat[4] = car.transform.eulerAngles.x;
        rowDataTempFloat[5] = car.transform.eulerAngles.y;
        rowDataTempFloat[6] = car.transform.eulerAngles.z;
        rowDataTempFloat[7] = (float)gasInput;
        rowDataTempFloat[8] = (float)breakInput;
        rowDataTempFloat[9] = (float)steerInput;

        outlet.push_sample(rowDataTempFloat);
        

    }

    void Inputs()
    {
        //checks the RCCSettings are set to keyboard mode. Can create different case for different modes. 
        switch (RCCSettings.controllerType)
        {

            case RCC_Settings.ControllerType.Keyboard:
                gasInput = Input.GetAxis(RCCSettings.verticalInput);
                breakInput = Mathf.Clamp01(-Input.GetAxis(RCCSettings.verticalInput));
                steerInput = Input.GetAxis(RCCSettings.horizontalInput);
                break;
        }
    }

    // Following method is used to retrive the relative path as device platform
    //will also check if there already exists a file of that name
    private string getPath(){
        int count = 1;

        filename = Application.dataPath + string.Format("/CSV/Camera_data_{0}.txt", count);
        while (System.IO.File.Exists(filename))
        {
            count++;
            filename = Application.dataPath +string.Format("/CSV/Camera_data_{0}.txt", count);
        }
        return filename;
    }

// Show the number of calls to both messages. Debuggin purposes. 
    //void OnGUI()
    //{
    //    GUIStyle fontSize = new GUIStyle(GUI.skin.GetStyle("label"));
    //    fontSize.fontSize = 24;
    //    GUI.Label(new Rect(100, 100, 200, 50), "filename: " + filename); 
    //    GUI.Label(new Rect(100,150,200,50), "time" + DateTime.UtcNow.ToString("hh.mm.ss.ffffff"));
    //}

    void OnApplicationQuit()
    {
        Debug.Log("Begin writing to CSV");
        StreamWriter outStream = System.IO.File.CreateText(filePath);
        outStream.WriteLine(sb);
        outStream.Close();
        Debug.Log("Finsihed writing to CSV");
    }

}
