using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasShake : MonoBehaviour
{
    float shake = 0;
    float shakeAmount = 0.7f;
    float decreaseFactor = 1.0f;

    Vector3 originalPosition;

    RectTransform rt;

    void Start()
    {
        rt = GetComponent<RectTransform>();
        originalPosition = rt.position;
    }

    void Update()
    {
        if (shake > 0)
        {
            rt.localPosition = Random.insideUnitSphere * shakeAmount;
            shake -= Time.deltaTime * decreaseFactor;
        }
        else
        {
            shake = 0.0f;
        }
    }

    public void Shake(float value = 5.0f)
    {
        shake = value;
    }
}
