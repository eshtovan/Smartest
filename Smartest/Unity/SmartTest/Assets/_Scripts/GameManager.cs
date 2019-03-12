using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SensorType
{
    Velodyne,
    Novotel,
    Tiltan

}
public class Sensor
{
    public int SensorID;
    public SensorType SensorType;
    public Vector3 SensorLocation;
    public GameObject Go;

}
public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    public Sensor[] sensors;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }


    public void CreateSensor(SensorType sensorType)
    {
        GameObject parent = GameObject.FindGameObjectWithTag("Car");
        foreach (var sen in sensors)
        {
            if (sen.SensorType == sensorType)
            {
              
                GameObject newSensor = Instantiate(sen.Go,parent.transform);
            }
        }

    }


}
