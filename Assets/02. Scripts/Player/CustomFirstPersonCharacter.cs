using ECM2.Examples.FirstPerson;
using UnityEngine;

namespace TT
{
    public class CustomFirstPersonCharacter : FirstPersonCharacter
    {
        [Header("Run")]
        [SerializeField] private float speedMutiplier = 1.3f;
        [SerializeField] private KeyCode runKey = KeyCode.LeftShift;
        
        private float originSpeed;

        protected override void Start()
        {
            originSpeed = maxWalkSpeed;
        }

        private void Update()
        {
            if (Input.GetKeyDown(runKey))
            {
                maxWalkSpeed *= speedMutiplier;
            }
            else if (Input.GetKeyUp(runKey))
            {
                maxWalkSpeed = originSpeed;
            }
        }
    }
}
