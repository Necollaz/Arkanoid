using UnityEngine;

namespace MiniIT.ARKANOID
{
    [CreateAssetMenu(fileName = "BrickConfig", menuName = "Game/Bricks/Brick Config")]
    public class BrickConfig : ScriptableObject
    {
        [Header("Visual")]
        [SerializeField] private Sprite      _sprite;
        [SerializeField] private Color       _tintColor = Color.white;

        [Header("Gameplay")]
        [SerializeField, Min(1)] private int _hitPoints = 1;
        [SerializeField] private int         _scoreReward = 100;
        [SerializeField] private bool        _isIndestructible = false;

        public Sprite Sprite => _sprite;
        public Color  TintColor => _tintColor;
        public int    HitPoints => _hitPoints;
        public int    ScoreReward => _scoreReward;
        public bool   IsIndestructible => _isIndestructible;
    }
}