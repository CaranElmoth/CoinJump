using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(BoxCollider2D))]
public class CoinBehaviour : MonoBehaviour
{
	private Animator animator = null;
	private BoxCollider2D boxCollider = null;
	private AudioSource audioSource = null;

	private void Awake()
	{
		animator = GetComponent<Animator>();
		boxCollider = GetComponent<BoxCollider2D>();
		audioSource = GetComponent<AudioSource>();
	}

	private void Start()
	{
		GameManager.Instance.RegisterCoin(this);
		animator.Play("coin_float", -1, Random.value);
	}

	public void Take()
	{
		animator.SetTrigger("Taken");
		GameManager.Instance.CoinTaken(this);
		boxCollider.enabled = false;

		if (audioSource)
			audioSource.Play();
	}
}
