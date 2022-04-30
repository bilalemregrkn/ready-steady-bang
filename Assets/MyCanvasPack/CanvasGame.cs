using UnityEngine;

namespace MyCanvasPack
{
	public class CanvasGame : CanvasController
	{
		public override CanvasType MyCanvasType() => CanvasType.Game;

		[SerializeField] private ScoreDisplay me;
		[SerializeField] private ScoreDisplay him;
		[SerializeField] private Animator scoreTableAnimation;

		[SerializeField] private PanelWin panelWin;
		[SerializeField] private PanelLose panelLose;

		[SerializeField] private GameObject home;
		[SerializeField] private GameObject howTo;
		[SerializeField] private GameObject buttonNext;

		[SerializeField] private GameObject gameplay;

		public override void Open()
		{
			base.Open();
			panelWin.Close();
			panelLose.Close();

			gameplay.SetActive(true);
		}

		public override void Close()
		{
			base.Close();
			gameplay.SetActive(false);
			MusicManager.Instance.SetVolume(true);
		}

		private void UpdateSomeButtons(bool active)
		{
			home.SetActive(active);
			howTo.SetActive(active);
			buttonNext.SetActive(active);
		}


		public void Populate(int player_1_score, float player_1_time, int player_2_score, float player_2_time)
		{
			int maxScore = GameManager.Instance.MaxScore;
			if (player_1_score == maxScore)
			{
				panelWin.Populate(GameManager.Instance.GetAverage());
			}
			else if (player_2_score == maxScore)
			{
				panelLose.Populate();
			}

			var end = player_1_score == maxScore || player_2_score == maxScore;
			UpdateSomeButtons(!end);
			gameplay.SetActive(!end);

			scoreTableAnimation.gameObject.SetActive(true);

			if (scoreTableAnimation.enabled)
				scoreTableAnimation.Play(0);
			else
				scoreTableAnimation.enabled = true;

			me.Populate(player_1_score, player_1_time);
			him.Populate(player_2_score, player_2_time);
		}

		public void CloseScore()
		{
			scoreTableAnimation.gameObject.SetActive(false);
		}

		public void OnClick_Next()
		{
			GameManager.Instance.Play();
		}
	}
}