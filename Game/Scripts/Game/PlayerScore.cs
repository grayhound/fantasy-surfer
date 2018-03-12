using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class PlayerScore : MonoBehaviour {
    public GameObject _playerScoreCanvasObject;
    
    public UnityEngine.UI.Text _playerScoreTextObject;
    public UnityEngine.UI.Text _playerScoreBonusTextObject;

    public GooglePlayServices _googlePlayerServices; 

    private long _playerScorePoints = 0;

    private int _playerScoreTimeBonusStart = 10;
    private int _playerScoreTimeBonus;
    private int _playerScoreTime = 0;

    private bool _isPaused = false;

    private float _startPlayerScoreTime = 3.0f;
    private float _addTimePointsInterval = 3.0f;

    private int _bonusAmplifier = 1;

    private Sequence _addTimePointsSequence;

    private long maxPossibleDisplayScore = 9999999999;

    private float playerScoreBonus = 1.0f;
    private float playerScoreBonusStep = 0.5f;
    
    private float maxPossibleDisplayScoreBonus = 9999999999.9f;
    private string playerScoreBonusTextTemplate = "x{0}";

    void Start()
    {
    }

    public void StartPlayerScore()
    {
        _playerScorePoints = 0;
        playerScoreBonus = 1.0f;
        _playerScoreTimeBonus = _playerScoreTimeBonusStart;
        ShowPlayerScore();
        UpdatePlayerScore();

        //_addTimePointsSequence = DOTween.Sequence();
        //_addTimePointsSequence.AppendInterval(_startPlayerScoreTime);
        //_addTimePointsSequence.AppendCallback(AddTimePoints);
    }

    public void AddPoints(int addPoints, bool useBonus = false)
    {
        if (useBonus) {            
            addPoints = addPoints * _bonusAmplifier;
        }
        _playerScorePoints += Mathf.RoundToInt(addPoints * playerScoreBonus);
        UpdatePlayerScore();
    }

    public void ScoreLevelUp()
    {
        playerScoreBonus += playerScoreBonusStep;
        UpdatePlayerScore();
    }

    public long GetPoints()
    {
        return _playerScorePoints;
    }
    
    public void AddBonus()
    {
        _bonusAmplifier++;
    }

    public void ResetBonus()
    {
        _bonusAmplifier = 1;
    }

    public void PausePlayerScore()
    {
        _isPaused = true;
        _addTimePointsSequence.Pause();
    }

    public void UnpausePlayerScore()
    {
        _isPaused = false;
        _addTimePointsSequence.Play();
    }

    private void ShowPlayerScore()
    {
        _playerScoreCanvasObject.SetActive(true);
    }

    public void StopPlayerScore()
    {
        _addTimePointsSequence.Kill();
    }

    public void SendPlayerScore()
    {
        _googlePlayerServices.SendHiscore(GetPoints());
    }    

    private void UpdatePlayerScore()
    {
        long scoreTextPoints = _playerScorePoints;
        if (scoreTextPoints >= maxPossibleDisplayScore) {
            scoreTextPoints = maxPossibleDisplayScore;
        }
        float playerScoreBonusText = playerScoreBonus;
        if (playerScoreBonusText >= maxPossibleDisplayScoreBonus) {
            playerScoreBonusText = maxPossibleDisplayScoreBonus;
        }
        _playerScoreTextObject.text = scoreTextPoints.ToString();
        _playerScoreBonusTextObject.text = string.Format(playerScoreBonusTextTemplate, playerScoreBonusText.ToString("F1"));
    }

    private void AddTimePoints()
    {
        _playerScoreTime++;
        int points = _playerScoreTimeBonus;
        AddPoints(points);
        _playerScoreTimeBonus++;

        _addTimePointsSequence = DOTween.Sequence();
        _addTimePointsSequence.AppendInterval(_addTimePointsInterval);
        _addTimePointsSequence.AppendCallback(AddTimePoints);
    }
}
