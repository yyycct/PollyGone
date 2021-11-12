using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DigitalRuby.WeatherMaker
{
    public class WeatherTransitionControl : MonoBehaviour
    {
        public static WeatherTransitionControl instance;
        public bool cloudy;
        public bool rainy;
        public bool lighting;
        public float internsity = 0.5f;
        public WeatherMakerCloudType clouds;
        public WeatherMakerCloudType lastclouds;
        public float beforeRainTime;
        private float beforRainCounter = 0f;
        public float lightingTimeout;
        private float lightingTimer;
        private void Awake()
        {
            instance = this;
        }
        // Start is called before the first frame update
        void Start()
        {
        }

        // Update is called once per frame
        void Update()
        {
            if (DayControl.instance.raining)
            {
                if (beforRainCounter > 0)
                {
                    clouds = WeatherMakerCloudType.LightMediumScattered;
                    beforRainCounter -= Time.deltaTime;
                }
                else
                {
                    rainy = true;
                    lighting = true;
                    clouds = WeatherMakerCloudType.MediumHeavyScatteredStormy;
                 }

            }
            else if (DayControl.instance.cloudy)
            {
                clouds = WeatherMakerCloudType.MediumHeavyScattered;
                rainy = false;
                lighting = false;
            }
            else 
            {
                rainy = false;
                lighting = false;
                clouds = WeatherMakerCloudType.Light;
                beforRainCounter = beforeRainTime;

            }
            if (WeatherMakerPrecipitationManagerScript.Instance != null)
            {
                WeatherMakerPrecipitationManagerScript.Instance.Precipitation = (rainy ? WeatherMakerPrecipitationType.Rain : WeatherMakerPrecipitationType.None);
                WeatherMakerPrecipitationManagerScript.Instance.PrecipitationIntensity = internsity;
            }

            UpdateClouds();  
            if (lighting)
            {
                if (lightingTimer > 0f)
                {
                    lightingTimer -= Time.deltaTime;
                }
                else
                {
                    lightingTimer = lightingTimeout;
                    WeatherMakerThunderAndLightningScript.Instance.CallIntenseLightning();
                }
            }


        }
        private void UpdateClouds()
        {
            if (clouds == lastclouds)
            {
                return;
            }
            lastclouds = clouds;
            if (WeatherMakerLegacyCloudScript2D.Instance != null && WeatherMakerLegacyCloudScript2D.Instance.enabled &&
                WeatherMakerLegacyCloudScript2D.Instance.gameObject.activeInHierarchy)
            {
                if (clouds == WeatherMakerCloudType.None)
                {
                    WeatherMakerLegacyCloudScript2D.Instance.RemoveClouds();
                }
                else if (clouds != WeatherMakerCloudType.Custom)
                {
                    WeatherMakerLegacyCloudScript2D.Instance.CreateClouds();
                }
            }
            else if (WeatherMakerFullScreenCloudsScript.Instance != null && WeatherMakerFullScreenCloudsScript.Instance.enabled &&
                WeatherMakerFullScreenCloudsScript.Instance.gameObject.activeInHierarchy)
            {
                float duration = WeatherMakerPrecipitationManagerScript.Instance.PrecipitationChangeDuration;
                if (clouds == WeatherMakerCloudType.None)
                {
                    WeatherMakerFullScreenCloudsScript.Instance.HideCloudsAnimated(duration);
                }
                else if (clouds != WeatherMakerCloudType.Custom)
                {
                    WeatherMakerFullScreenCloudsScript.Instance.ShowCloudsAnimated(duration, "WeatherMakerCloudProfile_" + clouds.ToString());
                }
                else
                {
                    // custom clouds, do not modify current cloud script state
                }
            }
        }
    }
}
