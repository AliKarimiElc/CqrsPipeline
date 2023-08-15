namespace CqrsPipeline;

public interface IDependencyResolver
{
    TType Resolve<TType>();
    object Resolve(Type type);
}

public class DependencyResolver : IDependencyResolver
{
    public TType Resolve<TType>()
    {
        throw new NotImplementedException();
    }

    public object Resolve(Type type)
    {
        throw new NotImplementedException();
    }
}