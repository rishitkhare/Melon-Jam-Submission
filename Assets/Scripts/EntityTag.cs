using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityTag : MonoBehaviour
{
    public string myTag;
    GameManager manager;

    public static readonly List<string> possibleTags = new List<string>()
    {"Goose", "Generic", "Old", "Germaphobe", "Procrastinator", "Skateboarder", "Spitter"};

    void Start() {
        manager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
        if(!possibleTags.Contains(myTag)) {
            myTag = "Generic";
        }
    }

    public bool HasTag(string givenTag) {
        return myTag.Equals(givenTag);
    }

    public void SetTag(string givenTag) {
        if(possibleTags.Contains(givenTag)) {
            myTag = (givenTag);
        }
        else {
            myTag = "Generic";
        }
    }

    public void GivePlayerControl() {
        //Give player control over the object
        gameObject.AddComponent<PlayerMovement>();

        //Tag new object with Player and remove tag from old player
        GameObject oldPlayer = GameObject.FindGameObjectWithTag("Player");
        oldPlayer.tag = "Untagged";
        gameObject.tag = "Player";

        //kill the old object
        oldPlayer.GetComponent<PlayerMovement>().Die();

        //reset turn count and update manager's Player object reference
        manager.UpdatePlayerReference();
        manager.SetPlayerLives(GetNewTurnCount());
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
