using System.Collections;
using UnityEngine;

namespace CM.UI.Notched
{
	public class FitParentCanvas : MonoBehaviour
	{
		void Start()
		{
			Fit();
		}

		void OnEnable()
		{
			//StartCoroutine(DelayedFit());
		}

		IEnumerator DelayedFit()
		{
			yield return null;
			Fit();
		}

		[ContextMenu("fit")]
		void Fit()
		{
			var t = GetComponent<RectTransform>();
			var parent = t.parent as RectTransform;
			var canvas = GetComponentInParent<Canvas>();
			var canvasT = canvas.transform as RectTransform;

			var anchorMin = t.anchorMin;
			var anchorMax = t.anchorMax;
			var offsetMin = t.offsetMin;
			var offsetMax = t.offsetMax;

			t.SetParent(canvasT);
			t.anchorMin = anchorMin;
			t.anchorMax = anchorMax;
			t.offsetMin = offsetMin;
			t.offsetMax = offsetMax;
			t.SetParent(parent);
		}
	}
}