using Example.Empty;
using Example.SecondWindow;
using MessagePipe;
using UnityEngine;
using VContainer;
using VContainer.Unity;
using VContainerUi;
using VContainerUi.Interfaces;
using VContainerUi.Model;
using VContainerUi.Services.Impl;

namespace Example
{
	public class ExampleUiLifeCycle : LifetimeScope
	{
		[SerializeField] private EmptyView _emptyView;
		[SerializeField] private SecondView _secondView;
		[SerializeField] private Canvas _canvas;
		
		protected override void Configure(IContainerBuilder builder)
		{
			builder.Register<UiMessagesReceiverService>(Lifetime.Singleton)
				.AsImplementedInterfaces();
			
			builder.Register<UiMessagesPublisherService>(Lifetime.Singleton)
				.AsImplementedInterfaces();
			
			builder.RegisterUiView<EmptyController, EmptyView>(_emptyView, _canvas.transform);
			builder.RegisterUiView<SecondController, SecondView>(_secondView, _canvas.transform);
			
			builder.Register<IWindow, EmptyWindow>(Lifetime.Scoped)
				.AsImplementedInterfaces().AsSelf();
			
			builder.Register<IWindow, SecondWindow.SecondWindow>(Lifetime.Scoped)
				.AsImplementedInterfaces().AsSelf();
			
			builder.Register<WindowState>(Lifetime.Scoped)
				.AsImplementedInterfaces()
				.AsSelf();
			
			var options = builder.RegisterMessagePipe();
			
			builder.RegisterBuildCallback(c 
				=> GlobalMessagePipe.SetProvider(c.AsServiceProvider()));

			builder.RegisterUiSignals(options);

			builder.RegisterEntryPoint<WindowsController>();
			builder.RegisterEntryPoint<ExampleUsage>();
		}
	}
	

}
