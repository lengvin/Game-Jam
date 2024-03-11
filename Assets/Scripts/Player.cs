using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("General")]
	public Rigidbody2D rb;
	public Animator anim;
	
	[Header("Movement")]
	public float speed;
	public Vector2 accelTime;
	public float rotPerSec;
	
	[Header("Jump")]
	public KeyCode jumpKey;
	public float jumpForce;
	public float jumpDelay;
	public LayerMask groundLayer;
	
	private bool m_grounded;
	private bool m_closeToGround;
	private float m_closeToGroundDist;
	private float m_jumpDelayTimer;
	
	private float m_playerHeight;
	private float m_startXScale;
	
	private float m_currSpeed;
	private float m_desSpeed;
	private float m_lastDesSpeed;
	private float m_prevDesSpeed;
	private float m_accelTime;
	private float m_accelTimer;
	
	private Vector2 m_velocity;
	private Vector2 m_fallVel;
	
	
	
	private void Start()
	{
		m_playerHeight = GetComponent<BoxCollider2D>().bounds.size.y;
		m_closeToGroundDist = GetComponent<BoxCollider2D>().bounds.size.magnitude + 0.1f;
		m_startXScale = transform.localScale.x;
	}
	
	private void Update()
	{
		
		SmoothSpeed();
		GroundCheck();
		RotateSprite();
		Jump();
		Move();
		Animation();
	}
	
	
	// Speed
	
	private void SmoothSpeed()
	{
		m_desSpeed = Input.GetAxisRaw("Horizontal") * speed;
		
		if(m_desSpeed != m_lastDesSpeed)
		{
			m_accelTime = (Mathf.Sign(m_currSpeed) == Mathf.Sign(m_desSpeed)) ? accelTime.y : accelTime.x
			- ((m_currSpeed - m_lastDesSpeed) * m_accelTime) / (m_desSpeed - m_lastDesSpeed);
			m_prevDesSpeed = m_currSpeed;
			m_accelTimer = 0;
		}
		
		m_lastDesSpeed = m_desSpeed;
		
		if(m_accelTime != 0)
		{
			m_currSpeed = Mathf.Lerp(m_prevDesSpeed, m_desSpeed, 
			Mathf.Clamp(m_accelTimer / Mathf.Abs(m_accelTime), Mathf.NegativeInfinity, 1));
			
			m_accelTimer += Time.deltaTime;
		}
	}
	
	
	// Animation
	
	private void RotateSprite()
	{
		if(Mathf.Abs(rb.velocity.x) < 0.1)
			return;
		
		if(rb.velocity.x < 0)
			transform.localScale = new Vector3(-m_startXScale, transform.localScale.y, transform.localScale.z);
		else
			transform.localScale = new Vector3(m_startXScale, transform.localScale.y, transform.localScale.z);
	}
	
	private void Animation()
	{
		anim.SetBool("walk", Mathf.Abs(m_currSpeed) > 0.1);
		
		anim.SetBool("grounded", m_grounded);
	}

	
	// Jump
	
	private void Jump()
	{
		m_jumpDelayTimer -= Time.deltaTime;
		m_velocity = new Vector2(m_velocity.x, RotVec2(rb.velocity, -transform.localRotation.eulerAngles.z).y);
		
		if(Input.GetKeyDown(jumpKey) && m_jumpDelayTimer < 0 && m_grounded)
		{
			m_velocity = new Vector2(m_velocity.x, jumpForce);
			m_jumpDelayTimer = jumpDelay;
		}
	}
	
	private void GroundCheck()
	{
		m_grounded = Physics2D.Raycast(transform.position, -transform.up, m_playerHeight + 0.1f, groundLayer);
		
		RaycastHit2D hit = Physics2D.CircleCast(transform.position, m_closeToGroundDist, Vector2.up, 0, groundLayer);
		m_closeToGround = hit.collider != null;
		
		if(m_closeToGround && Vector2.Angle(hit.normal, Vector2.up) < 45)
			transform.localRotation = Quaternion.Slerp(transform.localRotation, Quaternion.Euler(0, 0, -Mathf.Rad2Deg * Mathf.Atan2(hit.normal.x, hit.normal.y)), Mathf.Clamp(Time.deltaTime * 10, 0, 1));
	}
	
	
	// Movement
	
	private void Move()
	{
		m_velocity = new Vector2(m_currSpeed, m_velocity.y);
		
		rb.velocity = RotVec2(m_velocity, transform.localRotation.eulerAngles.z);
		//rb.AddForce(-Vector2.up * 9.81f);
	}
	
	
	// Other
	
	private Vector3 MultVec3(Vector3 vec1, Vector3 vec2)
	{
		return new Vector3(vec1.x * vec2.x, vec1.y * vec2.y, vec1.z * vec2.z);
	}
	
	private Vector2 MultVec2(Vector2 vec1, Vector2 vec2)
	{
		return new Vector2(vec1.x * vec2.x, vec1.y * vec2.y);
	}
	
	private Vector2 RotVec2(Vector2 vec, float angleDeg)
	{
		float theta = Mathf.Deg2Rad * angleDeg;
		
		float cs = Mathf.Cos(theta);
		float sn = Mathf.Sin(theta);
		
		return new Vector2(vec.x * cs - vec.y * sn, vec.x * sn + vec.y * cs);
	}
}
