using Core;
using ScriptableObjects;

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
            var oldLayoutId = LayoutId;
            CardPosition = CardGame.Instance.GetCardsInLayout(newLayoutId).Count;
            LayoutId = newLayoutId;
            CardGame.Instance.RecalculateLayout(oldLayoutId);
        }
    }
}
