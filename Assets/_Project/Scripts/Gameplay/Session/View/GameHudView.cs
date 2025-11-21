using UnityEngine;
using Zenject;

namespace MiniIT.ARKANOID
{
    public class GameHudView : MonoBehaviour
    {
        private GameHudPresenter presenter;

        [Inject]
        private void Construct(GameSession gameSession, GameHudUiReferences ui, GameplayAudio gameplayAudio)
        {
            GameHudAnimator animator = new GameHudAnimator();
            presenter = new GameHudPresenter(gameSession, ui, gameplayAudio, animator);
        }

        private void Awake()
        {
            presenter.Initialize();
        }

        private void OnEnable()
        {
            presenter.Subscribe();
        }

        private void OnDisable()
        {
            presenter.Unsubscribe();
        }
    }
}