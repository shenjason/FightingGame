using UnityEngine;
using UnityEngine.Events;

public class PlayerMovement : MonoBehaviour
{
	public float JumpHeight, WallJumpForce, WallJumpDisablerTimer, MovementSpeed, SpriteWidth, SpriteHeight, GroundFriction, AirFriction, MaxSpeed, FallGravity, NormalGravity;

	public bool AutoSizing = true;
	public LayerMask ContactLayer;
	private Rigidbody2D rb;

	private Vector2 moveDir;
	private bool Jumping, Moving, onWall, WallJumping, CanWallJump;
	private float tick;
	
	void Awake()
	{
		rb = GetComponent<Rigidbody2D>();
		Jumping = false;
		Moving = false;
		tick = 0;
		if (AutoSizing)
		{
			SpriteWidth = transform.localScale.x;
			SpriteHeight = transform.localScale.y;
		}
	}
	void Update()
	{
		if (Input.GetAxisRaw("Horizontal") != 0)
		{
			Moving = true;
			if (!WallJumping)
			{
				if (Input.GetAxisRaw("Horizontal") > 0)
					{
						Flip(1);
					}
					else
					{
						Flip(-1);
					}
			}
		}
		else
		{
			Moving = false;
		}
		moveDir.x = Input.GetAxisRaw("Horizontal");
		if (Input.GetButton("Jump"))
		{
			if (OnGround())
			{
				Jumping = true;
			}
		}
		onWall = OnWall(transform.localScale.x);
		if (onWall && !OnGround())
		{
			CanWallJump = true;
		}
		else
		{
			CanWallJump = false;
		}

		if (WallJumping == true & tick > WallJumpDisablerTimer)
		{
			WallJumping = false;
		}
		tick += Time.deltaTime;

	}

	void FixedUpdate()
	{
		HorizontalMovement();
		ApplyDrag();
		JumpAction();
		WallJump(transform.localScale.x);
	}

	bool OnGround()
	{
		RaycastHit2D hit;	
		hit = Physics2D.BoxCast(transform.position, new Vector2(SpriteWidth - 0.2f, 0.1f), 0, transform.up * -1, SpriteHeight/2, ContactLayer);
		return hit;
	}

	void JumpAction()
	{
		if (Jumping)
		{
			rb.velocity = new Vector2(rb.velocity.x, JumpHeight);
			Jumping = false;
		}
		if (rb.velocity.y < -0.3f)
		{
			rb.gravityScale = FallGravity;
		}
		else
		{
			rb.gravityScale = NormalGravity;
		}
	}

	void ApplyDrag()
	{
		if (OnGround())
		{
			if (!Jumping)
				rb.velocity = new Vector2(rb.velocity.x * GroundFriction, 0);
			WallJumping = false;
		}
		else
		{
			rb.velocity = new Vector2(rb.velocity.x * AirFriction, rb.velocity.y);
		}
	}

	void HorizontalMovement()
	{
		if (!WallJumping)
		{
			if (Moving & !OnWall(transform.localScale.x))
			{
				rb.velocity = new Vector2(rb.velocity.x + (moveDir.x * MovementSpeed), rb.velocity.y);
			}
			if (Mathf.Abs(rb.velocity.x) > MaxSpeed)
			{
				rb.velocity = new Vector2(Mathf.Sign(rb.velocity.x) * MaxSpeed, rb.velocity.y);
			}
		}
	}

	bool OnWall(float dir)
	{
		if (Input.GetAxisRaw("Horizontal") == 0) return false;
		int dirnormal = (int)Mathf.Sign(dir);
		RaycastHit2D hit;	
		hit = Physics2D.Raycast(transform.position, transform.right * dirnormal, SpriteWidth/2 + 0.01f, ContactLayer);
		return hit;
	}

	void Flip(int dir)
	{
		transform.localScale = new Vector3(dir * Mathf.Abs(transform.localScale.x), transform.localScale.y, 1);
	}
	
	void WallJump(float dir)
	{
		if (CanWallJump && Input.GetButton("Jump"))
		{
		int dirnormal = (int)Mathf.Sign(dir);
		rb.velocity = new Vector2(-dirnormal * WallJumpForce, WallJumpForce);
		Flip((int)Mathf.Sign(-transform.localScale.x));
		WallJumping = true;
		tick = 0;
		}
	}
	
}