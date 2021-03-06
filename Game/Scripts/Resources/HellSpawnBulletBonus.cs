﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HellSpawnBulletBonus : MonoBehaviour {
    public PlayerGun playerGun;
    public SFX sfx;

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
            sfx.PlaySfxPowerup();
            playerGun.SpeedUpBonus();
            gameObject.SendMessage("RecycleGameObject", SendMessageOptions.DontRequireReceiver);
        }
    }
}
