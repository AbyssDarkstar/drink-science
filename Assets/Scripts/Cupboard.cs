using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.ScriptableObjects;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Random = System.Random;

namespace Assets.Scripts
{
    public class Cupboard : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField]
        private GameObject _cupboardDoors;

        [SerializeField]
        private Transform _cupboardShelves;
    
        [SerializeField]
        private Transform _inventorySlot;
    
        private List<FluidItemSO> _inventoryItems;

        private static Random rng = new Random();

        private void Awake()
        {
        }

        private void Start()
        {
            _inventoryItems = Resources.Load<FluidItemListSO>("FluidItemList").FluidItemList;

            var x = 0;
            var y = 0;
            var itemSlotSize = 70f;

            foreach (var item in _inventoryItems.OrderBy(i => rng.Next()).ToList())
            {
                var slotTemplate = Instantiate(_inventorySlot, _cupboardShelves).GetComponent<RectTransform>();
                slotTemplate.GetChild(0).GetComponent<FluidItem>().SetFluidItemSO(item);
                slotTemplate.gameObject.SetActive(true);
                slotTemplate.anchoredPosition = new Vector2(-105 + x * itemSlotSize, 125 + y * itemSlotSize + 10 * y);
                x++;
                if (x > 3)
                {
                    x = 0;
                    y--;
                }
            }
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            _cupboardDoors.GetComponent<Image>().enabled = false;
        }
    }
}
