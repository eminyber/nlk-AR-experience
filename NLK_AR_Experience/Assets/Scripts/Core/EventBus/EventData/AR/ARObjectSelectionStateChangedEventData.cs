using System.Collections.Generic;

using UnityEngine;

public struct ARObjectSelectionStateChangedEventData
{
    public readonly Transform SelectedARObject;

    public ARObjectSelectionStateChangedEventData(Transform selectedObject)
    {
        SelectedARObject = selectedObject;
    }
}