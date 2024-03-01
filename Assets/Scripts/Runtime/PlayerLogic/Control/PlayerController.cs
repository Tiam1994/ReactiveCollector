using UnityEngine;
using System;
using UniRx;

namespace Runtime.PlayerLogic.Control
{
	public class PlayerController : MonoBehaviour, IDisposable
	{
		[SerializeField] private PlayerMovementController _playerMovementController;

		private readonly CompositeDisposable _disposable = new();

		public void Init(Player player, InputManager inputManager)
		{
			_playerMovementController.Init(player);

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
