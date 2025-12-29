namespace NLKARExperience.Core.EventBus.EventData.AR
{
    public readonly struct SelectSpawnTypeRequestEventData
    {
        public readonly int Index;
        
        public SelectSpawnTypeRequestEventData(int index)
        {
            Index = index;
        }
    }
}