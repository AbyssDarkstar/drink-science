using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.ScriptableObjects
{
    [CreateAssetMenu(menuName = "ScriptableObjects/ObjectiveList")]
    public class ObjectiveListSO : ScriptableObject
    {
        public List<ObjectiveSO> ObjectiveList;
    }
}