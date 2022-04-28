using System;
using System.Collections;
using System.Collections.Generic;
using MyCanvasPack;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public enum PersonType
{
	None,
	Player,
	Enemy
}

public class GameManager : MonoBehaviour
{
	public static GameManager Instance { get; private set; }

	[SerializeField] private List<StateDisplay> displays;
	[SerializeField] private float steadyOpenDelay;
	[SerializeField] private Vector2 rangeBangDelay;
	[SerializeField] private Vector2 rangeEnemyShootTime;

	[SerializeField] private PlayerController player;
	[SerializeField] private PlayerController enemy;

	private PersonType _killPerson;

	private DateTime _bangTime;
	private float _enemyDistance;

	public int Level { get; set; }

	private void Awake()
	{
		Instance = this;
	}

	public void Play()
	{
		var canvasGame = (CanvasGame)CanvasManager.Instance.GetCanvasController(CanvasType.Game);
		canvasGame.CloseScore();

		player.gameObject.SetActive(true);
		enemy.gameObject.SetActive(true);

		player.SetActiveSprite(false);
		enemy.SetActiveSprite(false);

		_killPerson = PersonType.None;

		IEnumerator Animation()
		{
			Active(State.Ready);
			yield return new WaitForSeconds(steadyOpenDelay);
			Active(State.Steady);
			var randomSecond = Random.Range(rangeBangDelay.x, rangeBangDelay.y);
			yield return new WaitForSeconds(randomSecond);
			_bangTime = DateTime.Now;
			_enemyDistance = Random.Range(rangeEnemyShootTime.x, rangeEnemyShootTime.y);
			CountDown();

			Active(State.Bang);
		}

		StartCoroutine(Animation());
	}

	private void CountDown()
	{
		IEnumerator Do()
		{
			yield return new WaitForSeconds(_enemyDistance);
			Shoot(PersonType.Enemy);
		}

		StartCoroutine(Do());
	}

	private void Active(State state)
	{
		foreach (var item in displays)
			item.gameObject.SetActive(false);

		var current = GetDisplay(state);
		current.gameObject.SetActive(true);
	}

	private StateDisplay GetDisplay(State state)
	{
		return displays.Find(x => x.myState == state);
	}

	public void Shoot(PersonType type)
	{
		if (_killPerson != PersonType.None) return;
		_killPerson = type == PersonType.Player ? PersonType.Enemy : PersonType.Player;

		if (_killPerson == PersonType.Enemy)
			player.Score++;
		else
			enemy.Score++;

		OnKill();
	}

	private void OnKill()
	{
		var current = GetDisplay(State.Bang);
		current.gameObject.SetActive(false);

		player.SetActiveSprite(_killPerson == PersonType.Player);
		enemy.SetActiveSprite(_killPerson == PersonType.Enemy);

		//Display
		var playerDistance = _killPerson == PersonType.Enemy ? DateTime.Now - _bangTime : TimeSpan.Zero;

		IEnumerator Do()
		{
			yield return new WaitForSeconds(1);


			var canvasGame = (CanvasGame)CanvasManager.Instance.GetCanvasController(CanvasType.Game);
			canvasGame.Populate(player.Score, (float)playerDistance.TotalSeconds,
				enemy.Score, _enemyDistance);
		}

		StartCoroutine(Do());
	}
}