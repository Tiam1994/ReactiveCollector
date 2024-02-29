using UnityEngine;

namespace Runtime.PlayerLogic.Control
{
	public class PlayerMovementController : MonoBehaviour
	{
		private Player _player;
		private float _moveSpeed;

		private Vector3 _horizontalMovement = Vector3.zero;
		private Vector3 _verticalMovement = Vector3.zero;
		private Vector3 _direction;

		public void Init(Player player, float moveSpeed)
		{
			_player = player;
			_moveSpeed = moveSpeed;
		}

		public void HorizontalMove(float horizontalInput)
		{
			_horizontalMovement.Set(horizontalInput * _moveSpeed * Time.deltaTime, 0f, 0f);
			_player.transform.Translate(_horizontalMovement, Space.World);
			Rotate(_horizontalMovement);
		}

		public void VerticalMove(float verticalInput)
		{
			_verticalMovement.Set(0f, 0f, verticalInput * _moveSpeed * Time.deltaTime);
			_player.transform.Translate(_verticalMovement, Space.World);
			Rotate(_verticalMovement);
		}

		public void Rotate(Vector3 movement)
		{
			if(_direction == movement.normalized)
			{
				return;
			}

			_direction = movement.normalized;
			_direction.y = 0f;
			_player.transform.LookAt(_player.transform.position + _direction);
		}
	}
}
