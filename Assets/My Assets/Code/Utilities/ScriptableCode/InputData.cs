using UnityEngine;

public enum InputType
{
    KEYBOARD,
    TOUCH
}

[CreateAssetMenu(fileName = "Input Data", menuName = "Scriptable/Input Data")]
public class InputData : ScriptableObject
{
    public InputType inputType;
    [Header ("<Size=15>KEYBOARD INPUTS")]
    public float forwardInputs;
    public float sideInputs;

    [Space]
    public int runningInputs;

    [Header ("<Size=15>MOUSE INPUTS")]
    public float mouseDeltaX;
    public float mouseDeltaY;

}
