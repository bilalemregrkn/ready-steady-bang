using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace MyCanvasPack
{
	public class PersonDisplay : MonoBehaviour
	{
		[SerializeField] private Image image;
		[SerializeField] private TextMeshProUGUI nickName;
		[SerializeField] private TextMeshProUGUI speed;

		public void Populate(EnemyData data)
		{
			image.sprite = data.sprite;
			nickName.SetText(data.nickName);

			var average = (data.rangeTime.x + data.rangeTime.y) / 2;
			var msg = $"speed ~ {average:0.000}s";
			speed.SetText(msg);
		}
	}
}