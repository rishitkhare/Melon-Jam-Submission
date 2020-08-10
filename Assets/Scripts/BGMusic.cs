using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BGMusic : MonoBehaviour
{

    public AudioSource sneeze;
    //public AudioSource ;

    // Start is called before the first frame update
    void Start()
    {
        Object.DontDestroyOnLoad(gameObject);
    }

    void Update()
    {
        if (SceneManager.GetActiveScene().name.Equals("Startup"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        if(Input.GetKeyDown("p"))
        {
            SneezeSound();
        }
    }

    public void SneezeSound()
    {
        sneeze.Play();
    }
}
