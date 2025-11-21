using UnityEngine;
using UnityEngine.UI;
using Zenject;
using TMPro;

namespace MiniIT.ARKANOID
{
    public class GameplayInstaller : MonoInstaller
    {
        [SerializeField] private Camera                 mainCamera;

        [Header("World Walls")]
        [SerializeField] private Transform              topWall;
        [SerializeField] private Transform              bottomWall;
        [SerializeField] private Transform              leftWall;
        [SerializeField] private Transform              rightWall;
        [SerializeField] private WorldBoundsConfig      worldBoundsConfig;

        [Header("Platform")]
        [SerializeField] private PlatformConfig         platformConfig;
        [SerializeField] private Slider                 movementPlatformSlider;

        [Header("Ball")]
        [SerializeField] private BallConfig             ballConfig;
        [SerializeField] private Collider2D             bottomLoseTrigger;
        [SerializeField] private Transform              ballSpawnPoint;

        [Header("Bricks")]
        [SerializeField] private LevelBricksConfig      levelBricksConfig;
        [SerializeField] private BrickView              brickPrefab;
        [SerializeField, Min(1)] private int            brickInitialPoolSize = 40;

        [Header("Brick Effects")]
        [SerializeField] private BrickDestroyEffectView brickDestroyEffectPrefab;
        [SerializeField, Min(1)] private int            brickDestroyEffectInitialPoolSize = 20;

        [Header("HUD References")]
        [SerializeField] private TextMeshProUGUI        scoreText;

        [Header("Win Window")]
        [SerializeField] private Image                  winWindow;
        [SerializeField] private TextMeshProUGUI        winScoreText;
        [SerializeField] private Button                 winRepeatButton;

        [Header("Lose Window")]
        [SerializeField] private Image                  loseWindow;
        [SerializeField] private TextMeshProUGUI        loseScoreText;
        [SerializeField] private Button                 loseRestartButton;

        [Header("Audio")]
        [SerializeField] private GameplayAudioConfig    gameplayAudioConfig;
        [SerializeField] private AudioSource            sfxAudioSource;
        
        public override void InstallBindings()
        {
            if (mainCamera == null)
            {
                mainCamera = Camera.main;
            }

            Container.Bind<Camera>().FromInstance(mainCamera).AsSingle();

            Container.Bind<PlatformConfig>().FromInstance(platformConfig).AsSingle();
            Container.Bind<Slider>().FromInstance(movementPlatformSlider).AsSingle();

            Container.Bind<BallConfig>().FromInstance(ballConfig).AsSingle();
            Container.Bind<Collider2D>().FromInstance(bottomLoseTrigger).AsSingle();
            Container.Bind<Transform>().FromInstance(ballSpawnPoint).AsSingle();

            Container.Bind<WorldBounds>().AsSingle().WithArguments(worldBoundsConfig, mainCamera, topWall,
                bottomWall, leftWall, rightWall);

            Container.Bind<LevelBricksConfig>().FromInstance(levelBricksConfig).AsSingle();
            Container.Bind<BrickView>().FromInstance(brickPrefab).AsSingle();

            Container.Bind<GameSession>().AsSingle();

            GameHudUiReferences hud = new GameHudUiReferences(scoreText, winWindow, winScoreText, winRepeatButton,
                loseWindow, loseScoreText, loseRestartButton);
            Container.Bind<GameHudUiReferences>().FromInstance(hud).AsSingle();

            Container.Bind<GameplayAudioConfig>().FromInstance(gameplayAudioConfig).AsSingle();
            Container.Bind<AudioSource>().FromInstance(sfxAudioSource).AsSingle();
            Container.Bind<GameplayAudio>().AsSingle();


            Container.Bind<ObjectPool<BrickView>>().FromMethod(ctx => new ObjectPool<BrickView>(brickPrefab,
                null, brickInitialPoolSize)).AsSingle();

            Container.Bind<ObjectPool<BrickDestroyEffectView>>().FromMethod(ctx =>
                new ObjectPool<BrickDestroyEffectView>(brickDestroyEffectPrefab, null,
                    brickDestroyEffectInitialPoolSize)).AsSingle();
        }
    }
}