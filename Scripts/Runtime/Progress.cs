﻿using System;
using Unity.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace ProgressBar.Scripts.Runtime
{
    [ExecuteInEditMode]
    public class Progress : MonoBehaviour
    {
        [Header("Values")] [SerializeField] [Range(0f, 1f)]
        private float value = 1f;

        [SerializeField] [Range(0f, 1f)] private float delayValue = 1f;

        [Header("Behaviour")] [SerializeField] private float speed = 1f;
        [SerializeField] private float delay = 0.5f;

        [Header("Images")] [SerializeField] private Image imgBackground;
        [SerializeField] private Image imgPrediction;
        [SerializeField] private Image imgFill;

        private float _elapsedFill;
        public float Value => value;

        public void Update()
        {
            if (delayValue <= value)
            {
                SetDelayValue(value);
                _elapsedFill = 0;
                return;
            }

            if (_elapsedFill < delay)
            {
                _elapsedFill += Time.deltaTime;
            }
            else
            {
                var p = delayValue - speed * Time.deltaTime;
                p = Mathf.Max(value, p);
                SetDelayValue(p);
            }
        }

        private void OnValidate()
        {
            UpdateValueSprite();
            UpdateDelaySprite();
        }

        public bool IsAnimating()
        {
            return value < delayValue;
        }

        public void SetFill(float value)
        {
            value = Mathf.Clamp01(value);
            this.value = value;
            UpdateValueSprite();
        }

        public void SetFillColor(Color color)
        {
            if (imgFill != null)
            {
                imgFill.color = color;
            }
        }

        public void SetPredictionColor(Color color)
        {
            if (imgPrediction != null)
            {
                imgPrediction.color = color;
            }
        }

        public void SetBackgroundColor(Color color)
        {
            if (imgBackground != null)
            {
                imgBackground.color = color;
            }
        }

        private void SetDelayValue(float value)
        {
            value = Mathf.Clamp01(value);
            delayValue = value;
            UpdateDelaySprite();
        }

        private void UpdateValueSprite()
        {
            if (imgFill != null)
            {
                imgFill.fillAmount = value;
            }
        }

        private void UpdateDelaySprite()
        {
            if (imgPrediction != null)
            {
                imgPrediction.fillAmount = delayValue;
            }
        }
    }
}