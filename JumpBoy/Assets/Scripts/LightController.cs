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

    Light2D light2D; 
  
    void Awake()
    {
        light2D = gameObject.GetComponent<Light2D>();
    }

    // Update is called once per frame

    void Update()
    {
        if (isLightChange)
        {
            if (light2D.lightType == Light2D.LightType.Point)
            {
                // light2D.pointLightInnerRadius = Mathf.PingPong(Time.time * lightChangeSpeed, lightMaxRange);
                // light2D.pointLightOuterRadius = Mathf.PingPong(Time.time * lightChangeSpeed, (lightMaxRange + 1));
                light2D.intensity = Mathf.PingPong(Time.time * lightChangeSpeed, lightMaxRange);

            }
        }
    }
    public void SetLight(string value)
    {
        switch (value)
        {
            case "Dead":
                {
                    light2D.intensity = valueDead;
                    break;
                }
            default:
                {
                    break;
                }
        }

    }
}
