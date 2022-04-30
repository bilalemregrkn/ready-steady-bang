using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
	public static LevelManager Instance { get; private set; }
	[SerializeField] private List<EnemyData> listEnemyData;
	public int Level { get; set; }

	public int MaxLevel => listEnemyData.Count;

	public EnemyData GetEnemy()
	{
		return listEnemyData[Level];
	}

	private void Awake()
	{
		Instance = this;
	}
}