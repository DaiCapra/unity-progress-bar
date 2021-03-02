using System;
using Unity.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace ProgressBar.Scripts.Runtime
{
    [ExecuteInEditMode]
    public class Progress : MonoBehaviour
    {
        [Header("Values")] [SerializeField] [Range(0f, 1f)]
        private float fill = 1f;

        [SerializeField] [Range(0f, 1f)] private float prediction = 1f;

        [Header("Behaviour")] [SerializeField] private float speed = 1f;
        [SerializeField] private float delay = 0.5f;

        [Header("Images")] [SerializeField] private Image imgBackground;
        [SerializeField] private Image imgPrediction;
        [SerializeField] private Image imgFill;

        private float _elapsedFill;

        private void OnValidate()
        {
            UpdateFillSprite();
            UpdatePredictionSprite();
        }


        public void SetFill(float value)
        {
            value = Mathf.Clamp01(value);
            fill = value;
            UpdateFillSprite();
        }

        public bool IsAnimating()
        {
            return fill < prediction;
        }

        private void SetPrediction(float value)
        {
            value = Mathf.Clamp01(value);
            prediction = value;
            UpdatePredictionSprite();
        }

        private void UpdateFillSprite()
        {
            if (imgFill != null)
            {
                imgFill.fillAmount = fill;
            }
        }

        private void UpdatePredictionSprite()
        {
            if (imgPrediction != null)
            {
                imgPrediction.fillAmount = prediction;
            }
        }

        public void Update()
        {
            if (prediction <= fill)
            {
                SetPrediction(fill);
                _elapsedFill = 0;
                return;
            }

            if (_elapsedFill < delay)
            {
                _elapsedFill += Time.deltaTime;
            }
            else
            {
                var p = prediction - speed * Time.deltaTime;
                p = Mathf.Max(fill, p);
                SetPrediction(p);
            }
        }
    }
}