using UnityEngine;

namespace TT
{
    public class CheckIfRedBeam : MonoBehaviour
    {
        bool m_IsInsideBeam = false;
        [SerializeField] private Collider m_Collider = null;

        [SerializeField] private AIController aIController;
  
        private void FixedUpdate()
        {
            m_IsInsideBeam = false;
        }

        private void OnTriggerStay(Collider trigger)
        {
            var dynamicOcclusion = trigger.GetComponent<VLB.DynamicOcclusionRaycasting>();

            if (dynamicOcclusion)
            {
                m_IsInsideBeam = !dynamicOcclusion.IsColliderHiddenByDynamicOccluder(m_Collider);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            m_IsInsideBeam = false;
        }

        private void Update()
        {
            aIController.IsInRedBeam = m_IsInsideBeam;
        }
    }
}
