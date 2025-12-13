namespace NLKARExperience.Core.EventBus.EventData.AR
{
    public readonly struct ARObjectToSpawnChangeRequestedEventData
    {
        public readonly int Index;
        
        public ARObjectToSpawnChangeRequestedEventData(int index)
        {
            Index = index;
        }
    }
}