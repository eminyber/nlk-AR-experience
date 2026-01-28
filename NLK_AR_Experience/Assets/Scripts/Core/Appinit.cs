using UnityEngine;

public class Appinit : MonoBehaviour
{
    void Start()
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 60;
    }
}
