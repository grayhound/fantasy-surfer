using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HellSpawnMissedCheck : MonoBehaviour {
    public GameObject alternativeSpritePosition;

	void Start ()
    {
        SetStartPosition();
	}
	
	void Update ()
    {		
	}

    public void SetStartPosition()
    {
        Vector3 startPosition = gameObject.transform.position;
        startPosition.x = alternativeSpritePosition.transform.position.x;
        gameObject.transform.position = startPosition;
    }
}
