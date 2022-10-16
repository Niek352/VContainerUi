using UnityEngine;

namespace VContainerUi.Interfaces
{
	public interface IUiView
	{
		bool IsShow { get; }

		void Show();
		void Hide();
		IUiElement[] GetUiElements();
		void SetOrder(int index);
		void SetParent(Transform parent);
		void Destroy();
	}
}