namespace NLKARExperience.Core.Interfaces.Registries
{
    public interface IObjectRegistry<Tkey, Tvalue> 
    {
        bool TryGetObject(Tkey key, out Tvalue registeredObject);
    }
}