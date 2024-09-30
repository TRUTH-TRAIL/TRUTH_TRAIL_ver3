using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TT
{
    public class CameraShake : MonoBehaviour
    {
        public float shakeDuration = 5f;    // 흔들림 지속시간
        public float shakeMagnitude = 0.5f;   // 흔들림 진폭
        public float shakeFrequency = 3f;   // 흔들림 속도
        private Vector3 initialPosition;

        /// 카메라 흔들림
        public void OnCameraShake(float duration = 0f)
        {
            initialPosition = transform.localPosition;
            StartCoroutine(ShakeCoroutine(duration > 0 ? duration : shakeDuration));
        }

        private IEnumerator ShakeCoroutine(float duration)
        {
            float elapsedTime = 0f;

            while (elapsedTime < duration)
            {
                elapsedTime += Time.deltaTime;

                // Perlin Noise(부드러운 흔들림)
                float xOffset = (Mathf.PerlinNoise(Time.time * shakeFrequency, 0f) * 2 - 1) * shakeMagnitude;
                float yOffset = (Mathf.PerlinNoise(0f, Time.time * shakeFrequency) * 2 - 1) * shakeMagnitude;

                transform.localPosition = initialPosition + new Vector3(xOffset, yOffset, 0);

                yield return null;
            }

            // 원위치
            transform.localPosition = initialPosition;
        }
    }
}