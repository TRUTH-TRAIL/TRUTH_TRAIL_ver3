using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TT
{
    public class CameraShake : MonoBehaviour
    {
        public float shakeDuration = 5f;    // ��鸲 ���ӽð�
        public float shakeMagnitude = 0.5f;   // ��鸲 ����
        public float shakeFrequency = 3f;   // ��鸲 �ӵ�
        private Vector3 initialPosition;

        /// ī�޶� ��鸲
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

                // Perlin Noise(�ε巯�� ��鸲)
                float xOffset = (Mathf.PerlinNoise(Time.time * shakeFrequency, 0f) * 2 - 1) * shakeMagnitude;
                float yOffset = (Mathf.PerlinNoise(0f, Time.time * shakeFrequency) * 2 - 1) * shakeMagnitude;

                transform.localPosition = initialPosition + new Vector3(xOffset, yOffset, 0);

                yield return null;
            }

            // ����ġ
            transform.localPosition = initialPosition;
        }
    }
}