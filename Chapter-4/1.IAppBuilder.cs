public interface IAppBuilder
{
    IDictionary<string, object> Proeprties { get; }
    
    object Build(Type returnType);
    IAppBuilder New();
    IAppBuilder Use(object middleware, params object[] args);
}
