using System;

namespace Cartography.Runtime
{
    public interface IMappingContext
    {
        object Get(Type type);
        T Get<T>() where T : class;
        void Set<T>(T value) where T : class;
        void Set(Type type, object value);
        bool Has<T>() where T : class;
        bool Has(Type type);
    }
}