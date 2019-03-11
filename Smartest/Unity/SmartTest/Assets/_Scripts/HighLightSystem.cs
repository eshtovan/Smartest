using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighLightSystem : MonoBehaviour
{
    public static HighLightSystem Instance;
    [SerializeField] private Highlight _trans;
    void Awake()
    {
        if (Instance == null)
            Instance = this;
    }
    void OnDestroy()
    {
        if (Instance == this)
            Instance = null;
    }
    
    public virtual void LightState(float amount , Highlight trans)
    {
      if (this._trans !=null)
          this._trans._rend.material.SetFloat("_OutlineWidth", 0);
     this._trans = trans;
     this._trans._rend.material.SetFloat("_OutlineWidth", amount);
    }





}
