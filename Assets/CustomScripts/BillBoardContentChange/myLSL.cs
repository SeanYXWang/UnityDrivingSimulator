using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LSL;

public class myLSL : MonoBehaviour
{
    // create stream info and outlet
    private liblsl.StreamInfo info;
    // liblsl.StreamOutlet outlet;
    private liblsl.XMLElement chns;
    // create outlet for the stream
    private liblsl.StreamOutlet outlet;

    float[] marker = new float[1];

    GameObject[] Billboards = new GameObject[24];

    public static bool checkFlag = false;
    public static bool semaphore = true;

    void Start()
    {
        // create stream info and outlet
        info = new liblsl.StreamInfo("BillboardMarker", "Marker", 1, 0, liblsl.channel_format_t.cf_float32, "MarkerID");
        // chns = info.desc().append_child("channels");

        outlet = new liblsl.StreamOutlet(info);

        marker[0] = 5.0f;
    }

 
    void Update()
    {
        checkFlag = (ads_change.flag || ads_change_1.flag || ads_change_2.flag || ads_change_3.flag 
                || ads_change_4.flag || ads_change_5.flag || ads_change_6.flag || ads_change_7.flag 
                || ads_change_8.flag || ads_change_9.flag || ads_change_10.flag || ads_change_11.flag 
                || ads_change_12.flag || ads_change_13.flag || ads_change_14.flag || ads_change_15.flag 
                || ads_change_16.flag || ads_change_17.flag || ads_change_18.flag || ads_change_19.flag 
                || ads_change_20.flag || ads_change_21.flag || ads_change_22.flag || ads_change_23.flag || ads_change_24.flag);
        if (checkFlag && semaphore)
        {
            print("CHECK FLAG ============================== " + checkFlag);

            outlet.push_sample(marker);
            semaphore = false;
        }
        else
        {
            if (!checkFlag)
            {
                semaphore = true;
            }
            //print(" ============================== CHECK FLAG ===   " + checkFlag);
        }
            
    }
}
