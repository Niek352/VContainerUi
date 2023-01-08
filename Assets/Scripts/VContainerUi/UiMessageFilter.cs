using System;
using MessagePipe;
using VContainerUi.Interfaces;

namespace VContainerUi
{
	public class UiMessageFilter<T> : MessageHandlerFilter<T>
		where T : IUiMessage
	{
		private readonly UiScope _uiScope;

		public UiMessageFilter(UiScope uiScope)
		{
			_uiScope = uiScope;
		}

		public override void Handle(T message, Action<T> next)
		{
			if (message.UiScope == _uiScope)
				next(message);
		}
	}
}