using UnityEngine;
using System;
using UniRx;

namespace Runtime.PlayerLogic.Control
{
	public class PlayerController : MonoBehaviour, IDisposable
	{
		[SerializeField] private PlayerMovementController _playerMovementController;

		[SerializeField] private Player _player;

		private readonly CompositeDisposable _disposable = new();

		public void Init(InputManager inputManager, float moveSpeed, float jumpForce)
		{
			_playerMovementController.Init(_player, moveSpeed, jumpForce);

			inputManager.OnHorizontalMoving.Subscribe(input => _playerMovementController.HorizontalMove(input)).AddTo(_disposable);
			inputManager.OnVerticalMoving.Subscribe(input => _playerMovementController.VerticalMove(input)).AddTo(_disposable);
			inputManager.OnJumping.Subscribe(input => _playerMovementController.Jump()).AddTo(_disposable);
		}

		public void Dispose()
		{
			_disposable.Dispose();
		}
	}
}
