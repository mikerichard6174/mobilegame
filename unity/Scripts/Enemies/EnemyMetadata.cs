using UnityEngine;

public class EnemyMetadata : MonoBehaviour
{
    [SerializeField] private EnemyTier tier = EnemyTier.Normal;
    [SerializeField] private int scoreValue = 1;

    public EnemyTier Tier => tier;
    public int ScoreValue => scoreValue;
}
