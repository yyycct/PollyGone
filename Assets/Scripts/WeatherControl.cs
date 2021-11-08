using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DigitalRuby.WeatherMaker
{
    public class WeatherControl : MonoBehaviour
    {
        public bool cloudy;
        public bool rainy;
        public bool lighting;
        public float internsity = 0.5f;
        public WeatherMakerCloudType clouds;
        public WeatherMakerCloudType lastclouds;
        public float timerTotal;
        private float timer;
        public float lightingTimeout;
        private float lightingTimer;
        // Start is called before the first frame update
        void Start()
        {
            timer = timerTotal;
        }

        // Update is called once per frame
        void Update()
        {
            
            if(timer <= 0f)
            {
                clouds = WeatherMakerCloudType.HeavyDark;
            }
            if (WeatherMakerPrecipitationManagerScript.Instance != null)
            {
                WeatherMakerPrecipitationManagerScript.Instance.Precipitation = (rainy ? WeatherMakerPrecipitationType.Rain : WeatherMakerPrecipitationType.None);
                WeatherMakerPrecipitationManagerScript.Instance.PrecipitationIntensity = internsity;
            }
            if (cloudy)
            {
                UpdateClouds();
                timer -= Time.deltaTime;
            }
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
