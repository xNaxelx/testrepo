using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    class ScoreManager : MonoBehaviour
    {
        [SerializeField] private PriceIndicator _priceIndicator;
        [SerializeField] private TMPro.TMP_Text _playerScoreText;

        private const float MaxDistance = 4f;
        private static ScoreManager _instance;
        
        private PlayerBalance _playerBalance;
        private List<PriceIndicator> _indicators;
        private int _score  = 0;
        private bool initDone;

        private void Awake()
        {
            if (_instance != null && _instance!=this)
            {
                Debug.Log("Destroy",_instance.gameObject);
                Destroy(this);
                return;
            }
            Init();
        }
        public static ScoreManager GetInstance()
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<ScoreManager>();
                _instance.Init();
            }
            if (_instance != null) return _instance;

            Debug.LogWarning("Score manager is not found on scene, creating new blank instance!");
            _instance = new GameObject().AddComponent<ScoreManager>();
            return _instance;
        }

        private void Init()
        {
            if(initDone) return;
            if (_priceIndicator == null) Debug.LogWarning("Score manager's price indicator object is not set, displaying disabled", _instance);
            if (_playerScoreText == null) Debug.LogWarning("Score manager's score text field is not set, displaying disabled", _instance);
            _indicators = new List<PriceIndicator>(10);
            _playerBalance = PlayerBalance.GetInstance();
            initDone = true;
        }
        public void ScoreChanged(Vector3 position, int value)
        {
            if (_playerScoreText)
            {
                _score += value;
                _playerScoreText.text = _score + "$";
            }
            if (_priceIndicator == null) return;

            var closest = GetClosest(position,Math.Sign(value));
            if (!_indicators.Contains(closest))
            {
                _indicators.Add(closest);
                closest.LifetimeEnded += PriceIndicatorExpired;
                closest.SetValue(value);
            }
            else
            {
                closest.AddValue(value);
            }
        }

        public bool ChangePlayerBalance(int amount)
        {
            if (_playerBalance.Get() + amount < 0) return false;
            _playerBalance.Add(amount);
            return true;
        }

        private void OnDestroy()
        {
            _playerBalance?.Save();
        }

        private PriceIndicator GetClosest(Vector3 position,int sign = 1)
        {
            PriceIndicator closest = null;

            float minDistance = MaxDistance;
            foreach (var indicator in _indicators)
            {
                if (Math.Sign(indicator.GetValue()) != sign) continue;

                float dist = Vector3.Distance(position, indicator.transform.position);
                if (!(dist < minDistance)) continue;

                minDistance = dist;
                closest = indicator;
            }

            if (closest == null) closest = _priceIndicator.CreateCopyAt(position).GetComponent<PriceIndicator>();
            return closest;

        }

        private void PriceIndicatorExpired(PriceIndicator indicator)
        {
            _indicators.Remove(indicator);
            indicator.LifetimeEnded -= PriceIndicatorExpired;
            Destroy(indicator);
        }
    }
}
