using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(BoxCollider2D))]
public class ExitBehaviour : MonoBehaviour
{
	private BoxCollider2D boxCollider = null;
	private Animator animator = null;

	private void Awake()
	{
		boxCollider = GetComponent<BoxCollider2D>();
		animator = GetComponent<Animator>();

		boxCollider.enabled = false;
	}

	private void Start()
	{
		GameManager.Instance.RegisterExit(this);
	}

	public void Open()
	{
		animator.SetTrigger("Open");
		boxCollider.enabled = true;
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		PlayerInput playerInput = collision.gameObject.GetComponent<PlayerInput>();
		if (playerInput)
		{
			playerInput.WinSequence();
			playerInput.enabled = false;
			GameManager.Instance.Win();
		}
	}
}
