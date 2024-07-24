using UnityEngine;

public enum PlayerStance
{
    UNLOCKED,
    LOCKED
}
[CreateAssetMenu(fileName = "Controller Data", menuName = "Scriptable/Controller Data")]
public class ControllerData : ScriptableObject
{
    public float playerSpeed = 2f;
    public float mouseSensitivity = 0.5f;
    public PlayerStance playerStance;
}
