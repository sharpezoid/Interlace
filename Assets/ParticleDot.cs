using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ParticleDot : MonoBehaviour
{
    Image image; 
    RectTransform rt;

    float speed = 0;
    float rotationSpeed = 0;
    float reduction = 0.9f;

    // Use this for initialization
    void Start()
    {
        image = GetComponent<Image>();
        rt = GetComponent<RectTransform>();
        rt.Rotate(new Vector3(0, 0, Random.Range(0, 360)));

        speed = Random.Range(0, 15);
        rotationSpeed = Random.Range(0, 15);
    }
	
	// Update is called once per frame
	void Update ()
    {
        speed *= reduction;
        rotationSpeed *= reduction;

        rt.Translate(Vector3.right * speed * Time.deltaTime);
        image.color = Color.Lerp(image.color, new Color(image.color.r, image.color.g, image.color.b, 0), Time.deltaTime * 5);
	}
}