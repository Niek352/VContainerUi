using System.Collections.Generic;
using VContainer;
using VContainerUi.Interfaces;
using VContainerUi.Model;

namespace VContainerUi.Abstraction
{
	public abstract class UiController<T> : IUiController where T : IUiView
	{
		private readonly Stack<UiControllerState> _states = new Stack<UiControllerState>();
		private readonly UiControllerState _defaultState = new UiControllerState(false, false, 0);

		private UiControllerState _currentState;

		[Inject] protected readonly T View;
		public bool IsActive { get; private set; }
		public bool InFocus { get; private set; }

		public void SetState(UiControllerState state)
		{
			_currentState = state;
			_states.Push(state);
		}

		public void ProcessStateOrder()
		{
			if (!_currentState.IsActive)
				return;
			SetOrder(_currentState.Order);
		}

		public void ProcessState()
		{
			if (IsActive != _currentState.IsActive)
			{
				IsActive = _currentState.IsActive;
				if (_currentState.IsActive)
					Show();
				else
					Hide();
			}

			if (InFocus == _currentState.InFocus)
				return;
			InFocus = _currentState.InFocus;
			OnHasFocus(_currentState.InFocus);
		}

		public void Back()
		{
			if (_states.Count > 0)
				_states.Pop();
			if (_states.Count == 0)
			{
				_currentState = _defaultState;
				return;
			}

			SetState(_states.Pop());
		}

		private void Show()
		{
			View.Show();
			OnShow();
		}

		public virtual void OnShow() { }

		private void Hide()
		{
			View.Hide();
			OnHide();
		}

		public virtual void OnHide() { }

		public virtual void OnHasFocus(bool inFocus) { }

		IUiElement[] IUiController.GetUiElements() => View.GetUiElements();
		private void SetOrder(int index) => View.SetOrder(index);
	}
}