using UnityEngine;

namespace Patawee_Hutamai
{
    public class ThirdPersonCam : MonoBehaviour
    {
        [Header("References")]
        public Transform cameraTransform;
        public Transform orientation;
        public Transform player;
        public Transform playerObj;
        public float rotationSpeed = 7f;

        void Update()
        {
            // Rotate orientation to match camera view (ignoring the Y axis pitch)
            Vector3 viewDir = player.position - new Vector3(cameraTransform.position.x, player.position.y, cameraTransform.position.z);
            orientation.forward = viewDir.normalized;

            // Rotate player object towards movement input direction
            float horizontalInput = Input.GetAxis("Horizontal");
            float verticalInput = Input.GetAxis("Vertical");
            Vector3 inputDir = orientation.forward * verticalInput + orientation.right * horizontalInput;

            if (inputDir != Vector3.zero)
                playerObj.forward = Vector3.Slerp(playerObj.forward, inputDir.normalized, Time.deltaTime * rotationSpeed);
        }
    }
}