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
        private static CardGame _instance;

        [SerializeField] private GameObject cardPrefab;
        [SerializeField] private List<int> playerLayoutIds;
        [SerializeField] private int deckLayoutId;
        [field: SerializeField] public int FieldLayoutId { get; private set; }

        [SerializeField] public int HandCapacity;
        [field: SerializeField] private List<CardAsset> StartingCards { get; set; } = new List<CardAsset>();
        [field: SerializeField] private List<CardAsset> Deck { get; set; } = new List<CardAsset>();
        
        private readonly Dictionary<CardInstance, CardView> _cards = new Dictionary<CardInstance, CardView>();

        public static CardGame Instance
        {
            get { return _instance; }
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
                .OrderBy(card => card.CardPosition)
                .ToList();
        }

        public CardView GetCardView(CardInstance instance) => _cards[instance];

        public void StartTurn()
        {
            foreach (int playerLayoutId in playerLayoutIds)
            {
                var cardsInHand = GetCardsInLayout(playerLayoutId);

                var currCardsCount = cardsInHand.Count;
                while (currCardsCount < HandCapacity)
                {
                    var cardsInDeck = GetCardsInLayout(deckLayoutId);
                    if (cardsInDeck.Count == 0)
                        return;
                    var card = cardsInDeck[0];
                    card.MoveToLayout(playerLayoutId);
                    ++currCardsCount;
                }
            }
        }

        private void StartGame()
        {
            foreach (var playerLayout in playerLayoutIds)
            foreach (var cardAsset in StartingCards)
                CreateCard(cardAsset, playerLayout);

            InitializeDeck();
        }


        private void CreateCardView(CardInstance instance)
        {
            var cardView = Instantiate(cardPrefab, transform).GetComponent<CardView>();
            cardView.Init(instance);
            _cards[instance] = cardView;
        }

        private void InitializeDeck()
        {
            foreach (var cardAsset in Deck)
                CreateCard(cardAsset, deckLayoutId);
        }

        private void CreateCard(CardAsset asset, int layoutId)
        {
            CardInstance instance = new CardInstance(asset);
            CreateCardView(instance);
            instance.MoveToLayout(layoutId);
        }

        public void ShuffleLayout(int layoutId)
        {
            var cards = GetCardsInLayout(layoutId);
            for (int i = 0; i < cards.Count; ++i)
            {
                int index = UnityEngine.Random.Range(0, cards.Count);
                (cards[i].CardPosition, cards[index].CardPosition) = (cards[index].CardPosition, cards[i].CardPosition);
            }
        }


        public void RecalculateLayout(int layoutId)
        {
            var cardsInLayout = GetCardsInLayout(layoutId);

            for (int i = 0; i < cardsInLayout.Count; ++i)
            {
                var card = cardsInLayout[i];
                card.CardPosition = i;
            }
        }
    }
}
