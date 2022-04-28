using TMPro;
using UnityEngine;

namespace MyCanvasPack
{
	public class PanelWin : MonoBehaviour
	{
		[SerializeField] private TextMeshProUGUI textAverage;

		public void Populate(float average)
		{
			gameObject.SetActive(true);
			textAverage.SetText(average.ToString("0.000"));
		}

		public void Close()
		{
			gameObject.SetActive(false);
		}

		public void OnClick_Yes()
		{
			LevelManager.Instance.Level++;
			CanvasManager.Instance.Open(CanvasType.Level);
		}

		public void OnClick_No()
		{
			CanvasManager.Instance.Open(CanvasType.Home);
		}
	}
}