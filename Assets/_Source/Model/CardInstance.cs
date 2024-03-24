using Core;
using ScriptableObjects;
using System;

namespace Model
{
    public class CardInstance
    {
        public int LayoutId;
        public int CardPosition;
        public CardAsset CardAsset { get; private set; }

        public CardInstance(CardAsset asset)
        {
            CardAsset = asset;
        }

        public void MoveToLayout(int newLayoutId)
        {
            CardPosition = CardGame.Instance.GetCardsInLayout(newLayoutId).Count;
            LayoutId = newLayoutId;
        }
    }
}
