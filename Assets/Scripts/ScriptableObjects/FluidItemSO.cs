using UnityEngine;

namespace Assets.Scripts.ScriptableObjects
{
    [CreateAssetMenu(menuName = "ScriptableObjects/FluidItem")]
    public class FluidItemSO : ScriptableObject
    {
        public string ItemName;

        public Transform Prefab;

        public FluidStats Stats;

        public string ItemDescription;
    }
}