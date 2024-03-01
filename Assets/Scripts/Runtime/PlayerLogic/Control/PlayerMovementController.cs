using UnityEngine;

namespace Runtime.PlayerLogic.Control
{
	public class PlayerMovementController : MonoBehaviour
	{
		private Player _player;
		private float _moveSpeed;
		private float _jumpForce;

		private Vector3 _horizontalMovement = Vector3.zero;
		private Vector3 _verticalMovement = Vector3.zero;
		private Vector3 _direction;

		public void Init(Player player)
		{
			_player = player;
			_moveSpeed = _player.Settings.Speed;
			_jumpForce = _player.Settings.JumpForce;
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

		public void Jump()
		{
			if (IsPlayerGrounded())
			{
				_player.GetComponent<Rigidbody>().AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);
			}
		}

		private bool IsPlayerGrounded()
		{
			return Physics.Raycast(_player.transform.position, Vector3.down, 0.1f);
		}
	}
}
