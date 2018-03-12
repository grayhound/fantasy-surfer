using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerBullet : MonoBehaviour {
    public string objectPoolType;
    public SFX sfx;

    private float _flySpeed = 8.0f;
    private float _endPositionX = 11.0f;

    static public string PLAYER_BULLET_DOTWEEN_ID = "PlayerBullet";
    static public string PLAYER_BULLET_EXAMPLE_DOTWEEN_ID = "PlayerBulletExample";

    public void StartMoving()
    {
        sfx.PlaySfxBulletFire();
        gameObject.transform.DOMoveX(_endPositionX, _flySpeed)
                            .OnComplete(RecycleGameObject)
                            .SetId(PLAYER_BULLET_DOTWEEN_ID)
                            .SetSpeedBased();
    }

    public void StartMovingExample()
    {        
        gameObject.transform.DOMoveX(3.0f, _flySpeed)
                            .OnComplete(RecycleExampleGameObject)                            
                            .SetId(PLAYER_BULLET_EXAMPLE_DOTWEEN_ID)
                            .SetSpeedBased();
    }

    public void RecycleGameObject()
    {
        Spawner.RemoveSpawnedObject(gameObject);
        RecycleOnly();        
    }


    public void RecycleExampleGameObject()
    {
        RecycleOnly();
    }

    public void RecycleOnly()
    {        
        gameObject.transform.DOKill();
        ObjectPool.Recycle(objectPoolType, gameObject);
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject collidedObject = collision.gameObject;        
        if (collidedObject.tag == HellSpawn.HELLSPAWN_ENEMY_TAG) {
            RecycleGameObject();
            collidedObject.SendMessage("AddDeath");
        }
        if (collidedObject.tag == HellSpawn.HELLSPAWN_BLOCK_TAG) {
            RecycleGameObject();            
        }        
    }
}
