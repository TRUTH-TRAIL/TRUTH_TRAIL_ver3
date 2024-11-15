using UnityEngine;

namespace TT
{
    public class ExorcismManager : BaseManager
    {
        public static ExorcismManager Instance;

        // 퇴마 조건
        public int placedCandleCount = 0; 
        public int litCandleCount = 0;
        public bool hasPlacedCross = false;
        public bool hasPlacedExorcismBook = false;
        private const int totalCandleCount = 3;

        public GameObject Timeline_E;
 

        private void Awake()
        {
            Instance = this;
        }

        /// 게임 성공
        private void CheckGameClear()
        {
            if (placedCandleCount >= totalCandleCount &&
                litCandleCount >= totalCandleCount &&
                hasPlacedCross && hasPlacedExorcismBook)
            {
                Debug.Log("GameClear");
                Timeline_E.SetActive(true);
            }
        }

        /// 퇴마 조건
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