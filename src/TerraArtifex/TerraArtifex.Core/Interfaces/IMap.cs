namespace TerraArtifex.Core.Interfaces
{
    public interface IMap
    {
        void AddLayer(ILayer layer);
        T GetLayer<T>() where T : ILayer;
    }
}
