using Model;
using ScriptableObjects;
using System;
using System.Collections.Generic;
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

        
        //public List<CardAsset> StartingCards
        //{
        //    get { return _startingCards; }
        //}

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
