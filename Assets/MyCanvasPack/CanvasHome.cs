namespace MyCanvasPack
{
	public class CanvasHome : CanvasController
	{
		public override CanvasType MyCanvasType() => CanvasType.Home;

		public void OnClick_Next()
		{
			CanvasManager.Instance.Open(CanvasType.GameOver);
		}

		public void OnClick_2_Player()
		{
		}

		public void OnClick_1_Player()
		{
			CanvasManager.Instance.Open(CanvasType.Level);
		}

		public void OnClick_Kill_Gallery()
		{
		}
	}
}