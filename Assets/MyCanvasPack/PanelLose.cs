using UnityEngine;

namespace MyCanvasPack
{
	public class PanelLose : MonoBehaviour
	{
		public void Populate()
		{
			gameObject.SetActive(true);
		}
		
		public void Close()
		{
			gameObject.SetActive(false);
		}

		public void OnClick_Yes()
		{
			CanvasManager.Instance.Open(CanvasType.Level);
		}

		public void OnClick_No()
		{
			CanvasManager.Instance.Open(CanvasType.Home);
		}
	}
}