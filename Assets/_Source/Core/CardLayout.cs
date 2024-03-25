using Model;
using System;
using UnityEngine;
using UnityEngine.UIElements;
using View;

namespace Core
{
    public class CardLayout : MonoBehaviour
    {

        [SerializeField] public int LayoutId;
        [SerializeField] public Vector2 Offset;

        [field: SerializeField] public bool FaceUp { get; private set; }
        
        private RectTransform _rectTransform; private void Awake()
        {
            _rectTransform = GetComponent<RectTransform>();
        }

        private void Update()
        {
            
            var cardsInLayout = CardGame.Instance.GetCardsInLayout(LayoutId);
            foreach (var cardInstance in cardsInLayout)
            {
                CardView cardView = CardGame.Instance.GetCardView(cardInstance);
                var cardTransform = cardView.transform;
                cardTransform.SetParent(transform, false);
                cardTransform.localPosition = GetLocalPosition(cardInstance);
                cardTransform.SetSiblingIndex(cardInstance.CardPosition);
                Rotate(cardView, FaceUp);
            }
        }

        private Vector3 GetLocalPosition(CardInstance card)
        {
            var rect = _rectTransform.rect;
            var width = rect.width;
            var height = rect.height;
            var position = Offset * card.CardPosition;
            position.x -= width / 2;
            return position;
        }

        private void Rotate(CardView cardView, bool faceUp)
        {
            if(faceUp)
                cardView.ShowFrontSide();
            else
                cardView.ShowBackSide();
        }
    }
}
