using UnityEngine;

[CreateAssetMenu(menuName = "Configurations/Create AttachCarConfigurations", fileName = "AttachCarConfigurations", order = 0)]
public class AttachCarConfigurations : ScriptableObject
{
    [SerializeField] private float speedLerp;
    [SerializeField] private float speedRotate;

    public float SpeedLerp => speedLerp;

    public float SpeedRotate => speedRotate;
}