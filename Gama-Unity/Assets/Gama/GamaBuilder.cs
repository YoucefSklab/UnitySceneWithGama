using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Nextzen;
using UnityEngine;

public class GamaBuilder : MonoBehaviour {

	// Use this for initialization

	public GameObject gamaBuilder;	

	public RegionMap regionMap;
	protected MonoBehaviour[] scripts { get ; set ;}
	void Start () {
		gamaBuilder = GameObject.Find("GamaBuilder");
	
		regionMap = (RegionMap) FindObjectOfType(typeof(RegionMap));

		regionMap.ApiKey =  "NO9atv-JQf289NztiKv45g";
        regionMap.UnitsPerMeter = 1.0f;
        //regionMap.RegionName = "GamaMap";
        regionMap.Area = new TileArea(
            new LngLat(-74.009463, 40.711446),
            new LngLat(-73.999306, 40.706939),
            16);
		
    //regionMap.DownloadTilesAsync();
    //regionMap.GenerateSceneGraph();




	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
