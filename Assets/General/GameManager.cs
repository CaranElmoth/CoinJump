using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
	public static GameManager Instance { get; private set; }

	public Animator WinTextAnimator = null;
	public Animator LoseTextAnimator = null;
	public Animator PressKeyAnimator = null;

	private List<CoinBehaviour> coinBehaviours = new List<CoinBehaviour>();
	private ExitBehaviour exit = null;
	private bool gameEnded = false;

	private void Awake()
	{
		if(Instance != null)
		{
			Debug.LogError("Only one GameManager allowed per scene.");
			return;
		}

		Instance = this;
		coinBehaviours.Clear();
	}

	private void Update()
	{
		if (gameEnded && Input.anyKeyDown)
			SceneManager.LoadScene("main");
	}

	public void RegisterExit(ExitBehaviour exit)
	{
		this.exit = exit;
	}

	public void RegisterCoin(CoinBehaviour coinBehaviour)
	{
		if(!coinBehaviours.Contains(coinBehaviour))
			coinBehaviours.Add(coinBehaviour);
	}

	public void UnregisterCoin(CoinBehaviour coinBehaviour)
	{
		coinBehaviours.Remove(coinBehaviour);
	}

	public void CoinTaken(CoinBehaviour coinBehaviour)
	{
		UnregisterCoin(coinBehaviour);

		if (coinBehaviours.Count == 0 && exit != null)
			exit.Open();
	}

	public void Win()
	{
		StartCoroutine(GameEndCoroutine(true));
	}

	public void Lose()
	{
		StartCoroutine(GameEndCoroutine(false));
	}

	private IEnumerator GameEndCoroutine(bool win)
	{
		if(win)
		{
			WinTextAnimator.SetTrigger("Enter");
		}
		else
		{
			LoseTextAnimator.SetTrigger("Enter");
		}
		PressKeyAnimator.SetTrigger("Enter");

		yield return new WaitForSeconds(0.5f);

		gameEnded = true;
	}
}
