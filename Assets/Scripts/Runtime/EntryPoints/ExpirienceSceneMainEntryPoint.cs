using Runtime.PlayerLogic.Control;
using UnityEngine;

namespace Runtime.EntryPoints
{
	public class ExpirienceSceneMainEntryPoint : MonoBehaviour
	{
		[SerializeField] private PlayerController _playerController;

		private InputManager _inputManager;

		private void Start()
		{
			Init();
		}

		private void Init()
		{
			_inputManager = new InputManager();
			_inputManager.Init();

			_playerController.Init(_inputManager, 5, 8);
		}
	}
}
