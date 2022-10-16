using System.Collections.Generic;

namespace VContainerUi.Interfaces
{
	public interface IWindowsController
	{
		Stack<IWindow> Windows { get; }

		void Reset();
	}
}