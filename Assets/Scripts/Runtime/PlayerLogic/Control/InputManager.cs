using UnityEngine;
using System;
using UniRx;

namespace Runtime.PlayerLogic.Control
{
	public class InputManager : IDisposable
	{
		private readonly ISubject<float> _onHorizontalMoving = new Subject<float>();
		private readonly ISubject<float> _onVerticalMoving = new Subject<float>();
		private readonly ISubject<Unit> _onJumping = new Subject<Unit>();
		private readonly CompositeDisposable _disposable = new();

		public IObservable<float> OnHorizontalMoving => _onHorizontalMoving; 
		public IObservable<float> OnVerticalMoving => _onVerticalMoving;
		public IObservable<Unit> OnJumping => _onJumping;

		public void Init()
		{
			GetInput();
		}

		private void GetInput()
		{
			Observable.EveryUpdate().Select(_ => (GetHorizontalInput(), GetVerticalInput(), GetJumpingInput()))
									.Where(input => input.Item1 != 0 || input.Item2 != 0 || input.Item3 != 0)
									.Subscribe(input =>
									{
										HandleInput(input.Item1, input.Item2, input.Item3);
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

		private float GetJumpingInput()
		{
			if (Input.GetKey(KeyCode.Space))
			{
				return 1f;
			}

			return 0f;
		}

		private void HandleInput(float horizontalInput, float verticalInput, float jumpInput)
		{
			if (horizontalInput != 0)
			{
				_onHorizontalMoving.OnNext(horizontalInput);
			}

			if (verticalInput != 0)
			{
				_onVerticalMoving.OnNext(verticalInput);
			}

			if (jumpInput != 0)
			{
				_onJumping.OnNext(Unit.Default);
			}
		}

		public void Dispose()
		{
			_disposable?.Dispose();
		}
	}
}
