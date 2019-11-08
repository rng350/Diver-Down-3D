using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Underwater : MonoBehaviour
{

	[SerializeField] bool aboveWaterFog;
	[SerializeField] Color aboveWaterFogColor;
	[SerializeField] float aboveWaterFogDensity;
	[SerializeField] Material aboveWaterSkybox;

	[SerializeField] float underwaterLevel;
	[SerializeField] Color underwaterFogColor;
	[SerializeField] float underwaterFogDensity;
	[SerializeField] Material noSkybox;

	// Start is called before the first frame update
	void Start()
    {
		aboveWaterFog = RenderSettings.fog;
		aboveWaterFogColor = RenderSettings.fogColor;
		aboveWaterFogDensity = RenderSettings.fogDensity;
		aboveWaterSkybox = RenderSettings.skybox;

		GameObject seaTop = GameObject.Find("SeaTop");
		underwaterLevel = seaTop.transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < underwaterLevel)
		{
			RenderSettings.fog = true;
			RenderSettings.fogColor = underwaterFogColor;
			RenderSettings.fogDensity = underwaterFogDensity;
			RenderSettings.skybox = noSkybox;
		} else
		{
			RenderSettings.fog = aboveWaterFog;
			RenderSettings.fogColor = aboveWaterFogColor;
			RenderSettings.fogDensity = aboveWaterFogDensity;
			RenderSettings.skybox = aboveWaterSkybox;
		}
    }
}
