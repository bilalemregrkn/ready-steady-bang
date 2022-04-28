using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MyCanvasPack
{
	public class FillImageDisplay : MonoBehaviour
	{
		[SerializeField] private List<Image> images;
		[SerializeField] private Color emptyColor;
		[SerializeField] private Color fillColor;

		public void UpdateDisplay(int score)
		{
			for (int i = 0; i < images.Count; i++)
			{
				var current = images[i];
				var active = i < score;
				current.color = active ? fillColor : emptyColor;
			}
		}
	}
}