using System;
using System.Collections;
using System.Collections.Generic;
using MyCanvasPack;
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

	[SerializeField] private PlayerController player;
	[SerializeField] private PlayerController enemy;

	private PersonType _killPerson;

	private DateTime _bangTime;
	private float _enemyDistance;

	private readonly List<float> _listPlayerShootTime = new List<float>();
	public int MaxScore { get; set; } = 5;

	public float GetAverage()
	{
		var total = 0f;
		foreach (var item in _listPlayerShootTime)
			total += item;

		return total / _listPlayerShootTime.Count;
	}

	private void Awake()
	{
		Instance = this;
	}

	public void LevelStart()
	{
		_killPerson = PersonType.None;
		_listPlayerShootTime.Clear();
		player.Score = 0;
		enemy.Score = 0;

		Play();
	}

	public void Play()
	{
		var canvasGame = (CanvasGame)CanvasManager.Instance.GetCanvasController(CanvasType.Game);
		canvasGame.CloseScore();

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
			var currentEnemy = LevelManager.Instance.GetEnemy();
			_enemyDistance = Random.Range(currentEnemy.rangeTime.x, currentEnemy.rangeTime.y);
			StartCoroutine(CountDown(_enemyDistance));

			Active(State.Bang);
		}

		StartCoroutine(Animation());
	}

	private IEnumerator CountDown(float delay)
	{
		yield return new WaitForSeconds(delay);
		Shoot(PersonType.Enemy);
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
		_listPlayerShootTime.Add((float)playerDistance.TotalSeconds);

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