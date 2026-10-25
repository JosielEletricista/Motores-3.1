using UnityEngine;

[CreateAssetMenu(fileName = "NewRoboData", menuName = "GuerraDeRobos/RoboData")]
public class RoboData : ScriptableObject
{
    public string roboName;
    public GameObject roboPrefab;
    public Color primaryColor;
    public Color secondaryColor;

    [Header("Status")]
    public float scale = 1f;
    public float baseSpeed = 5f;
    public float pushForceMultiplier = 10f;
}