using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeTerrain : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject TerrainHigh,TerrianLow;
    

    void Start()
    {
        
    }

    void OnEnable()
    {
        EventManager.StartListening("ChangeTerrainSurfce", ChangeTerrainSurfce);
    }

    void OnDisable()
    {
        EventManager.StopListening("ChangeTerrainSurfce", ChangeTerrainSurfce);
    }

    private void ChangeTerrainSurfce()
    {
        if (TerrainHigh.activeInHierarchy)
        {
            TerrianLow.SetActive(true);
            TerrainHigh.SetActive(false);
        }
        else if(TerrianLow.activeInHierarchy)
        {
            TerrianLow.SetActive(false);
            TerrainHigh.SetActive(true);
        }
    }
}
