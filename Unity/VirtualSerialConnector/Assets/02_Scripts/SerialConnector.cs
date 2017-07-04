using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;
using System;

public class SerialConnector : MonoBehaviour {
    private const string COM_PORT = "COM40";
    private const int BAUDRATE = 9600;
    SerialPort stream = new SerialPort(@"\\.\"+ COM_PORT, BAUDRATE);
    private float[] lastVal = { 0, 0, 0 };

    void Start()
    {
        try
        {
            stream.Open();
        }
        catch(Exception e)
        {
            print("Exception : " + e);
        }
    }

    void Update()
    {
        if (stream.IsOpen)
        {
            string value = stream.ReadLine();
            if(value.Length > 0)
            {
                string[] vec3 = value.Split(',');
                if(vec3[0] != "" && vec3[1] != "" && vec3[2] != "")
                {
                    transform.position = new Vector3(
                        float.Parse(vec3[0]) - lastVal[0],
                        float.Parse(vec3[1]) - lastVal[1],
                        float.Parse(vec3[2]) - lastVal[2]
                        );

                    lastVal[0] = float.Parse(vec3[0]);
                    lastVal[1] = float.Parse(vec3[1]);
                    lastVal[2] = float.Parse(vec3[2]);
                    stream.BaseStream.Flush();
                }
            }
        }
    }
}
