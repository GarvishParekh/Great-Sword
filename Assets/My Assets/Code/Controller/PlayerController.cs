using UnityEngine;

namespace Game.GreatSword.Controller
{
    [RequireComponent(typeof(Rigidbody))]  
    public class PlayerController : MonoBehaviour
    {
        [Header("<size=15>SCRIPTABLE OBJECT")]
        [SerializeField] private InputData inputData;
        [SerializeField] private ControllerData controllerData;

        [Header("<size=15>MOVEMENT COMPONENTS")]
        [SerializeField] private Transform refDirectionTransform;
        [SerializeField] private Transform cameraHolder;
        [SerializeField] private Transform directionTransform;
        [SerializeField] private Transform mainHolder;

        [Header("<size=15>COMPONENTS")]
        [SerializeField] private Rigidbody playerRb;
        [SerializeField] private Animator playerAnime;

        //MOVEMENT VECTORS
        Vector3  velocityVector;
        Vector3 forwardMovement;
        Vector3 sideMovement;

        float gravityValue = 0;

        #region UnityFunctions

        private void Awake()
            => playerRb = GetComponent<Rigidbody>();

        private void Start()
        {
            Application.targetFrameRate = 60;   
        }

        private void Update()
        {
            PlayerMovement();
            PlayerAnimation();
        }

        #endregion

        private void PlayerMovement()
        {
            switch (controllerData.playerStance)
            {
                case PlayerStance.UNLOCKED:
                    Direction();
                    UnlockedMovement();
                    PlayerRotation();
                    CameraController();
                    break;
                case PlayerStance.LOCKED:
                    LockedMovement();
                    break;
            }
        }

        private void UnlockedMovement()
        {
            if (inputData.runningInputs == 1)
                controllerData.playerSpeed = 6;
            else if (inputData.runningInputs == 0)
                controllerData.playerSpeed = 3;

            forwardMovement = refDirectionTransform.forward * Mathf.Abs(inputData.forwardInputs);
            sideMovement = refDirectionTransform.forward * Mathf.Abs(inputData.sideInputs);

            velocityVector = (forwardMovement + sideMovement).normalized * controllerData.playerSpeed;
            gravityValue = playerRb.velocity.y;

            playerRb.velocity = new Vector3(velocityVector.x, gravityValue, velocityVector.z);
        }

        private void LockedMovement()
        {

        }

        public void PlayerRotation()
        {
            mainHolder.rotation = Quaternion.Euler(0, inputData.mouseDeltaX * controllerData.mouseSensitivity, 0);
        }
        public float rotationSpeed = 720f;
        private void Direction()
        {
            Vector3 direction = new Vector3(inputData.sideInputs, 0f, inputData.forwardInputs);

            // If there is some input (i.e., the player is pressing a key)
            if (direction.magnitude >= 0.1f)
            {
                // Calculate the target angle in degrees
                float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;

                // Smoothly rotate towards the target angle
                float angle = Mathf.SmoothDampAngle(refDirectionTransform.localEulerAngles.y, targetAngle, ref rotationSpeed, 0.03f);

                // Apply the rotation
                refDirectionTransform.localRotation = Quaternion.Euler(0f, angle, 0f);
            }

            directionTransform.localRotation = Quaternion.Lerp(directionTransform.localRotation, refDirectionTransform.localRotation, 5 * Time.deltaTime);
        }

        private void PlayerAnimation()
        {
            if (inputData.forwardInputs == 0 && inputData.sideInputs == 0)
                playerAnime.SetBool("isWalking", false);
            else
            {
                playerAnime.SetBool("isWalking", true);
                if (inputData.runningInputs == 1)
                    playerAnime.SetBool("isRunning", true);
                else if (inputData.runningInputs == 0)
                    playerAnime.SetBool("isRunning", false);
            }
        }

        private void CameraController()
        {
            cameraHolder.localRotation = Quaternion.Euler(inputData.mouseDeltaY, 0, 0);
        }

    }
}