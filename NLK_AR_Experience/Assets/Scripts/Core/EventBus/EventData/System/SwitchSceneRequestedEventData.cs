using NLKARExperience.Core.Models;

namespace NLKARExperience.Core.EventBus.EventData.System
{
    public readonly struct SwitchSceneRequestedEventData
    {
        public AppScene SceneToLoad { get; }

        public SwitchSceneRequestedEventData(AppScene newAppScene)
        {
            SceneToLoad = newAppScene;
        }
    }
}