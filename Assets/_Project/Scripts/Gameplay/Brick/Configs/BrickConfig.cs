using UnityEngine;

namespace MiniIT.ARKANOID
{
    [CreateAssetMenu(fileName = "BrickConfig", menuName = "Game/Bricks/Brick Config")]
    public class BrickConfig : ScriptableObject
    {
        [Header("Visual")]
        [SerializeField] private Sprite      sprite;
        [SerializeField] private Color       tintColor = Color.white;

        [Header("Gameplay")]
        [SerializeField, Min(1)] private int hitPoints = 1;
        [SerializeField] private int         scoreReward = 100;
        [SerializeField] private bool        isIndestructible = false;

        public Sprite Sprite => sprite;
        public Color  TintColor => tintColor;
        public int    HitPoints => hitPoints;
        public int    ScoreReward => scoreReward;
        public bool   IsIndestructible => isIndestructible;
    }
}