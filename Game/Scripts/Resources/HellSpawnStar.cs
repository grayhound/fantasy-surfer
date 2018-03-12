using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HellSpawnStar : MonoBehaviour {
    public PlayerController playerController;
    public SFX sfx;

    void Start ()
    {		
	}
	
	void Update ()
    {
        transform.Rotate(-Vector3.forward * Time.deltaTime * 200);
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject collidedObject = collision.gameObject;
        if (collidedObject.tag == Player.PLAYER_TAG) {
            sfx.PlaySfxPowerup();
            playerController.StartSpeedBonus();
            gameObject.SendMessage("RecycleGameObject", SendMessageOptions.DontRequireReceiver);            
        }
    }
}
