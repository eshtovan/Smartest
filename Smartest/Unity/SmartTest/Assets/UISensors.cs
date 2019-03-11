using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class Sensorbutton
{
    public Image Iamge;
    public Text Text;
    public Action Action;

    }
public class UISensors : UiElement
{

    public List<Sensorbutton> SensorButtons;


    // Start is called before the first frame update

    public override void OnEnable()
    {
        EventManager.StartListening("UpdateLowerPanel", UpdateLowerPanel);
    }
    
    public override void OnDisable()
    {
        EventManager.StopListening("UpdateLowerPanel", UpdateLowerPanel);
    }
    
    void Start()
    {
        
    }

    // Update is called once per frame

    void UpdateLowerPanel()
    {

    }
}
