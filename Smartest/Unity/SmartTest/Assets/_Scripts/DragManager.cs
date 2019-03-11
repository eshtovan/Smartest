using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum DragStates
{
    Car,
    Surface,
    Sensor
}
public class DragManager : MonoBehaviour
{
    public static DragManager instance;
    public Transform Dragable;
    public DragStates DragStates = DragStates.Surface;
    public LayerMask layers;

    void Awake()
    {
        if (instance == null)
            instance = this;
    }

    void OnDestroy()
    {
        if (instance == this)
            instance = null;
    }
           

    // Update is called once per frame
    void Update()
    {

        Move();

    }

    void Move()
    {
        if(!Dragable)
        {
            return;
        }
        RaycastHit hit;
        
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
   
        // The GameObject this script attached should be on layer "Surface"
        if (Physics.Raycast(ray, out hit, 30.0f,layers))
        {
            
            Dragable.position = new Vector3(hit.point.x, hit.point.y, hit.point.z);
            Dragable.eulerAngles= new Vector3(hit.transform.localRotation.x, hit.transform.rotation.y, hit.transform.rotation.z);

        }
    }
}
