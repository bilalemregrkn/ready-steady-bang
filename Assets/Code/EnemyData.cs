using UnityEngine;

[CreateAssetMenu(menuName = "Create EnemyData", fileName = "EnemyData", order = 0)]
public class EnemyData : ScriptableObject
{
	public string nickName;
	public Sprite sprite;
	public Vector2 rangeTime;
}