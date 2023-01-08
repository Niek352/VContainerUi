using System;
using System.Collections.Generic;
using System.Linq;
using MessagePipe;
using UniRx;
using UnityEngine;
using VContainer;
using VContainer.Unity;
using VContainerUi.Interfaces;
using VContainerUi.Messages;
using VContainerUi.Model;
using VContainerUi.Services;
using Object = UnityEngine.Object;

namespace VContainerUi
{
	public class WindowsController : IWindowsController, IInitializable, IDisposable
	{
		private readonly IObjectResolver _container;
		private readonly IReadOnlyList<IWindow> _windows;
		
		private readonly WindowState _windowState;
		private readonly IUiMessagesReceiverService _uiMessagesReceiver;
		private readonly IUiMessagesPublisherService _uiMessagesPublisher;
		private readonly Stack<IWindow> _windowsStack = new Stack<IWindow>();
		private readonly CompositeDisposable _disposables = new CompositeDisposable();
		private readonly UiScope _scope;
		private readonly Canvas _canvas;

		private IWindow _window;
		public Stack<IWindow> Windows => _windowsStack;

		public WindowsController(
			IObjectResolver container,
			IReadOnlyList<IWindow> windows, 
			WindowState windowState,
			IUiMessagesReceiverService uiMessagesReceiver,
			IUiMessagesPublisherService uiMessagesPublisher,
			UiScope scope,
			Canvas canvas
			)
		{
			_container = container;
			_windows = windows;
			_windowState = windowState;
			_uiMessagesReceiver = uiMessagesReceiver;
			_uiMessagesPublisher = uiMessagesPublisher;
			_scope = scope;
			_canvas = canvas;
		}

		public void Initialize()
		{
			_uiMessagesReceiver.OpenWindowSubscriber.Subscribe(OnOpen,new UiMessageFilter<MessageOpenWindow>(_scope)).AddTo(_disposables);
			_uiMessagesReceiver.BackWindowSubscriber.Subscribe(_=> OnBack(),new UiMessageFilter<MessageBackWindow>(_scope)).AddTo(_disposables);
			_uiMessagesReceiver.OpenRootWindowSubscriber.Subscribe(OnOpenRootWindow).AddTo(_disposables);
		}

		public void Dispose()
		{
			if (_canvas != null)
				Object.Destroy(_canvas.gameObject);
			_disposables.Dispose();
		}

		private void OnOpen(MessageOpenWindow message)
		{
			IWindow window;
			if (message.Type != null)
				window = _container.Resolve(message.Type) as IWindow;
			else
				window = _windows.First(f => f.Name == message.Name);
			Open(window);
		}

		private void Open(IWindow window)
		{
			var isNextWindowPopUp = window is IPopUp;
			var currentWindow = _windowsStack.Count > 0 ? _windowsStack.Peek() : null;
			if (currentWindow != null)
			{
				var isCurrentWindowPopUp = currentWindow is IPopUp;
				var isCurrentWindowNoneHidden = currentWindow is INoneHidden;
				if (isCurrentWindowPopUp)
				{
					if (!isNextWindowPopUp)
					{
						var openedWindows = GetPreviouslyOpenedWindows();
						var popupsOpened = GetPopupsOpened(openedWindows);
						var last = openedWindows.Last();
						last.SetState(UiWindowState.NotActiveNotFocus);

						foreach (var openedPopup in popupsOpened)
						{
							openedPopup.SetState(UiWindowState.NotActiveNotFocus);
						}
					}
					else
						currentWindow.SetState(isCurrentWindowNoneHidden
							? UiWindowState.IsActiveNotFocus
							: UiWindowState.NotActiveNotFocus);
				}
				else if (isNextWindowPopUp)
					_window?.SetState(UiWindowState.IsActiveNotFocus);
				else
					_window?.SetState(isCurrentWindowNoneHidden
						? UiWindowState.IsActiveNotFocus
						: UiWindowState.NotActiveNotFocus);
			}

			_windowsStack.Push(window);
			_windowState.CurrentWindowName = window.Name;
			window.SetState(UiWindowState.IsActiveAndFocus);
			_uiMessagesPublisher.MessageShowWindowPublisher.Publish(new MessageShowWindow(window));
			ActiveAndFocus(window, isNextWindowPopUp);
		}

		private void OnBack()
		{
			if (_windowsStack.Count == 0)
				return;

			var currentWindow = _windowsStack.Pop();
			currentWindow.Back();
			_uiMessagesPublisher.MessageCloseWindowPublisher.Publish(new MessageCloseWindow(currentWindow));
			OpenPreviousWindows();
		}

		private void OpenPreviousWindows()
		{
			if (_windowsStack.Count == 0)
				return;

			var openedWindows = GetPreviouslyOpenedWindows();
			var popupsOpened = GetPopupsOpened(openedWindows);
			var firstWindow = GetFirstWindow();
			var isFirstPopUp = false;

			var isNoPopups = popupsOpened.Count == 0;
			var isOtherWindow = firstWindow != _window;
			if (isOtherWindow || isNoPopups)
			{
				firstWindow = openedWindows.Last();
				firstWindow.Back();
				_window = firstWindow;
			}

			if (!isNoPopups)
			{
				var window = popupsOpened.Last();
				window.Back();
				firstWindow = window;
				isFirstPopUp = true;

				if (isOtherWindow)
				{
					var nonHiddenPopUps = popupsOpened.Take(popupsOpened.Count - 1);
					foreach (var nonHiddenPopUp in nonHiddenPopUps)
						nonHiddenPopUp.Back();
				}
			}

			_windowState.CurrentWindowName = firstWindow.Name;
			ActiveAndFocus(firstWindow, isFirstPopUp);
		}

		private void ActiveAndFocus(IWindow window, bool isPopUp)
		{
			if (!isPopUp)
				_window = window;

			_uiMessagesPublisher.MessageActiveWindowPublisher.Publish(new MessageActiveWindow(window));
			_uiMessagesPublisher.MessageFocusWindowPublisher.Publish(new MessageFocusWindow(window));
		}

		private List<IWindow> GetPreviouslyOpenedWindows()
		{
			var windows = new List<IWindow>();

			var hasWindow = false;
			foreach (var window in _windowsStack)
			{
				var isPopUp = window is IPopUp;
				if (isPopUp)
				{
					if (hasWindow)
						break;

					windows.Add(window);
					continue;
				}

				if (hasWindow)
					break;
				windows.Add(window);
				hasWindow = true;
			}

			return windows;
		}

		private Stack<IWindow> GetPopupsOpened(List<IWindow> windows)
		{
			var stack = new Stack<IWindow>();

			var hasPopup = false;
			foreach (var window in windows)
			{
				var isPopUp = window is IPopUp;
				if (!isPopUp)
					break;

				if (hasPopup && !(window is INoneHidden))
					continue;

				stack.Push(window);
				hasPopup = true;
			}

			return stack;
		}

		private IWindow GetFirstWindow()
		{
			foreach (var element in _windowsStack)
			{
				if (element is IPopUp)
					continue;
				return element;
			}

			return null;
		}

		private void OnOpenRootWindow(MessageOpenRootWindow obj)
		{
			while (_windowsStack.Count > 1)
			{
				OnBack();
			}
		}

		public void Reset()
		{
			while (_windowsStack.Count > 0)
			{
				OnBack();
			}

			_window = null;
		}
	}
}