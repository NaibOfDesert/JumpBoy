using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class LightController : MonoBehaviour
{
    [SerializeField] bool isLightChange;
    // [SerializeField] float lightStartColor;
    [SerializeField] float lightMaxIntensity;
    [SerializeField] float lightMaxRange;
    [SerializeField] float lightChangeSpeed;

    [SerializeField] float valueDead;

    Light2D light; 
  
    void Awake()
    {
        light = gameObject.GetComponent<Light2D>();
    }

    // Update is called once per frame

    void Update()
    {
        if (isLightChange)
        {
            if (light.lightType == Light2D.LightType.Point)
            {
                light.pointLightInnerRadius = Mathf.PingPong(Time.time * lightChangeSpeed, lightMaxRange / 2);
                light.pointLightOuterRadius = Mathf.PingPong(Time.time * lightChangeSpeed, lightMaxRange);
                light.intensity = Mathf.PingPong(Time.time * lightChangeSpeed, lightMaxIntensity);

            }
        }
    }
    public void SetLight(string value)
    {
        switch (value)
        {
            case "Dead":
                {
                    // light.intensity = valueDead; // to rebuild
                    break;
                }
            default:
                {
                    break;
                }
        }

    }
}
