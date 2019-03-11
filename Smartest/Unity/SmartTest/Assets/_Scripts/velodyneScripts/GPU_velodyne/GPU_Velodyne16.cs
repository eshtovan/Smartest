using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using SPINACH.iSCentralDispatch;
using UnityEngine.EventSystems;


public class VelodyneFrame
{
    public Color[] ranges;

    public Vector3 rotation;

    public Vector3 Position;

    public Vector3 Right;

    public float cameraRotationAngle;

    public float TimeStamp;
}

public class GPU_Velodyne16 : MonoBehaviour
{


    VelodyneWrapper vel16ICDinterface;

    public string ICD_ConfigFile;
    GPULidar Sensor;
    Camera depthCam;
    Texture2D RangesSamples;


    //Public vars
    public float RotateFrequency = 10.0f;
    public float AngularResolution = 0.2f, LowerAngle = -10f, HigherAngle = 10f, RotationAngle = 360f;
    public int Channels = 16, SuperSample = 1;
    public float MeasurementRange = 120f, MinMeasurementRange = 0.2f, MeasurementAccuracy = 0.02f;
    public float horizontalFOV, verticalFOV;
    public float cameraRotationAngle;
    public int BLOCKS_ON_PACKET = 24;
    public float drawSize = 0.1f, drawTime = 0.1f;
    public Color drawColor = Color.red;
    public Text displayText;
    public List<VelodyneFrame> FramesQueue = new List<VelodyneFrame>();

    //Bool Vars
    public bool sendDataOnICD;
    public bool Rotate;
    public bool DrawLidar; // for digug
    public bool UseThreading = false;


    //Private Vars
    int m_ResWidth, m_ResHeight;
    int m_ColumnsPerPhysStep;
    float m_VerticalAngularResolution;
    float m_Camera360Counter = 0;
    float m_LastFull360 = 0;
    private int m_BlocksCounter = 0;
    RenderTexture m_CurrentActiveRt;
    Color[] m_Ranges;
    float m_Accum = 0;
    float m_Frames = 0;
    float m_Up = 0;
    int m_I = 0;
    private float m_Delay = 0.005f;
    private float m_TimeSinceLastCalled;
    private float hAng;




    void Start()
    {

        Application.targetFrameRate = 200;

        Sensor = GetComponentInChildren<GPULidar>();
        depthCam = GetComponentInChildren<Camera>();

        // Calculation of FOV 
        verticalFOV = HigherAngle - LowerAngle;
        m_VerticalAngularResolution = verticalFOV / (Channels - 1f);
        horizontalFOV = Time.fixedDeltaTime * 360.0f * RotateFrequency / SuperSample;

        // Calculation of the Camera Projection Mat 
        Matrix4x4 projMat = depthCam.projectionMatrix;
        float horizontalMval = 1.0f / (Mathf.Tan((horizontalFOV / 2.0f) * Mathf.Deg2Rad));
        float verticalMval = 1.0f / (Mathf.Tan((verticalFOV / 2.0f) * Mathf.Deg2Rad));
        projMat[0, 0] = horizontalMval;
        projMat[1, 1] = verticalMval;
        depthCam.projectionMatrix = projMat;

        // target Texture size calculation 
        m_ColumnsPerPhysStep = Mathf.RoundToInt(Time.fixedDeltaTime * RotationAngle * RotateFrequency / AngularResolution) / SuperSample;
        m_ResWidth = m_ColumnsPerPhysStep;
        m_ResHeight = Channels;
        depthCam.targetTexture = new RenderTexture(m_ResWidth, m_ResHeight, 1, RenderTextureFormat.RFloat, RenderTextureReadWrite.Default);
        RangesSamples = new Texture2D(m_ResWidth, m_ResHeight, TextureFormat.RGBAFloat, false);


        depthCam.farClipPlane = MeasurementRange;
        depthCam.nearClipPlane = MinMeasurementRange;

        // initial direction of the depthCam scan window center
        cameraRotationAngle = horizontalFOV / 2.0f;
        Sensor.transform.localEulerAngles = new Vector3(-(HigherAngle + LowerAngle) / 2.0f, cameraRotationAngle, 0);

        // activtion of the ICD interface    
        if (sendDataOnICD)
        {
            vel16ICDinterface = new VelodyneWrapper(ICD_ConfigFile, true);
        }

        if (UseThreading)
        {
            int bar1ID = iSCentralDispatch.DispatchNewThread("ProcessVelodyne", ProcessVelodyneThread);
            iSCentralDispatch.SetPriorityForThread(bar1ID, iSCDThreadPriority.VeryHigh);
            iSCentralDispatch.SetTargetFramerate(100);
        }


      
    }
  
    
    void Update()
    {
       
        m_CurrentActiveRt = RenderTexture.active;
        RenderTexture.active = depthCam.targetTexture;

        // When a RenderTexture becomes active its hardware rendering context is created
        // depthCam.Render();
        RangesSamples.ReadPixels(new Rect(0, 0, m_ResWidth, m_ResHeight), 0,0); // copy a rectangular pix el area from the currently active RenderTexture 
        RangesSamples.Apply();
        m_Ranges = RangesSamples.GetPixels();

        if (UseThreading)
        {
            VelodyneFrame newFrame = new VelodyneFrame();
            newFrame.ranges = m_Ranges;
            newFrame.TimeStamp = Time.fixedTime;
            newFrame.rotation = Sensor.transform.up;
            newFrame.cameraRotationAngle = cameraRotationAngle;
            FramesQueue.Add(newFrame);

        }
        else
        {
            VelodyneCalcAndSendData();
        }


        if (Rotate)
        {

            cameraRotationAngle += horizontalFOV;
            Sensor.transform.localEulerAngles = new Vector3(-(HigherAngle + LowerAngle) / 2.0f, cameraRotationAngle, 0);
            m_Camera360Counter += horizontalFOV;
            if (m_Camera360Counter >= 360)
            {
                m_Camera360Counter = 0;
                Debug.LogWarning("Time it took for full 360 = " + (Time.time - m_LastFull360));
                m_LastFull360 = Time.time;
            }
        }

        RenderTexture.active = m_CurrentActiveRt;
        m_Accum += Time.timeScale / Time.deltaTime;
        m_Frames++;
        float fps = m_Accum / m_Frames;


        //Print
        //displayText.text = "Velodyne: Freq[Hz]=" + RotateFrequency.ToString() + " \n" +
        //                   "vFOV[deg]=" + verticalFOV.ToString("0.00") + " vRes[deg]= " +
        //                   m_VerticalAngularResolution.ToString("0.00") + "\n" +
        //                   "hFOV[deg]=" + RotationAngle.ToString("0.00") + " hRes[deg]= " +
        //                   AngularResolution.ToString("0.00") + " DeltaTime =" + Time.deltaTime + " FPS =" + fps;

        
    }


    private void VelodyneCalcAndSendData()
    {
        hAng = -horizontalFOV / 2.0f;
        for (int i = 0; i < m_ResWidth; i++) //columns
        {
            float vAng = HigherAngle;
            for (int j = 0; j < Channels; j++) //rows
            {
              float range = (m_Ranges[j * m_ResWidth + i].r * MeasurementRange) / (Mathf.Cos(hAng * Mathf.Deg2Rad) * Mathf.Cos(vAng * Mathf.Deg2Rad));

                if (range >= MeasurementRange)
                    range = 0;

                if (sendDataOnICD)
                    vel16ICDinterface.SetChannel((double)range, 0);

                if (DrawLidar)
                {
                    Vector3 rangePointPos = Sensor.transform.position + Quaternion.AngleAxis(vAng, Sensor.transform.right) * Quaternion.AngleAxis(hAng, Sensor.transform.up) * Sensor.transform.forward * range;
                    Debug.DrawLine(rangePointPos, rangePointPos + Vector3.up * drawSize, drawColor, drawTime);
                }
                vAng = vAng - m_VerticalAngularResolution;
            }
            hAng = hAng + AngularResolution;

            if (sendDataOnICD)
            {
                float columnAng = Mathf.Repeat(-horizontalFOV / 2.0f + cameraRotationAngle + i * AngularResolution, 360.0f);
                // displayText.text += "   " + columnAng.ToString();
                vel16ICDinterface.SetAzimuth((double)columnAng);
                vel16ICDinterface.SetTimeStamp(Time.fixedTime);
                vel16ICDinterface.CloseBlock();
                m_BlocksCounter++;
                if (m_BlocksCounter == BLOCKS_ON_PACKET)
                {
                   vel16ICDinterface.SendData();
                   m_BlocksCounter = 0;
                //   displayText.text = "";
                }
            }
        }

        m_Ranges = new Color[0];
    }
    public void ProcessVelodyneThread()
    {
        while (true)
        {
            iSCentralDispatch.LifeReport();

            if (FramesQueue.Count > 0)
            {
                                    
                iSCDDebug.LogWarning("New Frame : "+DateTime.UtcNow.ToLongTimeString() + "Processing Frame with Timestamp " +FramesQueue[0].TimeStamp);

                float hAng = -horizontalFOV / 2.0f;
                for (int i = 0; i < m_ResWidth; i++) //columns
                {
                    float vAng = HigherAngle;
                    for (int j = 0; j < Channels; j++) //rows
                    {
                        float range = (FramesQueue[0].ranges[j * m_ResWidth + i].r * MeasurementRange) / (Mathf.Cos(hAng * Mathf.Deg2Rad) * Mathf.Cos(vAng * Mathf.Deg2Rad));

                        if (range >= MeasurementRange)
                            range = 0;

                        if (sendDataOnICD)
                            vel16ICDinterface.SetChannel((double)range, 0);

                        if (DrawLidar)
                        {
                            Vector3 rangePointPos = Sensor.transform.position + Quaternion.AngleAxis(vAng, Sensor.transform.right) * Quaternion.AngleAxis(hAng, Sensor.transform.up) * Sensor.transform.forward * range;
                            Debug.DrawLine(rangePointPos, rangePointPos + Vector3.up * drawSize, drawColor, drawTime);
                        }
                        vAng = vAng - m_VerticalAngularResolution;
                    }
                    hAng = hAng + AngularResolution;

                    if (sendDataOnICD)
                    {

                        float columnAng = Mathf.Repeat(-horizontalFOV / 2.0f + FramesQueue[0].cameraRotationAngle + i * AngularResolution, 360.0f);
                        // displayText.text += "   " + columnAng.ToString();
                        vel16ICDinterface.SetAzimuth((double)columnAng);
                        vel16ICDinterface.SetTimeStamp(FramesQueue[0].TimeStamp);
                        vel16ICDinterface.CloseBlock();
                        m_BlocksCounter++;
                        if (m_BlocksCounter == BLOCKS_ON_PACKET)
                        {
                            vel16ICDinterface.SendData();
                            m_BlocksCounter = 0;
                        }
                    }
                }
                FramesQueue.RemoveAt(0);
            }
        }
    }
}



