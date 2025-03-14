using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[AddComponentMenu("UI/Effects/Gradient")]
public class Gradient : BaseMeshEffect
{
	public override void ModifyMesh(VertexHelper helper)
	{
		if (!this.IsActive() || helper.currentVertCount == 0)
		{
			return;
		}
		List<UIVertex> list = new List<UIVertex>();
		helper.GetUIVertexStream(list);
		int count = list.Count;
		global::Gradient.Type gradientType = this.GradientType;
		if (gradientType != global::Gradient.Type.Vertical)
		{
			if (gradientType == global::Gradient.Type.Horizontal)
			{
				float num = list[0].position.x;
				float num2 = list[0].position.x;
				for (int i = count - 1; i >= 1; i--)
				{
					float x = list[i].position.x;
					if (x > num2)
					{
						num2 = x;
					}
					else if (x < num)
					{
						num = x;
					}
				}
				float num3 = 1f / (num2 - num);
				UIVertex vertex = default(UIVertex);
				for (int j = 0; j < helper.currentVertCount; j++)
				{
					helper.PopulateUIVertex(ref vertex, j);
					vertex.color = Color32.Lerp(this.EndColor, this.StartColor, (vertex.position.x - num) * num3 - this.Offset);
					helper.SetUIVertex(vertex, j);
				}
			}
		}
		else
		{
			float num4 = list[0].position.y;
			float num5 = list[0].position.y;
			for (int k = count - 1; k >= 1; k--)
			{
				float y = list[k].position.y;
				if (y > num5)
				{
					num5 = y;
				}
				else if (y < num4)
				{
					num4 = y;
				}
			}
			float num6 = 1f / (num5 - num4);
			UIVertex vertex2 = default(UIVertex);
			for (int l = 0; l < helper.currentVertCount; l++)
			{
				helper.PopulateUIVertex(ref vertex2, l);
				vertex2.color = Color32.Lerp(this.EndColor, this.StartColor, (vertex2.position.y - num4) * num6 - this.Offset);
				helper.SetUIVertex(vertex2, l);
			}
		}
	}

	[SerializeField]
	public global::Gradient.Type GradientType;

	[SerializeField]
	[Range(-1.5f, 1.5f)]
	public float Offset;

	[SerializeField]
	private Color32 StartColor = Color.white;

	[SerializeField]
	private Color32 EndColor = Color.black;

	public enum Type
	{
		Vertical,
		Horizontal
	}
}
