using System;
using System.Collections.Generic;
using Example.Empty;
using UniRx;
using UnityEngine;
using VContainer.Unity;
using VContainerUi.Interfaces;
using VContainerUi.Messages;
using VContainerUi.Services;

namespace Example
{
	public class ExampleUsage : IInitializable
	{

		private readonly IUiMessagesPublisherService _uiMessagesPublisher;
		private readonly IReadOnlyList<IUiInitializable> _uiInitializables;

		public ExampleUsage(
			IUiMessagesPublisherService uiMessagesPublisher,
			IReadOnlyList<IUiInitializable> uiInitializables)
		{
			_uiMessagesPublisher = uiMessagesPublisher;
			_uiInitializables = uiInitializables;
		}
		
		public void Initialize()
		{
			foreach (var uiInitializable in _uiInitializables)
			{
				uiInitializable.Initialize();
			}
			Observable.Timer(TimeSpan.FromSeconds(3)).Subscribe(_ =>
			{
				_uiMessagesPublisher.OpenWindowPublisher.OpenWindow<EmptyWindow>();
			});
			
			Observable.Timer(TimeSpan.FromSeconds(6)).Subscribe(_ =>
			{
				_uiMessagesPublisher.OpenWindowPublisher.OpenWindow<SecondWindow.SecondWindow>();
			});
		}
	}
}