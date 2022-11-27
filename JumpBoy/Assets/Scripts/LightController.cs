using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightController : MonoBehaviour
{
    [SerializeField] bool isLightChange;
    [SerializeField] float lightMaxIntensity;
    [SerializeField] float lightMaxRange;
    [SerializeField] float lightChangeSpeed;
    [SerializeField] float valueDead;

    UnityEngine.Rendering.Universal.Light2D light2D; 
  
    void Awake()
    {
        light2D = gameObject.GetComponent<UnityEngine.Rendering.Universal.Light2D>();
    }
    void Update()
    {
        if (isLightChange)
        {
            if (light2D.lightType == UnityEngine.Rendering.Universal.Light2D.LightType.Point)
            {
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
