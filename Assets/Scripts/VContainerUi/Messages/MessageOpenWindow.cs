using System;

namespace VContainerUi.Messages
{
	public readonly struct MessageOpenWindow
	{
		public readonly Type Type;
		public readonly string Name;

		public MessageOpenWindow(Type type)
		{
			Type = type;
			Name = null;
		}

		public MessageOpenWindow(string name)
		{
			Type = null;
			Name = name;
		}
	}
}