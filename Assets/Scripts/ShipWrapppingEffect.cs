using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipWrapppingEffect : MonoBehaviour
{
	[SerializeField]
	private float padding = 0.1f;

	protected bool isOffscreen;
	protected Vector3 viewportPos;

	private Camera mainCam;
	private float top;
	private float bottom;
	private float left;
	private float right;


	private void Awake()
	{
		mainCam = Camera.main;
	}
	private void Start()
	{
		top = 0.0f - padding;
		bottom = 1.0f + padding;
		left = 0.0f - padding;
		right = 1.0f + padding;
	}

	private void Update()
	{
		WrapScreen();
		
		


	}

	private void WrapScreen()
	{
		// convert transform position to viewport position.
		viewportPos = mainCam.WorldToViewportPoint(transform.position);
		isOffscreen = false;

		// check x
		if (viewportPos.x < left)
		{
			viewportPos.x = right;
			isOffscreen = true;
		}
		else if (viewportPos.x > right)
		{
			viewportPos.x = left;
			isOffscreen = true;
		}

		// check y
		if (viewportPos.y < top)
		{
			viewportPos.y = bottom;
			isOffscreen = true;
		}
		else if (viewportPos.y > bottom)
		{
			viewportPos.y = top;
			isOffscreen = true;
		}

		// if IsOffscreen, convert viewport pos back to world pos and apply to transform.
		if (isOffscreen)
		{
			transform.position = mainCam.ViewportToWorldPoint(viewportPos);
		}
	}
}
