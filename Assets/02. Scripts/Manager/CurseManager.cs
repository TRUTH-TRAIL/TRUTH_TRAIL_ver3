using System.Collections.Generic;
using UnityEngine;

namespace TT
{
    public class CurseManager : MonoBehaviour
    {
        private List<ICurse> availableCurses = new List<ICurse>();
        private GameObject player;
        
        private void Start()
        {
            player = GameObject.FindWithTag("Player");

            // 초기 저주 목록 설정
            availableCurses.Add(new StairCurse(player));
            availableCurses.Add(new LookBackCurse(player));
            availableCurses.Add(new MoveSideCurse());
            availableCurses.Add(new DoubleClickDoorCurse());
            availableCurses.Add(new ExitHouseCurse());
            availableCurses.Add(new OpenBasementDoorCurse());
            availableCurses.Add(new LookOutWindowCurse());
            availableCurses.Add(new OpenDrawerCurse());
            availableCurses.Add(new RemoveBooksCurse());
            availableCurses.Add(new WalkForwardCurse());
            availableCurses.Add(new BathroomFloorCurse());
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
        
    }
}