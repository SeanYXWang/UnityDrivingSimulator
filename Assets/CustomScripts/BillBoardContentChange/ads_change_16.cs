using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LSL;

public class ads_change_16 : MonoBehaviour
{
    public Material boardMaterial;
    public GameObject car;
    public GameObject nextBillboard;
    // set the threshold distance between car and billboard
    public float Threshold = 50.0f;
    public static bool flag = false;

        /*
    // create stream info and outlet
    private liblsl.StreamInfo info;
    // liblsl.StreamOutlet outlet;
    private liblsl.XMLElement chns;
    // create outlet for the stream
    private liblsl.StreamOutlet outlet;

    float [] marker = new float[1];
    */
    public float nextBillboarPositionXFloat;
    void Start()
    {
        /*
        // create stream info and outlet
        info = new liblsl.StreamInfo("BillboardMarker", "Marker", 10, 100, liblsl.channel_format_t.cf_float32, "MarkerID");
        // chns = info.desc().append_child("channels");

        outlet = new liblsl.StreamOutlet(info);

        marker[0] = 5.5f;
        */
        nextBillboarPositionXFloat = nextBillboard.transform.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        float carPositionXFloat;
        

        carPositionXFloat = car.transform.position.x;
        

        //print(" ------------------- rowDataTempFloat = " + carPositionXFloat);


        // This if statment is a temporary solution to deal with car spwan cause carPositionXFloat = 7 
        if (carPositionXFloat > 10.0f)
        {
            // set up the distance here to trigger the billboard content change
            if (Math.Abs(nextBillboarPositionXFloat - carPositionXFloat) < Threshold)
            {
                //print("Materials === " + Resources.FindObjectsOfTypeAll(typeof(Material)));
                //print("Materials === === === " + materialArray[0]);
                // for (int i=0; i<materialArray.Length; i++)
                //    print(" ^^^^^^^^^^^^^ materialArray[" + i + "] = " + materialArray[i]);
                //Material[] newMaterials = new Material[] { boardMaterial };
                //GetComponent<Renderer>().materials = newMaterials;
                flag = true;
                GetComponent<MeshRenderer>().material = new Material(boardMaterial);
                //print("CHECK FLAG ========================================= " + flag);
                //print(" distance === " + nextBillboarPositionXFloat);
                //outlet.push_sample(marker);
                //flag = 1;
            }
            if (Math.Abs(nextBillboarPositionXFloat - carPositionXFloat) > Threshold)
            {
                flag = false;
                //print("CHECK FLAG ======================== " + flag);
                //print(" distance === " + nextBillboarPositionXFloat);
            }
        }
        
        /*
        if (carPositionXFloat > 806 && flag == 1)
        {
            GetComponent<MeshRenderer>().material = new Material(boardMaterial);
            flag = 2;
        }
        */


    }

}
