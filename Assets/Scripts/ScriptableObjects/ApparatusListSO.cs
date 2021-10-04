using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.ScriptableObjects
{
    [CreateAssetMenu(menuName = "ScriptableObjects/ApparatusList")]
    public class ApparatusListSO : ScriptableObject
    {
        public List<ApparatusSO> ApparatusList;
    }
}
