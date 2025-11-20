using UnityEngine;
using UnityEngine.UI;
using Zenject;
using TMPro;

namespace MiniIT.ARKANOID
{
    public class GameplayInstaller : MonoInstaller
    {
        [SerializeField] private Camera                 _mainCamera;

        [Header("World Walls")]
        [SerializeField] private Transform              _topWall;
        [SerializeField] private Transform              _bottomWall;
        [SerializeField] private Transform              _leftWall;
        [SerializeField] private Transform              _rightWall;
        [SerializeField] private WorldBoundsConfig      _worldBoundsConfig;

        [Header("Platform")]
        [SerializeField] private PlatformConfig         _platformConfig;
        [SerializeField] private Slider                 _movementPlatformSlider;

        [Header("Ball")]
        [SerializeField] private BallConfig             _ballConfig;
        [SerializeField] private Collider2D             _bottomLoseTrigger;
        [SerializeField] private Transform              _ballSpawnPoint;

        [Header("Bricks")]
        [SerializeField] private LevelBricksConfig      _levelBricksConfig;
        [SerializeField] private BrickView              _brickPrefab;
        [SerializeField, Min(1)] private int            _brickInitialPoolSize = 40;

        [Header("Brick Effects")]
        [SerializeField] private BrickDestroyEffectView _brickDestroyEffectPrefab;
        [SerializeField, Min(1)] private int            _brickDestroyEffectInitialPoolSize = 20;

        [Header("HUD References")]
        [SerializeField] private TextMeshProUGUI        _scoreText;

        [Header("Win Window")]
        [SerializeField] private Image                  _winWindow;
        [SerializeField] private TextMeshProUGUI        _winScoreText;
        [SerializeField] private Button                 _winRepeatButton;

        [Header("Lose Window")]
        [SerializeField] private Image                  _loseWindow;
        [SerializeField] private TextMeshProUGUI        _loseScoreText;
        [SerializeField] private Button                 _loseRestartButton;

        [Header("Audio")]
        [SerializeField] private GameplayAudioConfig    _gameplayAudioConfig;
        [SerializeField] private AudioSource            _sfxAudioSource;

        public override void InstallBindings()
        {
            if (_mainCamera == null)
            {
                _mainCamera = Camera.main;
            }

            Container.Bind<Camera>().FromInstance(_mainCamera).AsSingle();

            Container.Bind<PlatformConfig>().FromInstance(_platformConfig).AsSingle();
            Container.Bind<Slider>().FromInstance(_movementPlatformSlider).AsSingle();

            Container.Bind<BallConfig>().FromInstance(_ballConfig).AsSingle();
            Container.Bind<Collider2D>().FromInstance(_bottomLoseTrigger).AsSingle();
            Container.Bind<Transform>().FromInstance(_ballSpawnPoint).AsSingle();

            Container.Bind<WorldBounds>().AsSingle().WithArguments(_worldBoundsConfig, _mainCamera, _topWall,
                _bottomWall,
                _leftWall, _rightWall);

            Container.Bind<LevelBricksConfig>().FromInstance(_levelBricksConfig).AsSingle();
            Container.Bind<BrickView>().FromInstance(_brickPrefab).AsSingle();

            Container.Bind<GameSession>().AsSingle();

            var hud = new GameHudUiReferences(
                _scoreText, _winWindow, _winScoreText, _winRepeatButton,
                _loseWindow, _loseScoreText, _loseRestartButton);
            Container.Bind<GameHudUiReferences>().FromInstance(hud).AsSingle();

            Container.Bind<GameplayAudioConfig>().FromInstance(_gameplayAudioConfig).AsSingle();
            Container.Bind<AudioSource>().FromInstance(_sfxAudioSource).AsSingle();
            Container.Bind<GameplayAudio>().AsSingle();


            Container.Bind<ObjectPool<BrickView>>().FromMethod(ctx => new ObjectPool<BrickView>(_brickPrefab,
                null, _brickInitialPoolSize)).AsSingle();

            Container.Bind<ObjectPool<BrickDestroyEffectView>>().FromMethod(ctx =>
                new ObjectPool<BrickDestroyEffectView>(_brickDestroyEffectPrefab, null,
                    _brickDestroyEffectInitialPoolSize)).AsSingle();
        }
    }
}