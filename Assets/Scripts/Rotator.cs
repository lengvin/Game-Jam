using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    public float rotTime;
	public KeyCode rotLeftKey;
	public KeyCode rotRightKey;
	
	private Quaternion m_startRot;
	private float m_endRot;
	private float m_rotTimer;
	
	private void Update()
	{
		m_rotTimer += Time.deltaTime;
		transform.localRotation = Quaternion.Slerp(m_startRot, Quaternion.Euler(0, 0, m_endRot), Mathf.Clamp(m_rotTimer / rotTime, 0, 1));
		
		if(Input.GetKeyDown(rotLeftKey))
			Rot90Left();
		else if(Input.GetKeyDown(rotRightKey))
			Rot90Right();
	}
	
	public void Rot90Right()
	{
		m_startRot = transform.rotation;
		m_endRot += 90;
		m_rotTimer = 0;
	}
	
	public void Rot90Left()
	{
		m_startRot = transform.rotation;
		m_endRot -= 90;
		m_rotTimer = 0;
	}
}
