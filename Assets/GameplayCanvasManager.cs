using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameplayCanvasManager : MonoBehaviour
{
    public List<Text> playerScoreTexts = new List<Text>();

    public Text countdownText;
    RectTransform countdownTextRect;

    public GameObject particleDot;

    float shake = 0;
    float shakeAmount = 0.7f;
    float decreaseFactor = 1.0f;

    RectTransform rt;

    void Start()
    {
        rt = GetComponent<RectTransform>();

        countdownTextRect = countdownText.GetComponent<RectTransform>();
    }

    public void SetCountdownText(string _text)
    {
        Debug.Log("Setting Countdown Text To : " + _text);
        countdownText.text = _text;
        countdownTextRect.sizeDelta = new Vector2(720, 480);
        countdownText.color = new Color(countdownText.color.r, countdownText.color.g, countdownText.color.b, 1f);
    }

    void Update()
    {
        countdownTextRect.sizeDelta = Vector2.Lerp(countdownTextRect.sizeDelta, Vector2.zero, Time.deltaTime * 3.5f);
        countdownText.color = Color.Lerp(countdownText.color, new Color(countdownText.color.r, countdownText.color.g, countdownText.color.b, 0), Time.deltaTime * 2);

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




    public void ShowParticles(Vector3 localPos, int count = 3)
    {
        for (int i=0; i < count; i++)
        {
            GameObject newParticle = GameObject.Instantiate(particleDot, gameObject.transform);
            newParticle.GetComponent<RectTransform>().localPosition = localPos;
        }
    }

    public void Shake()
    {
        shake = 5.0f;
    }
}
