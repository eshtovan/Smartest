using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Drag : MonoBehaviour
{

    public Transform DragObject;
 
    public LayerMask layers;
    private Rigidbody[] rg;

    public void Start()
    {

        if (!DragObject)
        {
            DragObject = transform;
        }
    }
    private void OnMouseDown()
    {
            EventManager.TriggerEvent("Drag");
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100f) && hit.transform== DragObject)
            {
                if (DragObject.GetComponentInChildren<Rigidbody>())
                {
                    rg = DragObject.GetComponentsInChildren<Rigidbody>();
                    foreach (var rigid in rg)
                    {
                        rigid.isKinematic = false;
                    }
                }
              DragManager.instance.Dragable = DragObject;
              DragManager.instance.layers = layers;
              //cameraSmoothFollow.target = transform;


            }
        
    }
       
    private void OnMouseUp()
    {
        DragManager.instance.Dragable = null;
        EventManager.TriggerEvent("Drag");
        if (rg != null)
        {
            foreach (var rigid in rg)
            {
                rigid.isKinematic = true;
            }
        }
   

    }


  

  
}
