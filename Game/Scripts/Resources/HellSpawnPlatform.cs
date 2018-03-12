using UnityEngine;

public class HellSpawnPlatform : MonoBehaviour
{
    public GameObject parentEnemy;
    
    public void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject collidedObject = collision.gameObject;
        if (collidedObject.tag == Player.PLAYER_TAG) {
            collidedObject.SendMessage("AddDeath");
            parentEnemy.SendMessage("RecycleGameObject", SendMessageOptions.DontRequireReceiver);            
        }
    }       
}