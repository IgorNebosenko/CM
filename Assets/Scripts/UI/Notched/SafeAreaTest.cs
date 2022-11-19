using UnityEngine;
using System;

namespace CM.UI.Notched
{
	public class SafeAreaTest : MonoBehaviour
	{
		[SerializeField] KeyCode KeySafeArea = KeyCode.A;
		[SerializeField] SafeArea safeArea = null;
		SafeArea.SimDevice[] Sims;
		int SimIdx;

		void Awake()
		{
			if (!Application.isEditor)
				Destroy(this);

			Sims = (SafeArea.SimDevice[])Enum.GetValues(typeof(SafeArea.SimDevice));
		}

		void Update()
		{
			if (UnityEngine.Input.GetKeyDown(KeySafeArea))
				ToggleSafeArea();
		}

		/// <summary>
		/// Toggle the safe area simulation device.
		/// </summary>
		void ToggleSafeArea()
		{
			SimIdx++;

			if (SimIdx >= Sims.Length)
				SimIdx = 0;

			var simDevice = Sims[SimIdx];
			safeArea?.SetSimDevice(simDevice);
			Debug.Log($"Switched to sim device {simDevice} with debug key '{KeySafeArea}' safeArea={safeArea}");
		}
	}
}