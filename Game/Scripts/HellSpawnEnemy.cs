using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class HellSpawnEnemy : MonoBehaviour {
    public PlayerScore playerScore;
    public GameObject enemyHealthBar;
    public GameObject[] enemyHealthBarLives;

    private int enemyLives = 3;
    private int enemyLivesStart = 3;
    private int enemyLivesMin = 1;
    private int enemyLivesMax = 3;
    private float _blinkDeadHellSpawnInterval = 0.3f;

    private int _scorePerLife = 50;

    private SpriteRenderer hellSpawnEnemySprite;

    public void AddDeath()
    {
        enemyLives--;        
        if (enemyLives <= 0) {
            KillHellSpawnEnemy();
            BlinkDeadHellSpawn();
        }
        UpdateEnemyHealthBar();
    }

    private void BlinkDeadHellSpawn()
    {
        Sequence blinkSequence = DOTween.Sequence();
        _addBlinkToBlinkSequence(blinkSequence, hellSpawnEnemySprite);
        _addBlinkToBlinkSequence(blinkSequence, hellSpawnEnemySprite);
        _addBlinkToBlinkSequence(blinkSequence, hellSpawnEnemySprite);
        blinkSequence.AppendCallback(RunRecycle);
    }

    private void RunRecycle()
    {
        gameObject.SendMessage("RecycleGameObject", SendMessageOptions.DontRequireReceiver);
    }

    public void KillHellSpawnEnemy()
    {
        gameObject.SendMessage("StopFire");
        gameObject.GetComponent<Collider2D>().enabled = false;
        gameObject.GetComponent<Animator>().enabled = false;
        gameObject.transform.DOKill();
        playerScore.AddPoints(_scorePerLife * enemyLivesStart);        
    }

    public void PrepareSpawn()
    {        
        gameObject.GetComponent<Collider2D>().enabled = true;
        gameObject.GetComponent<Animator>().enabled = true;
        hellSpawnEnemySprite = gameObject.GetComponent<SpriteRenderer>();
        enemyLives = Random.Range(enemyLivesMin, enemyLivesMax + 1);
        enemyLivesStart = enemyLives;
        UpdateEnemyHealthBar();
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject collidedObject = collision.gameObject;
        if (collidedObject.tag == Player.PLAYER_TAG) {
            gameObject.SendMessage("RecycleGameObject", SendMessageOptions.DontRequireReceiver);
            collidedObject.SendMessage("AddDeath");            
        }
    }

    private void _addBlinkToBlinkSequence(Sequence blinkSequence, SpriteRenderer playerChildSprite)
    {
        blinkSequence.Append(playerChildSprite.DOFade(0, 0));
        blinkSequence.AppendInterval(_blinkDeadHellSpawnInterval);
        blinkSequence.Append(playerChildSprite.DOFade(1, 0));
        blinkSequence.AppendInterval(_blinkDeadHellSpawnInterval);
    }

    private void UpdateEnemyHealthBar()
    {
        for (int i = 0; i < enemyLivesMax; i++) {
            if (enemyLives > i) {
                enemyHealthBarLives[i].SetActive(true);
            } else {
                enemyHealthBarLives[i].SetActive(false);
            }
        }
        if (enemyLives <= 0) {
            enemyHealthBar.SetActive(false);
        } else {
            enemyHealthBar.SetActive(true);
        }
    }    
}
