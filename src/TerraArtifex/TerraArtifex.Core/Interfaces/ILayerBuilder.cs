namespace TerraArtifex.Core.Interfaces
{
    public interface ILayerBuilder
    {
        ILayerBuilderResult Execute(IMap map);
    }
}
