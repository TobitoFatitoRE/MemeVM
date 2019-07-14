using System;

namespace MemeVM.Runtime
{
    public static class Entry {
        public static T Run<T>(RuntimeTypeHandle handle, int index, object[] parameters) {
            var result = Dispatcher.Run(Type.GetTypeFromHandle(handle).Assembly, index, parameters);

            if (result is NoMoreStackItem)
                return default(T);

            if (typeof(T).IsEnum)
                return (T)Enum.ToObject(typeof(T), result);

            return (T)Convert.ChangeType(result, typeof(T));
        }
    }
}
