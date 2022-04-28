using TMPro;
using UnityEngine;

namespace MyCanvasPack
{
	public class CanvasLevel : CanvasController
	{
		public override CanvasType MyCanvasType() => CanvasType.Level;

		[SerializeField] private TextMeshProUGUI textLevel;
		[SerializeField] private PersonDisplay personDisplay;

		public override void Open()
		{
			base.Open();
			UpdateTextLevel();
			UpdatePersonDisplay();
		}

		public void OnClick_Play()
		{
			CanvasManager.Instance.Open(CanvasType.Game);
			GameManager.Instance.LevelStart();
		}

		public void OnClick_ChangeLevel(bool next)
		{
			var manager = LevelManager.Instance;
			var newLevel = manager.Level;
			newLevel += next ? 1 : -1;
			manager.Level = Mathf.Clamp(newLevel, 0, manager.MaxLevel-1);
			UpdateTextLevel();
			UpdatePersonDisplay();
		}

		private void UpdateTextLevel()
		{
			var manager = LevelManager.Instance;
			textLevel.SetText($"{manager.Level + 1}/{manager.MaxLevel}");
		}

		private void UpdatePersonDisplay()
		{
			var data = LevelManager.Instance.GetEnemy();
			personDisplay.Populate(data);
		}
	}
}