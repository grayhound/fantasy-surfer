using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TestCamera : MonoBehaviour {

	// Use this for initialization
	void Start ()
	{
		TestTest();
		//TestSpawner();
		//TestSequenceSpeedup();
	}
	
	private float prevTime = 0.0f;
	private float _testTimeScale = 1.0f;

	private void TestTest()
	{
		Debug.Log(Time.time);
		Sequence _s = DOTween.Sequence();
		_s.AppendCallback(() => Test2());
		_s.OnStart(() => Test2());
	}

	private void Test2()
	{
		Debug.Log(Time.time);
	}	
		
	private void TestSpawner()
	{
		Sequence _seq = DOTween.Sequence();
		_seq.AppendInterval(5.0f);
		_seq.AppendCallback(TestSequence);
		_seq.SetLoops(-1);
	}
        
	private void TestSequence()
	{
		Sequence _testSeq = DOTween.Sequence();
		_testSeq.timeScale = _testTimeScale;
		_testSeq.AppendInterval(0.25f);
		_testSeq.AppendCallback(TestSequenceCallback);
		_testSeq.SetId("TESTTEST");
		_testSeq.SetLoops(7);
	}    
    
	private void TestSequenceCallback()
	{
		if (prevTime != 0.0f) {
			Debug.Log(Time.time - prevTime);
		}
		prevTime = Time.time;
	}

	private void TestSequenceSpeedup()
	{
		Sequence _testSeq = DOTween.Sequence();
		_testSeq.AppendInterval(3.0f);
		_testSeq.AppendCallback(TestSequenceSpeedupCallback);
		_testSeq.SetLoops(-1);        
	}

	private void TestSequenceSpeedupCallback()
	{
		_testTimeScale += 0.1f;
		List<Tween> _tweens = DOTween.TweensById("TESTTEST");
		if (_tweens == null) {
			return;
		}
		for (int i = 0; i < _tweens.Count; i++) {
			var _tween = _tweens[i];
			_tween.timeScale = _testTimeScale;
		}
	}	
}
