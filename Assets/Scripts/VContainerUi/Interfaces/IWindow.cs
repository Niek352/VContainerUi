using VContainerUi.Model;

namespace VContainerUi.Interfaces
{
	public interface IWindow
	{
		string Name { get; }
		void SetState(UiWindowState state);
		void Back();
		IUiElement[] GetUiElements();
	}
}