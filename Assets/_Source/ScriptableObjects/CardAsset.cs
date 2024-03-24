using UnityEngine;
using UnityEngine.UI;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "CardAsset", menuName = "SO/newCardAsset")]
    public class CardAsset : ScriptableObject
    {
        public string cardName;
        public Color color;
        public Sprite image;
    }
}
