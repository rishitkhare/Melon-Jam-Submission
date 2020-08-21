using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class TopDownLayering : MonoBehaviour
{
    SpriteRenderer myRenderer;
    // Start is called before the first frame update
    void Start()
    {
        myRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        myRenderer.sortingOrder = -1 * (int) transform.position.y;
    }
}
