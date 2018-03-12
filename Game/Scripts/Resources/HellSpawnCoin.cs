using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HellSpawnCoin : MonoBehaviour {
    public SFX sfx;
    public PlayerScore playerScore;
    public Spawner spawner;

    private int _spawnChainId;
    private bool _isLastInChain = false;

    public void OnCollisionEnter2D(Collision2D collision)
    {        
        GameObject collidedObject = collision.gameObject;        
        if (collidedObject.tag == Player.PLAYER_TAG) {
            sfx.PlaySfxCoinPickup();
            gameObject.SendMessage("HideHellSpawn", SendMessageOptions.DontRequireReceiver);
            AddPoints();
        }
    }

    public void AddPoints()
    {        
        spawner.addChainCount(_spawnChainId);
        if (_isLastInChain && spawner.isFullChainCheck(_spawnChainId)) {
            playerScore.AddPoints(10 * spawner.getChainAmount(_spawnChainId));
            //playerScore.AddBonus();            
        } else {
            CheckBonusReset();
            playerScore.AddPoints(10);
        }
    }

    private void CheckBonusReset()
    {
        if (!spawner.isPreviousFullChainCheck()) {
            playerScore.ResetBonus();
        }
    }

    public void SetSpawnChainId(int spawnChainId)
    {
        _spawnChainId = spawnChainId;
    }

    public void SetIsLastInChain(bool isLastInChain)
    {
        _isLastInChain = isLastInChain;
    }

    public void HellspawnMissed()
    {
        if (_isLastInChain) {
            spawner.isFullChainCheck(_spawnChainId);
        }
    }
}