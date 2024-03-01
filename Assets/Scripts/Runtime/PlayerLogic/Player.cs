using UnityEngine;

namespace Runtime.PlayerLogic
{
	public class Player : MonoBehaviour
	{
		private PlayerSettingsConfig _settings;

		public PlayerSettingsConfig Settings { get => _settings; }

		public void Init(PlayerSettingsConfig settings)
		{
			_settings = settings;
		}
	}
}
