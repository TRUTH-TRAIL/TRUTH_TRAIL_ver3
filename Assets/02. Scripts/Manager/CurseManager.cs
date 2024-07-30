using System.Collections.Generic;
using UnityEngine;

namespace TT
{
    public class CurseManager : Singleton<CurseManager>
    {
        private List<ICurse> availableCurses = new List<ICurse>();

        private void Start()
        {
            // 초기 저주 목록 설정
            availableCurses.Add(new StairCurse(GameObject.FindWithTag("Player")));
            availableCurses.Add(new LookBackCurse(GameObject.FindWithTag("Player")));
            // 다른 저주들도 추가
        }

        public ICurse GetRandomCurse()
        {
            if (availableCurses.Count == 0)
                return null;

            int index = Random.Range(0, availableCurses.Count);
            ICurse selectedCurse = availableCurses[index];
            availableCurses.RemoveAt(index); // 선택된 저주는 목록에서 제거

            return selectedCurse;
        }

        public void AddCurse(ICurse curse)
        {
            if (!availableCurses.Contains(curse))
            {
                availableCurses.Add(curse);
            }
        }
    }
}