using Runtime.PlayerLogic.Control;
using Runtime.PlayerLogic;
using UnityEngine;

namespace Runtime.EntryPoints
{
	public class ExpirienceSceneMainEntryPoint : MonoBehaviour
	{
		[SerializeField] private Player _player;
		[SerializeField] private PlayerController _playerController;
		[SerializeField] private PlayerSettingsConfig _playerSettingsConfig;

		private InputManager _inputManager = new InputManager();

		private void Start()
		{
			Init();
		}

		private void Init()
		{
			_player.Init(_playerSettingsConfig);
			_inputManager.Init();

			_playerController.Init(_player, _inputManager);
		}
	}
}
