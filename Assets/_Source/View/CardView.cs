using Model;
using System;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace View
{
    public class CardView : MonoBehaviour
    {
        [SerializeField]
        private GameObject cardBack;
        [SerializeField]
        private GameObject cardFront;
        
        private CardInstance _cardInstance;
        

        public void Init(CardInstance instance)
        {
            _cardInstance = instance;
            var cardRenderer = cardFront.GetComponent<Image>();

            cardRenderer.color = _cardInstance.CardAsset.color;
            cardRenderer.sprite = _cardInstance.CardAsset.image;
            cardRenderer.name = _cardInstance.CardAsset.cardName;
        }

        public void ShowFrontSide()
        {
            cardFront.SetActive(true);
            cardBack.SetActive(false);
        }

        public void ShowBackSide()
        {
            cardFront.SetActive(false);
            cardBack.SetActive(true);
        }
    }
}
