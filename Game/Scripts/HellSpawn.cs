using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class HellSpawn : MonoBehaviour {
    public struct SpawnMovingParameters
    {
        public float flyTime;
        public float hellSpawnSpeed;
    }

    static public string HELLSPAWN_DOTWEEN_ID = "HellSpawn";
    static public string HELLSPAWN_MISS_CHECK_ID = "HellSpawnMissCheck";
    static public string HELLSPAWN_ENEMY_TAG = "HellSpawnEnemy";
    static public string HELLSPAWN_BLOCK_TAG = "HellSpawnBlock";

    public string _objectPoolType;

    private Vector3 _startPosition;

    public void StartMoving(SpawnMovingParameters spawnMovingParameters)
    {
        GetMovingTween(spawnMovingParameters);
    }

    public Tween GetMovingTween(SpawnMovingParameters spawnMovingParameters, 
                                bool useTimescale = true, bool withoutId = false,
                                bool hasOnComplete = true)
    {
        _startPosition = gameObject.transform.position;
        
        Tween result = gameObject.transform.DOMoveX(-_startPosition.x, spawnMovingParameters.flyTime);
        if (hasOnComplete) {
            result.OnComplete(RecycleGameObject);    
        }        
        if (!withoutId) {
            result.SetId(HELLSPAWN_DOTWEEN_ID);
        }
        if (useTimescale) {
            result.timeScale = spawnMovingParameters.hellSpawnSpeed;
        }
        result.ForceInit();
        return result;
    }   
   
    public void RecycleGameObject()
    {        
        Spawner.RemoveSpawnedObject(gameObject);
        RecycleOnly();        
    }

    public void RecycleOnly()
    {        
        gameObject.SendMessage("ResetGameObject", SendMessageOptions.DontRequireReceiver);
        gameObject.transform.DOKill();
        ObjectPool.Recycle(_objectPoolType, gameObject);
    }

    public void HideHellSpawn()
    {
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
    }    

    public void OnTriggerExit2D(Collider2D other)
    {        
        if (other.gameObject.tag == HELLSPAWN_MISS_CHECK_ID) {
            gameObject.SendMessage("HellspawnMissed", SendMessageOptions.DontRequireReceiver);
        }
    }
}
