using SimpleInjector;

namespace DXServicesDemo
{
    public class IoC
    {
        public static Container Instance { get; }
        
        static IoC() => Instance = new Container();

        public static T Get<T>() where T : class => Instance.GetInstance<T>();
    }
}