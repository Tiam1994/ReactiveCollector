using UnityEngine;
using System;
using UniRx;

namespace Runtime.PlayerLogic.Control
{
	public class InputManager : IDisposable
	{
		private readonly ISubject<float> _onHorizontalMoving = new Subject<float>();
		private readonly ISubject<float> _onVerticalMoving = new Subject<float>();
		private readonly CompositeDisposable _disposable = new();

		public IObservable<float> OnHorizontalMoving => _onHorizontalMoving; 
		public IObservable<float> OnVerticalMoving => _onVerticalMoving;

		public void Init()
		{
			GetInput();
		}

		private void GetInput()
		{
			Observable.EveryUpdate().Select(_ => (GetHorizontalInput(), GetVerticalInput()))
									.Where(input => input.Item1 != 0 || input.Item2 != 0)
									.Subscribe(input =>
									{
										HandleInput(input.Item1, input.Item2);
									}).AddTo(_disposable);
		}

		private float GetHorizontalInput()
		{
			if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
			{
				return -1f;
			}
			else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
			{
				return 1f;
			}
			return 0f;
		}

		private float GetVerticalInput()
		{
			if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
			{
				return 1f;
			}
			else if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
			{
				return -1f;
			}
			return 0f;
		}

		private void HandleInput(float horizontalInput, float verticalInput)
		{
			if (horizontalInput != 0)
			{
				_onHorizontalMoving.OnNext(horizontalInput);
			}

			if (verticalInput != 0)
			{
				_onVerticalMoving.OnNext(verticalInput);
			}
		}

		public void Dispose()
		{
			_disposable?.Dispose();
		}
	}
}
