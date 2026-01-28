using NLKARExperience.Core.EventBus.EventData.System;
using NLKARExperience.Core.Interfaces.Handlers;
using NLKARExperience.Core.Models;

using UnityEngine;

using UnityEngine.XR.ARFoundation;

using Logger = NLKARExperience.Core.Utils.Logger;

namespace NLKARExperience.System.Handlers
{
    public class TurnOfARInSceneHandler : MonoBehaviour, IEventHandler<TurnOffARInSceneRequestedEventData>
    {
        [SerializeField] ARSession ARSession;
        [SerializeField] Camera Camera;
        [SerializeField] ARCameraManager ARCameraManager;
        [SerializeField] ARCameraBackground ARCameraBackground;

        void Start()
        {
            if (ARSession == null)
            {
                Logger.Log(LogSeverityLevel.Error, $"Error: ARSession reference is null in {nameof(TurnOfARInSceneHandler)}");
                enabled = false;
                return;
            }

            if (Camera == null)
            {
                Logger.Log(LogSeverityLevel.Error, $"Error: Camera reference is null in {nameof(TurnOfARInSceneHandler)}");
                enabled = false;
                return;
            }

            if (ARCameraManager == null)
            {
                Logger.Log(LogSeverityLevel.Error, $"Error: ARCameraManager reference is null in {nameof(TurnOfARInSceneHandler)}");
                enabled = false;
                return;
            }

            if (ARCameraBackground == null)
            {
                Logger.Log(LogSeverityLevel.Error, $"Error: ARCameraBackground reference is null in {nameof(TurnOfARInSceneHandler)}");
                enabled = false;
                return;
            }
        }

        public void HandleEvent(TurnOffARInSceneRequestedEventData eventData)
        {
            if (!enabled) return;

            DisableAndResetCamera();
            DisableAndResetARSession();
        }

        private void DisableAndResetARSession()
        {
            ARSession.enabled = false;
            ARSession.Reset();
        }

        //Delete the artifact of the camera outside the safe area of the screen
        private void DisableAndResetCamera()
        {
            ARCameraBackground.enabled = false;

            ARCameraManager.enabled = false;

            Camera.clearFlags = CameraClearFlags.SolidColor;
            Camera.backgroundColor = Color.black;
        }
    }
}