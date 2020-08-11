using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SpaceToSneezeText : MonoBehaviour
{
    public float averageFontSize = 15f;
    public float amplitude = 5f;
    public float speed = 60f;

    float textAnimate;

    GameManager manager;
    TMP_Text text;

    // Start is called before the first frame update
    void Start()
    {
        manager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
        text = gameObject.GetComponent<TMP_Text>();
        textAnimate = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if(manager.playerLivesLeft == 1)
        {
            text.enabled = true;
            AnimateText();
        }
        else
        {
            text.enabled = false;
        }
    }

    private void AnimateText()
    {
        textAnimate += speed * Time.deltaTime;

        text.fontSize = averageFontSize + amplitude * Mathf.Sin(textAnimate);
    }
}
