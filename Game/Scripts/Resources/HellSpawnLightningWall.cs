using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HellSpawnLightningWall : MonoBehaviour {
	void Start ()
    {		
	}
	
	void Update ()
    {		
	}

    public void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject collidedObject = collision.gameObject;
        if (collidedObject.tag == Player.PLAYER_TAG) {
            gameObject.SendMessage("RecycleGameObject", SendMessageOptions.DontRequireReceiver);
            collidedObject.SendMessage("AddDeath");
        }
    }
}
