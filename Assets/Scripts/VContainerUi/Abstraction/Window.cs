using VContainerUi.Interfaces;
using VContainerUi.Model;

namespace VContainerUi.Abstraction
{
	public abstract class Window : IWindow
	{
		public abstract string Name { get; }
		public abstract void SetState(UiWindowState state);
		public abstract void Back();
		public abstract IUiElement[] GetUiElements();
	}
}