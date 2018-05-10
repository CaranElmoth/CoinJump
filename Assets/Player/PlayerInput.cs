using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TilemapMover))]
public class PlayerInput : MonoBehaviour
{
	private TilemapMover tilemapMover = null;
	private AudioSource audioSource = null;

	private bool canJump = true;

	public AudioClip JumpSound = null;
	public AudioClip HitSound = null;
	public AudioClip ExitSound = null;

	private void Awake()
	{
		tilemapMover = GetComponent<TilemapMover>();
		audioSource = GetComponent<AudioSource>();
	}

	private void Update()
	{
		if (Input.GetAxis("Horizontal") > 0.0f)
		{
			tilemapMover.MoveRight();
		}
		else if (Input.GetAxis("Horizontal") < 0.0f)
		{
			tilemapMover.MoveLeft();
		}
		else
		{
			tilemapMover.Stop();
		}

		if (Input.GetAxis("Vertical") > 0.0f)
		{
			if (canJump)
			{
				canJump = false;
				tilemapMover.Jump();
				PlayJumpSound();
			}
		}
		else
		{
			canJump = true;
		}
	}

	private void PlayJumpSound()
	{
		if (audioSource && JumpSound)
		{
			audioSource.clip = JumpSound;
			audioSource.Play();
		}
	}

	private void PlayHitSound()
	{
		if (audioSource && HitSound)
		{
			audioSource.clip = HitSound;
			audioSource.Play();
		}
	}

	private void PlayExitSound()
	{
		if (audioSource && ExitSound)
		{
			audioSource.clip = ExitSound;
			audioSource.Play();
		}
	}

	public void LoseSequence()
	{
		PlayHitSound();
		tilemapMover.Hit();
		tilemapMover.Stop();
		tilemapMover.ForceJump();
		tilemapMover.DisableCollisions();
	}

	public void WinSequence()
	{
		PlayExitSound();
		tilemapMover.Stop();
		tilemapMover.Exit();
	}
}
