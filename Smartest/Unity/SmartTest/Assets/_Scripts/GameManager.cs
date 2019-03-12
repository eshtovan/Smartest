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

                GameObject newSensor = Instantiate(sen.Go, parent.transform);
            }
        }


    }

    //void CreateObject(string args)
    //{
    //    string[] commandParams= args.Split(',');

    //}

    //void CreateObject(Category, SensorType name, ProjectPath, Params)
    //{
    //    "CreateObject,'asdasdad:asdasda;asasdasdasd;asdasdsa'"
    //}

    //void RemoveObject(Category, Name, ProjectPath)
    //{

    //}

    //void RenameObject(Category, OldName, NewName, ProjectPath, Params)//– תשמש לשינוי שם של אובייקט ביוניטי
    //{

    //}

    //SetDisplayObject(Category, Name, ProjectPath) //– פונקציה שתשתמש לשינוי סביבות וכלי רכב במסך היוניטי
    //{

    //}

    //void GetAllObjects(ProjectPath, Category)  // – תשמש לצורך הבאת כל הנתונים לממשק ה- WPF לצורך הצגה למשתמש של המצב הנוכחי(תחזיר רשימה של אובייקטים צריך לסגור את הסוגים שלהם)
    //{

    //}
   
}







