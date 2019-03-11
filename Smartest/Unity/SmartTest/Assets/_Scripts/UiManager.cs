using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiManager : MonoBehaviour
{

    public static UiManager Instance;

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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void ChangeTerrainSurface()
    {
        EventManager.TriggerEvent("ChangeTerrainSurfce");
    }

}
