using App.Helpers;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	[SerializeField] private SpriteRenderer person;
	[SerializeField] private SpriteRenderer rip;
	[SerializeField] private SpriteRenderer blood;
	[SerializeField] private Transform bloodParent;
	[SerializeField] private ColorFunctions colorFunctions;

	public int Score { get; set; }

	public void SetActiveSprite(bool isRip)
	{
		//init
		person.color = person.color.With(a: 1);
		rip.color = rip.color.With(a: 0);
		blood.color = blood.color.With(a: isRip ? 1 : 0);

		//SetRotate Blood
		var rotation = bloodParent.transform.localRotation;
		rotation.eulerAngles = Vector3.forward * Random.Range(-20, 20);
		bloodParent.transform.localRotation = rotation;

		if (!isRip) return;
		colorFunctions.ColorTransition(person, person.color.With(a: 0), delay: 1);
		colorFunctions.ColorTransition(blood, blood.color.With(a: 0), delay: 1);
		colorFunctions.ColorTransition(rip, rip.color.With(a: 1), delay: 1);
	}


	[ContextMenu(nameof(TEST_WIN))]
	public void TEST_WIN()
	{
		Score = 4;
	}
}