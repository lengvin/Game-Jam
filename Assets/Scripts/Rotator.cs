using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    public float rotPerSec;
	public KeyCode rotLeftKey;
	public KeyCode rotRightKey;
	
	
	private void Update()
	{
		if(Input.GetKey(rotLeftKey))
			transform.localRotation = transform.localRotation * Quaternion.Euler(0, 0, -rotPerSec * Time.deltaTime);
		else if(Input.GetKey(rotRightKey))
			transform.localRotation = transform.localRotation * Quaternion.Euler(0, 0, rotPerSec * Time.deltaTime);
	}
}
