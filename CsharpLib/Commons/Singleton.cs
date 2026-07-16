using System;

namespace Vosiz.Commons
{
    public abstract class Singleton<T> where T : Singleton<T>
    {
        public static T Instance => ClassInstance.Value;

        private static readonly Lazy<T> ClassInstance = new Lazy<T>(() => (T)Activator.CreateInstance(typeof(T), true));

        // Constructor
        protected Singleton() { }
    }
}
