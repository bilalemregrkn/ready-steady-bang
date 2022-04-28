using TMPro;
using UnityEngine;

namespace MyCanvasPack
{
	public class ScoreDisplay : MonoBehaviour
	{
		[SerializeField] TextMeshProUGUI scoreText;
		[SerializeField] TextMeshProUGUI secondText;
		[SerializeField] FillImageDisplay fillImage;

		public void Populate(int score, float second)
		{
			scoreText.SetText($"{score}");
			fillImage.UpdateDisplay(score);

			secondText.SetText(second <= 0 ? "-" : second.ToString("0.000"));
		}
	}
}