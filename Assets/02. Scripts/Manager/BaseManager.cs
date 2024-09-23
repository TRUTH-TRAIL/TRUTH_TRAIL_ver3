using UnityEngine;

namespace TT
{
    public enum GameState
    {
        Tutorial = 0,
        MainGame = 1,
        Exorcism = 2,
    }
    
    public class BaseManager : MonoBehaviour
    {
        public GameState State;
    }
}
