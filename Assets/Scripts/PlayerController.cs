using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	////////////////////////////////
	#region Attributes

	public Transform playerTransform;

	public Animator playerAnimator;

	public string playerAnimNameJump = "Jumping";

	public string playerAnimNameKick = "Kick";

	public string playerAnimNameHeight = "Height";
	
	public float jumpHeight = 1f;

	public float jumpAscendRate = 0.5f;

	public float jumpDescendRate = 0.25f;

	#endregion

	////////////////////////////////
	#region Members

	private Vector3 startPosition;

	private float currentHeight;

	private bool jumping;

	private bool awaitingJump;

	private bool canElevate;

	#endregion

	////////////////////////////////
	#region Properties

	// Set by UI
	public bool Jumping
	{
		get => jumping;

		set
		{			
			playerAnimator.SetBool( playerAnimNameJump, value );

			jumping = value;
		}
	}

	#endregion

	////////////////////////////////
	#region PlayerController
	
	// Invoked by UI
	public void TriggerKick()
	{
		playerAnimator.SetTrigger( playerAnimNameKick );
	}

	// Invoked by Animation Events
	public void PlayerJumpedUp()
	{
		canElevate = true;
	}

	// Invoked by Animation Events
	public void PlayerJumpingDown()
	{
		canElevate = false;
	}

	#endregion

	////////////////////////////////
	#region MonoBehaviour

	private void Awake()
	{
		startPosition = playerTransform.localPosition;
	}

	private void Update()
	{
		if( jumping )
		{
			if( canElevate )
			{
				currentHeight = Mathf.Lerp( currentHeight, jumpHeight, ( jumpHeight / jumpAscendRate ) * Time.deltaTime );
			}
		}
		else if( currentHeight > 0f )
		{
			currentHeight = Mathf.MoveTowards( currentHeight, 0f, ( jumpHeight / jumpDescendRate ) * Time.deltaTime );

			if( currentHeight == 0f )
			{
				playerAnimator.ResetTrigger( playerAnimNameKick );
			}
		}

		playerTransform.localPosition = startPosition + new Vector3( 0f, currentHeight, 0f );

		playerAnimator.SetFloat( playerAnimNameHeight, currentHeight );
	}

	#endregion
}
