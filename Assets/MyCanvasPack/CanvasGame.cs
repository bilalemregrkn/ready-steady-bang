using UnityEngine;

namespace MyCanvasPack
{
	public class CanvasGame : CanvasController
	{
		public override CanvasType MyCanvasType() => CanvasType.Game;

		[SerializeField] private ScoreDisplay me;
		[SerializeField] private ScoreDisplay him;
		[SerializeField] private Animator scoreTableAnimation;

		public void Populate(int player_1_score, float player_1_time, int player_2_score, float player_2_time)
		{
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
	}
}