using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerInput))]
public class PlayerCollisions : MonoBehaviour
{
	private PlayerInput playerInput = null;

	private void Awake()
	{
		playerInput = GetComponent<PlayerInput>();
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if(collision.gameObject.GetComponent<EnemyAI>())
		{
			playerInput.LoseSequence();
			playerInput.enabled = false;
			GameManager.Instance.Lose();
		}
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		CoinBehaviour coin = collision.gameObject.GetComponent<CoinBehaviour>();
		if (coin)
			coin.Take();
	}
}
