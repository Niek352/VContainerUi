using System;
using VContainerUi.Interfaces;

namespace VContainerUi.Messages
{
	public readonly struct MessageOpenWindow : IUiMessage
	{
		public readonly Type Type;
		public readonly string Name;
		public UiScope UiScope { get; }

		public MessageOpenWindow(Type type, UiScope scope = UiScope.Local)
		{
			Type = type;
			Name = null;
			UiScope = scope;
		}

		public MessageOpenWindow(string name, UiScope scope = UiScope.Local)
		{
			Type = null;
			Name = name;
			UiScope = scope;
		}
	}
}