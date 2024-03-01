using UnityEngine;

namespace Runtime.PlayerLogic
{
	[CreateAssetMenu (fileName = "PlayerSettingsConfig", menuName = "ReactiveCollector/Configs/PlayerSettingsConfig")]
	public class PlayerSettingsConfig : ScriptableObject
	{
		public float Speed;
		public float JumpForce;
	}
}
