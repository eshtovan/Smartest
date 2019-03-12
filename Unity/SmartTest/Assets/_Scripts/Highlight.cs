using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Highlight : MonoBehaviour
{
   [SerializeField] private bool _islightOn = false;
 
   [Range(0,1)]
   [SerializeField] private float _amount;
  
   public Renderer _rend;
    void Start()
    {
        _rend = GetComponentInChildren<Renderer>();
        _rend.material.shader = Shader.Find("Outlined/Uniform");
    }
    public  void OnMouseDown()
    {
     HighLightSystem.Instance.LightState(_amount, this);
       _islightOn = true;
    }
   

    
}
