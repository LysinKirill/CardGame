using ScriptableObjects;
using System;

namespace Model
{
    public class CardInstance
    {
        public int LayoutId;
        public int CardPosition;
        private CardAsset _cardAsset;

        public CardInstance(CardAsset asset)
        {
            _cardAsset = asset;
        }

        public void MoveToLayout(int newLayoutId)
        {
            LayoutId = newLayoutId;
            CardPosition = 0;
        }
    }
}
