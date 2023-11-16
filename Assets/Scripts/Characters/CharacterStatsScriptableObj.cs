using UnityEngine;

namespace Assets.Scripts.Characters
{
	[CreateAssetMenu(
        fileName = "Character Stats",
        menuName = "RPG/Character Stats Scriptable Obj",
        order = 0
    )]
    public class CharacterStatsScriptableObj : ScriptableObject
    {
        public float Health = 100f;
        public float Damage = 10f;
        public float WalkSpeed = 1f;
        public float RunSpeed = 1.5f;
    }
}
