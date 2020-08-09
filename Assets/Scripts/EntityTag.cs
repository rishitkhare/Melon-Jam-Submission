using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityTag : MonoBehaviour
{
    public string myTag;
    GameManager manager;

    public static readonly List<string> possibleTags = new List<string>()
    {"Goose", "Generic", "Old", "Germaphobe", "Procrastinator", "Skateboarder"};

    void Start() {
        manager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
    }

    public bool HasTag(string givenTag) {
        return myTag.Equals(givenTag);
    }

    public void SetTag(string givenTag) {
        myTag = (givenTag);
    }

    public void GivePlayerControl() {
        //Give player control over the object
        gameObject.AddComponent<PlayerMovement>();

        //kill the old object
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>().Die();

        //reset turn count
        manager.SetPlayerLives(GetNewTurnCount());

        //Tag new object with Player
        gameObject.tag = "Player";
    }

    public int GetNewTurnCount() {
        if(myTag.Equals("Old")) {
            return 3;
        }
        else if(myTag.Equals("Procrastinator")) {
            return 10;
        }
        else {
            return 5;
        }
    }
}
