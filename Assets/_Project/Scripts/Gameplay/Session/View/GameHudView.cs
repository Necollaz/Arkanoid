using UnityEngine;
using Zenject;

namespace MiniIT.ARKANOID
{
    public class GameHudView : MonoBehaviour
    {
        private GameHudPresenter _presenter;

        [Inject]
        private void Construct(GameSession gameSession, GameHudUiReferences ui, GameplayAudio gameplayAudio)
        {
            GameHudAnimator animator = new GameHudAnimator();
            _presenter = new GameHudPresenter(gameSession, ui, gameplayAudio, animator);
        }

        private void Awake()
        {
            _presenter.Initialize();
        }

        private void OnEnable()
        {
            _presenter.Subscribe();
        }

        private void OnDisable()
        {
            _presenter.Unsubscribe();
        }
    }
}