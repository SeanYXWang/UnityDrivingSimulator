using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Globalization;
using System;

public class Repeat_Trajectory : MonoBehaviour
{
    public TextAsset csvfile;
    private char lineSeparator = '\n';
    private char fieldSeperator = ','; 
    private string [] records;
    private string record;
    private int curr_record;

    // Start is called before the first frame update
    void Start()
    {
        records = csvfile.text.Split(lineSeparator);
        curr_record = 1;
        InvokeRepeating("RepeatT", 1.0f, 0.01f);
    }

    void RepeatT()
    {
        if (curr_record < records.Length - 1)
        {
            record = records[curr_record];
            string[] fields = record.Split(fieldSeperator);

            try
            {
                float x_pos = float.Parse(fields[1], CultureInfo.InvariantCulture);
                float y_pos = float.Parse(fields[2], CultureInfo.InvariantCulture);
                float z_pos = float.Parse(fields[3], CultureInfo.InvariantCulture);
                float x_angle = float.Parse(fields[4], CultureInfo.InvariantCulture);
                float y_angle = float.Parse(fields[5], CultureInfo.InvariantCulture);
                float z_angle = float.Parse(fields[6], CultureInfo.InvariantCulture);
                transform.position = new Vector3(x_pos, y_pos, z_pos);
                transform.eulerAngles = new Vector3(x_angle, y_angle, z_angle);
            }
            catch (IndexOutOfRangeException e)
            {
                Debug.Log("there was an out of bounds exception for some reason");
                Debug.Log(e);
            }
            curr_record++;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
