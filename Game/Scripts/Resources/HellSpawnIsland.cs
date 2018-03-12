using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HellSpawnIsland : MonoBehaviour {    
    public void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject collidedObject = collision.gameObject;
        if (collidedObject.tag == Player.PLAYER_TAG) {
            collidedObject.SendMessage("AddDeath");
            gameObject.SendMessage("RecycleGameObject", SendMessageOptions.DontRequireReceiver);            
        }
    }
}
