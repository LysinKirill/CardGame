using Model;
using ScriptableObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;
using View;

namespace Core
{
    public class CardGame : MonoBehaviour
    {
        [SerializeField]
        private GameObject cardPrefab;
        [SerializeField]
        private List<int> playerLayoutIds;
        
        private static CardGame _instance;
        
        
        private Dictionary<CardInstance, CardView> _cards = new Dictionary<CardInstance, CardView>();
        [field: SerializeField]
        private List<CardAsset> StartingCards { get; set; } = new List<CardAsset>();

        
        public static CardGame Instance
        {
            get
            {
                return _instance;
            }
            set
            {
                if (_instance == null)
                    _instance = value;
            }
        }

        private CardGame() { }

        private void Awake()
        {
            _instance = this;
        }

        private void Start()
        {
            StartGame();
        }
        public List<CardInstance> GetCardsInLayout(int layoutId)
        {
            return _cards
                .Where(pair => pair.Key.LayoutId == layoutId)
                .Select(pair => pair.Key)
                .ToList();
        }

        public CardView GetCardView(CardInstance instance) => _cards[instance];

        private void StartGame()
        {
            foreach(var playerLayout in playerLayoutIds)
            foreach (var cardAsset in StartingCards)
                CreateCard(cardAsset, playerLayout);
        }
        
        private void CreateCardView(CardInstance instance)
        {
           var cardView = Instantiate(cardPrefab, transform).GetComponent<CardView>();
           cardView.Init(instance);
           _cards[instance] = cardView;
        }

        private void CreateCard(CardAsset asset, int layoutId)
        {
            CardInstance instance = new CardInstance(asset);
            CreateCardView(instance);
            instance.MoveToLayout(layoutId);
        }
    }
}
