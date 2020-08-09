using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIDesigner : MonoBehaviour
{
    GameManager manager;
    TMP_Text textBox;

    // Start is called before the first frame update
    void Start()
    {
        //init components
        manager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
        textBox = gameObject.GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        textBox.text = string.Format("Turns: {0}", manager.playerLivesLeft);
    }
}
