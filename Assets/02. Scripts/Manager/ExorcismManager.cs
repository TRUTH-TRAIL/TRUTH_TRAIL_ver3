using UnityEngine;

namespace TT
{
    public class ExorcismManager : BaseManager
    {
        public static ExorcismManager Instance;

        public int placedCandleCount = 0;
        public int litCandleCount = 0;
        public bool hasPlacedCross = false;
        public bool hasPlacedExorcismBook = false;

        private const int totalCandleCount = 3; 

        private void Awake()
        {
            Instance = this;
        }

        private void CheckGameClear()
        {
            if (placedCandleCount >= totalCandleCount &&
                litCandleCount >= totalCandleCount &&
                hasPlacedCross && hasPlacedExorcismBook)
            {
                Debug.Log("GameClear");
            }
        }

        public void PlaceCandle()
        {
            placedCandleCount++;
            CheckGameClear();
        }

        public void LightCandle()
        {
            litCandleCount++;
            CheckGameClear();
        }

        public void PlaceCross()
        {
            hasPlacedCross = true;
            CheckGameClear();
        }

        public void PlaceExorcismBook()
        {
            hasPlacedExorcismBook = true;
            CheckGameClear();
        }
    }
}