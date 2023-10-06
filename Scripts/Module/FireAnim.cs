using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering.HighDefinition;

public class FireAnim : MonoBehaviour
{
    [SerializeField] AnimationCurve curve;
    [SerializeField] float speed;
    [SerializeField] float intensity;

    HDAdditionalLightData fireLight;
    float t = 0f;

    // Start is called before the first frame update
    void Start()
    {
        fireLight = GetComponent<HDAdditionalLightData>();
    }
    // Update is called once per frame
    void Update()
    {
        t += Time.deltaTime;
        fireLight.intensity = intensity * curve.Evaluate(t * speed);
    }
}
