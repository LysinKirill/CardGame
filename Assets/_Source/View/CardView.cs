using Core;
using Model;
using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace View
{
    public class CardView : MonoBehaviour, IPointerDownHandler
    {
        [SerializeField] private GameObject cardBack;
        [SerializeField] private GameObject cardFront;
        
        [SerializeField] private GameObject ability;
        private CardInstance _cardInstance;
        
        public void Init(CardInstance instance)
        {
            _cardInstance = instance;
            var cardAbility = ability.GetComponent<Image>();
            cardAbility.color = _cardInstance.CardAsset.color;
            cardAbility.sprite = _cardInstance.CardAsset.image;
            cardAbility.name = _cardInstance.CardAsset.cardName;
        }

        public void ShowFrontSide()
        {
            cardFront.SetActive(true);
            cardBack.SetActive(false);
        }

        public void PlayCard()
        {
            _cardInstance.MoveToLayout(CardGame.Instance.FieldLayoutId);
        }

        public void ShowBackSide()
        {
            cardFront.SetActive(false);
            cardBack.SetActive(true);
        }
        
        public void OnPointerDown(PointerEventData eventData)
        {
            PlayCard();
        }
    }
}
