namespace CqrsPipeline;

public interface IDependencyResolver
{
    TType Resolve<TType>();
    object Resolve(Type type);
}