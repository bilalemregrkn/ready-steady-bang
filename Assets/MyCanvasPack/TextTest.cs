using TMPro;
using UnityEngine;

namespace MyCanvasPack
{
	public class TextTest : MonoBehaviour
	{
		[SerializeField] private TextMeshProUGUI text;
		[SerializeField] private float value;

		[ContextMenu(nameof(Populate))]
		private void Populate()
		{
			text.SetText(value.ToString("0.000"));
		}
	}
}