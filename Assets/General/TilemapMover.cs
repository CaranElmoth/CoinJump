using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class TilemapMover : MonoBehaviour
{
	public float MaxSpeed = 1.0f;
	public float JumpVelocity = 2.0f;
	public float GroundCheckRadius = 0.2f;
	public LayerMask GroundMask;
	public Transform GroundCheck = null;
	public Transform ForwardGroundCheck = null;

	public bool OnGround { get; private set; }
	public bool ForwardGround { get; private set; }
	public bool MovingLeft { get; private set; }
	public bool MovingRight { get; private set; }

	private Rigidbody2D body = null;
	private Animator animator = null;
	private Vector3 originalScale = Vector3.one;
	private Vector3 actualScale = Vector3.one;
	private Vector2 velocity = Vector2.zero;
	private bool facingRight = true;

	private void Awake()
	{
		body = GetComponent<Rigidbody2D>();
		animator = GetComponent<Animator>();
		actualScale = originalScale = transform.localScale;
	}

	private void FixedUpdate()
	{
		velocity = body.velocity;

		if(velocity.x > 0.0f)
		{
			MovingLeft = false;
			MovingRight = true;
		}
		else if(velocity.x < 0.0f)
		{
			MovingLeft = true;
			MovingRight = false;
		}
		else
		{
			MovingLeft = false;
			MovingRight = false;
		}

		OnGround = Physics2D.OverlapCircle(GroundCheck.position, GroundCheckRadius, GroundMask);
		ForwardGround = Physics2D.OverlapCircle(ForwardGroundCheck.position, GroundCheckRadius, GroundMask);

		if (moveRightRequest)
		{
			velocity.x = MaxSpeed;
		}
		else if (moveLeftRequest)
		{
			velocity.x = -MaxSpeed;
		}
		else
		{
			velocity.x = 0.0f;
		}

		if ((velocity.x > 0.0f && !facingRight) ||
			(velocity.x < 0.0f && facingRight))
			Flip();

		if (OnGround || forceJump)
		{
			if (jumpRequest)
			{
				jumpRequest = false;
				forceJump = false;
				velocity.y = JumpVelocity;
				OnGround = false;
			}
		}
		else
		{
			jumpRequest = false;
			forceJump = false;
		}

		if (animator)
		{
			animator.SetFloat("Speed", Mathf.Abs(velocity.x));
			animator.SetBool("OnGround", OnGround);
		}

		body.velocity = velocity;
	}

	private void Flip()
	{
		facingRight = !facingRight;
		if (facingRight)
			actualScale.x = originalScale.x;
		else
			actualScale.x = -originalScale.x;
		transform.localScale = actualScale;
	}

	private bool moveLeftRequest = false;
	public void MoveLeft()
	{
		moveLeftRequest = true;
	}

	private bool moveRightRequest = false;
	public void MoveRight()
	{
		moveRightRequest = true;
	}

	public void Stop()
	{
		moveLeftRequest = false;
		moveRightRequest = false;
	}

	private bool jumpRequest = false;
	public void Jump()
	{
		jumpRequest = true;
	}

	private bool forceJump = false;
	public void ForceJump()
	{
		jumpRequest = true;
		forceJump = true;
	}

	public void Hit()
	{
		if(animator)
			animator.SetTrigger("Hit");
	}

	public void Exit()
	{
		if(animator)
			animator.SetTrigger("Exit");
	}

	public void DisableCollisions()
	{
		Collider2D collider = GetComponent<Collider2D>();
		if (collider)
			collider.enabled = false;
	}
}
