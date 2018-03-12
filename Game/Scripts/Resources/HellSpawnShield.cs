using UnityEngine;
using System.Collections;

public class HellSpawnShield : MonoBehaviour {
    public Player player;

    void Start()
    {
    }

    void Update()
    {
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject collidedObject = collision.gameObject;
        if (collidedObject.tag == Player.PLAYER_TAG) {            
            player.StartShieldBonus();
            gameObject.SendMessage("RecycleGameObject", SendMessageOptions.DontRequireReceiver);
        }
    }
}
