using UnityEngine;
using UnityEngine.Events;

public class CharacterController2D : MonoBehaviour
{
	[SerializeField] private float m_JumpForce = 400f;
	[Range(0, 1)] [SerializeField] private float m_CrouchSpeed = .36f;
	[Range(0, .3f)] [SerializeField] private float m_MovementSmoothing = .05f;
	[SerializeField] private bool m_AirControl = false;
	[SerializeField] private LayerMask m_WhatIsGround;
	[SerializeField] private Transform m_GroundCheck;
	[SerializeField] private Transform m_CeilingCheck;
	[SerializeField] private Collider2D m_CrouchDisableCollider;
	[SerializeField] private LayerMask m_WhatIsClimbable;

	const float k_GroundedRadius = .2f;
	private bool m_Grounded;
	const float k_CeilingRadius = .2f;
	private Rigidbody2D m_Rigidbody2D;
	public bool m_FacingRight = true;
	public bool moving = false;
	private Vector3 m_Velocity = Vector3.zero;
	private float originalGravity;

	public AudioSource Walk;
	public AudioClip[] AudioClips;

	[Header("Events")]
	[Space]

	public UnityEvent OnLandEvent;

	[System.Serializable]
	public class BoolEvent : UnityEvent<bool> { }

	public BoolEvent OnCrouchEvent;
	private bool m_wasCrouching = false;

	private void Awake()
	{
		m_Rigidbody2D = GetComponent<Rigidbody2D>();
		originalGravity = m_Rigidbody2D.gravityScale;

		if (OnLandEvent == null)
			OnLandEvent = new UnityEvent();

		if (OnCrouchEvent == null)
			OnCrouchEvent = new BoolEvent();
	}

	private void FixedUpdate()
	{
		bool wasGrounded = m_Grounded;
		m_Grounded = false;

		Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround);
		for (int i = 0; i < colliders.Length; i++)
		{
			if (colliders[i].gameObject != gameObject)
			{
				m_Grounded = true;
				if (!wasGrounded)
					OnLandEvent.Invoke();
			}
		}
	}


	public void Move(float move, float climb, bool crouch, bool jump, bool isClimbing)
	{

		if (!crouch)
		{
			
			if (Physics2D.OverlapCircle(m_CeilingCheck.position, k_CeilingRadius, m_WhatIsGround))
			{
				crouch = true;
			}
		}

		if (m_Grounded || m_AirControl)
		{

			if (crouch)
			{
				if (!m_wasCrouching)
				{
					m_wasCrouching = true;
					OnCrouchEvent.Invoke(true);
				}

				move *= m_CrouchSpeed;

				if (m_CrouchDisableCollider != null)
					m_CrouchDisableCollider.enabled = false;
			} else
			{
				if (m_CrouchDisableCollider != null)
					m_CrouchDisableCollider.enabled = true;

				if (m_wasCrouching)
				{
					m_wasCrouching = false;
					OnCrouchEvent.Invoke(false);
				}
			}

			Vector3 targetVelocity = new Vector2(move * 10f, m_Rigidbody2D.velocity.y);
			m_Rigidbody2D.velocity = Vector3.SmoothDamp(m_Rigidbody2D.velocity, targetVelocity, ref m_Velocity, m_MovementSmoothing);

			if (m_Rigidbody2D.velocity.x != 0)
            {
				if (m_Grounded)
					if (!Walk.isPlaying)
						Walk.Play();

						moving = true;
            }
			else if (m_Rigidbody2D.velocity.x == 0)
            {
				moving = false;
            }

			if (move > 0 && !m_FacingRight)
			{
				Flip();
			}
			else if (move < 0 && m_FacingRight)
			{
				Flip();
			}
		}
		if (m_Grounded && jump)
		{
			if (Physics2D.OverlapCircle(m_CeilingCheck.position, k_CeilingRadius, m_WhatIsGround))
			{
				jump = false;
				crouch = true;
				OnLandEvent.Invoke();
			}
			else
			{
				Walk.Stop();
				m_Grounded = false;
				m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForce));
			}
		}
		//climb
		RaycastHit2D climbInfo = Physics2D.Raycast(transform.position, Vector2.up, 5, m_WhatIsClimbable);
		if(climbInfo.collider != null && isClimbing)
		{
			m_Rigidbody2D.velocity = new Vector2(m_Rigidbody2D.velocity.x, climb);
		} else
		{
			m_Rigidbody2D.gravityScale = originalGravity;
		}
	}


	public void Flip()
	{
		m_FacingRight = !m_FacingRight;

		transform.Rotate(0f, 180f, 0f);
	}

	public void ChangeMusicSource(string location)
	{
		int arrayPosition = MusicPosition(location);
		Walk.clip = AudioClips[arrayPosition];
		Debug.Log(Walk.clip.name);
	}

	private int MusicPosition(string location)
	{
		switch (location)
		{
			case "ForestInfo": return 0;
			case "MineInfo": return 1;
			case "VillageInfo": return 2;
			default: return 0;
		}
	}
}
