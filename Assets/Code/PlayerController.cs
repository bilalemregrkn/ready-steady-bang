using UnityEngine;

public class PlayerController : MonoBehaviour
{
	[SerializeField] private SpriteRenderer person;
	[SerializeField] private SpriteRenderer rip;
	
	public int Score { get; set; }

	public void SetActiveSprite(bool isRip)
	{
		person.enabled = !isRip;
		rip.enabled = isRip;
	}
}