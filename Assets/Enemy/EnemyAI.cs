using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TilemapMover))]
public class EnemyAI : MonoBehaviour
{
	private TilemapMover tilemapMover = null;
	private bool goRight = true;
	private bool justSwitched = false;

	private void Awake()
	{
		tilemapMover = GetComponent<TilemapMover>();
	}

	private void Update()
	{
		if (tilemapMover.OnGround)
		{
			if (goRight)
			{
				tilemapMover.MoveRight();
			}
			else
			{
				tilemapMover.MoveLeft();
			}

			if (goRight && tilemapMover.MovingRight || !goRight && tilemapMover.MovingLeft)
				justSwitched = false;

			if (!tilemapMover.ForwardGround && !justSwitched)
			{
				tilemapMover.Stop();
				goRight = !goRight;
				justSwitched = true;
			}
		}
		else
		{
			tilemapMover.Stop();
		}
	}
}
