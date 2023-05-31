using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
	////////////////////////////////
	#region Attributes

	public PlayerController playerController;

	public GameObject sliderKickJumpGameObject;

	public GameObject buttonJumpGameObject;

	public Image buttonJumpFadeImage;

	public float sliderFadeSpeed = 0.1f;

	public float buttonJumpAlpha = 0.8f;

	#endregion

	////////////////////////////////
	#region Members

	private Image[] sliderKickJumpRenderers;

	private Image[] buttonJumpRenderers;

	#endregion

	////////////////////////////////
	#region PlayerUI

	private void ToggleJumpSlider( bool visible, bool instant )
	{
		foreach( Image renderer in buttonJumpRenderers )
		{
			renderer.CrossFadeAlpha( visible ? 0f : 1f, instant ? 0f : sliderFadeSpeed, true );
		}

		foreach( Image renderer in sliderKickJumpRenderers )
		{
			renderer.CrossFadeAlpha( visible ? 1f : 0f, instant ? 0f : sliderFadeSpeed, true );
		}
	}

	#endregion

	////////////////////////////////
	#region MonoBehaviour

	private void Awake()
	{
		sliderKickJumpRenderers = sliderKickJumpGameObject.GetComponentsInChildren<Image>();

		buttonJumpRenderers = buttonJumpGameObject.GetComponentsInChildren<Image>();
	}

	private void OnEnable()
	{
		ToggleJumpSlider( false, true );

		buttonJumpFadeImage.CrossFadeAlpha( buttonJumpAlpha, 0f, true );
	}

	public void OnKick()
	{
		if( playerController.Jumping )
		{
			playerController.TriggerKick();
		}
	}

	public void OnJumpDown()
	{
		playerController.Jumping = true;

		ToggleJumpSlider( true, false );
	}

	public void OnJumpUp()
	{
		playerController.Jumping = false;

		ToggleJumpSlider( false, false );
		
		buttonJumpFadeImage.CrossFadeAlpha( buttonJumpAlpha, sliderFadeSpeed, true );
	}

	#endregion
}
