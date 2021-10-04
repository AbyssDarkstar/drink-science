using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.ScriptableObjects
{
    [CreateAssetMenu(menuName = "ScriptableObjects/FluidItemList")]
    public class FluidItemListSO : ScriptableObject
    {
        public List<FluidItemSO> FluidItemList;
    }
}
