using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerController : MonoBehaviour {
    public GameObject player_controller_canvas;
    public PlayerBonusStatus playerBonusStatus;

    private bool is_enabled = false;

    private float move_speed = 300.0f;

    private Rigidbody2D rigid_body;

    private bool move_up = false;
    private bool move_down = false;

    private bool isSpeedUp = false;
    private float speedUpBonusTime = 10.0f;

    private Sequence _stopSpeedBonusSequence;

    void Start ()
    {
        rigid_body = GetComponent<Rigidbody2D>();
	}

    private void FixedUpdate()
    {
        CheckMovement();
    }

    public void MoveUp()
    {        
        move_up = true;
        move_down = false;                
    }

    public void MoveDown()
    {
        move_up = false;
        move_down = true;
    }

    public void MoveStop()
    {
        move_up = false;
        move_down = false;
        rigid_body.velocity = new Vector2(0, 0);
    }

    public void EnableController()
    {
        move_up = false;
        move_down = false;
        is_enabled = true;
        player_controller_canvas.SetActive(true);
        _stopSpeedBonusSequence.Kill();
        StopSpeedBonus();
    }

    public void DisableController()
    {
        is_enabled = false;
        player_controller_canvas.SetActive(false);
        StopSpeedBonus();
        rigid_body.velocity = new Vector3(0, 0, 0);
    }

    public void PauseController()
    {
        _stopSpeedBonusSequence.Pause();
        is_enabled = false;
        rigid_body.velocity = new Vector3(0, 0, 0);        
    }

    public void UnpauseController()
    {
        _stopSpeedBonusSequence.Play();
        is_enabled = true;
    }

    public void StartSpeedBonus()
    {
        StopSpeedBonus();
        isSpeedUp = true;
        _stopSpeedBonusSequence = DOTween.Sequence();
        _stopSpeedBonusSequence.AppendInterval(speedUpBonusTime);
        _stopSpeedBonusSequence.AppendCallback(StopSpeedBonus);
        playerBonusStatus.StartBonusStar(speedUpBonusTime);
    }

    public void StopSpeedBonus()
    {
        isSpeedUp = false;
        _stopSpeedBonusSequence.Kill();
        playerBonusStatus.StopBonusStar();
    }

    private void CheckMovement()
    {
        if (!is_enabled) {
            return;
        }
        float speedBonus = 1;
        if (isSpeedUp) {
            speedBonus = 1.25f;
        }
        if (move_up) {
            rigid_body.velocity = new Vector2(0, move_speed * speedBonus * Time.fixedDeltaTime);
            return;
        }
        if (move_down) {
            rigid_body.velocity = new Vector2(0, -move_speed * speedBonus * Time.fixedDeltaTime);
            return;
        }
    }
    
    public static float DeviceDiagonalSizeInInches ()
    {
        float screenWidth = Screen.width / Screen.dpi;
        float screenHeight = Screen.height / Screen.dpi;
        float diagonalInches = Mathf.Sqrt (Mathf.Pow (screenWidth, 2) + Mathf.Pow (screenHeight, 2));
 
        return diagonalInches;
    }    
}
