using TMPro;
using UnityEngine;

namespace MyCanvasPack
{
	public class CanvasLevel : CanvasController
	{
		public override CanvasType MyCanvasType() => CanvasType.Level;

		[SerializeField] private TextMeshProUGUI textLevel;

		public override void Open()
		{
			base.Open();
			UpdateTextLevel();
		}

		public void OnClick_Play()
		{
			CanvasManager.Instance.Open(CanvasType.Game);
			GameManager.Instance.Play();
		}

		public void OnClick_ChangeLevel(bool next)
		{
			GameManager.Instance.Level += next ? 1 : -1;
			UpdateTextLevel();
		}

		private void UpdateTextLevel()
		{
			textLevel.SetText($"{GameManager.Instance.Level + 1}/10");
		}
	}
}