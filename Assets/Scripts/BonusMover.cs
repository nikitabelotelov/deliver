using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusMover : MonoBehaviour
{
    public float Speed = 0.1f;
    public float exitAfterSeconds = 10f; // how long to exist in the world
	private float targetTime;
	void Start ()
	{
		// set the targetTime to be the current time + exitAfterSeconds seconds
		targetTime = Time.time + exitAfterSeconds;
	}
    // Update is called once per frame
    void Update()
    {
        if (Time.time >= targetTime)
			Destroy (gameObject);
        
        gameObject.transform.Translate(Vector3.right * Speed * Time.deltaTime);
    }

    void OnMouseDown()
    {
        // Destroy the gameObject after clicking on it
        Destroy(gameObject);
    }
}
