using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireAnimation : MonoBehaviour
{
    public Animator FireFX;
    private ParticleSystem ps;
    bool settingUpFire = false;
    float settingTime = 2f;
    float timePortion = 0f;
    public static FireAnimation instance;
    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        ps = this.transform.GetChild(0).GetComponent<ParticleSystem>();
    }

    private void Update()
    {
        if (settingUpFire)
        {
            timePortion += 1/settingTime;
            ps.startSize = Mathf.Lerp(0f, 0.3f, Time.deltaTime * timePortion);
            ps.maxParticles = (int)Mathf.Lerp(0f, 50f, Time.deltaTime * timePortion);
            if (ps.maxParticles > 45)
            {
                ps.maxParticles = 50;
                ps.startSize = 0.3f;
                settingUpFire = false;
            }
        }
    }

    public void StartFireAnim()
    {
        ps = this.transform.GetChild(0).GetComponent<ParticleSystem>();
        settingUpFire = true;
    }
}
