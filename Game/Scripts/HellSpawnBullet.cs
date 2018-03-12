using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class HellSpawnBullet : MonoBehaviour {
    static public string HELLSPAWN_BULLET_DOTWEEN_ID = "HellSpawnBullet";

    public string objectPoolType;
    public SFX sfx;

    private Vector3 _startPosition;

    private float _flySpeed = 5.0f;
    private float _endPositionX = -11.0f;    

    void Start()
    {
	}
	
	void Update()
    {		
	}

    public void StartMoving(float hellSpawnSpeed)
    {
        sfx.PlaySfxBulletFire();
        gameObject.transform.localEulerAngles = new Vector3(0, 0, 180);
        gameObject.transform.DOMoveX(_endPositionX, _flySpeed)
                            .OnComplete(RecycleGameObject)
                            .SetId(HELLSPAWN_BULLET_DOTWEEN_ID)
                            .SetSpeedBased()
                            .timeScale = hellSpawnSpeed;
    }

    public void RecycleGameObject()
    {
        RecycleOnly();
        Spawner.RemoveSpawnedObject(gameObject);
    }

    public void RecycleOnly()
    {
        gameObject.transform.DOKill();
        ObjectPool.Recycle(objectPoolType, gameObject);
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
