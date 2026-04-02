using UnityEngine;

[CreateAssetMenu(fileName = "NewDifficulty", menuName = "Game/Difficulty Settings")]
public class DifficultySettings : ScriptableObject
{
    public float pipeSpeed;
    public float spawnInterval;
    public float minGapSize;
    public float maxGapSize;
    public float gravityMultiplier;
    public float jumpForce;
    public float minY;
    public float maxY;
}