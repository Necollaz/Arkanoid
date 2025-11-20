using UnityEngine;
using Zenject;

namespace MiniIT.ARKANOID
{
    public class WorldBoundsView : MonoBehaviour
    {
        private WorldBounds _worldBounds;

        [Inject]
        private void Construct(WorldBounds worldBounds)
        {
            _worldBounds = worldBounds;
        }

        private void Start()
        {
            _worldBounds?.ForceUpdate();
        }
    }
}