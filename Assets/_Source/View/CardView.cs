﻿using Model;
using UnityEngine;

namespace View
{
    public class CardView : MonoBehaviour
    {
        private CardInstance _cardInstance;

        public void Init(CardInstance instance)
        {
            _cardInstance = instance;
        }
    }
}
