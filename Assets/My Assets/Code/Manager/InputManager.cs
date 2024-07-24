using UnityEngine;
using UnityEngine.UI;

namespace Game.GreatSword.Manager
{
    public class InputManager : MonoBehaviour
    {
        [SerializeField] private InputData inputData;
        [SerializeField] private Joystick joystick;

        float deltaX = 0;
        float currentX = 0;

        float deltaY = 0;
        float currentY = 0;

        #region UnityFunctions

        private void Update()
        {
            switch (inputData.inputType)
            {
                case InputType.KEYBOARD:
                    NoteKeyboardInputs();
                    NoteMouseInputs();
                    NoteRunningInputs();
                break;
                case InputType.TOUCH:
                    NoteJoystickInputs();
                break;
            }
        }

        #endregion

        private void NoteJoystickInputs()
        {
            inputData.forwardInputs = joystick.Vertical;
            inputData.sideInputs = joystick.Horizontal; 
        }

        public void NoteKeyboardInputs()
        {
            inputData.forwardInputs = Input.GetAxisRaw("Vertical");
            inputData.sideInputs = Input.GetAxisRaw("Horizontal");
        }


        public void NoteMouseInputs()
        {
            currentX = Input.GetAxis("Mouse X");
            deltaX += currentX;

            currentY = Input.GetAxis("Mouse Y");
            deltaY -= currentY;

            inputData.mouseDeltaX = deltaX;

            deltaY = Mathf.Clamp(deltaY, -20, 50);
            inputData.mouseDeltaY = deltaY;
        }

        public void NoteRunningInputs()
        {
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                inputData.runningInputs = 1;
            }
            else if (Input.GetKeyUp(KeyCode.LeftShift))
            {
                inputData.runningInputs = 0;
            }
        }

        public void NoteRunningTouchInputs(Toggle sprintToggle)
        {
            if (sprintToggle.isOn)
            {
                inputData.runningInputs = 1;
            }
            else if (!sprintToggle.isOn)
            {
                inputData.runningInputs = 0;
            }
        }
    }
}
