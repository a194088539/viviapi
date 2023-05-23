namespace viviapi.Cache
{
    using System;

    public interface ICacheStrategy
    {
        void AddObject(string objId, object o);
        void AddObject(int hashCode, string objId, object o);
        void AddObject(string objId, object o, bool saved);
        void AddObject(string objId, object o, int expires);
        void AddObject(int hashCode, string objId, object o, bool saved);
        void AddObject(string objId, object o, int expires, bool saved);
        void RemoveObject(string objId);
        void RemoveObject(int hashCode, string objId);
        void RemoveObject(string objId, bool saved);
        object RetrieveObject(string objId);
        object RetrieveObject(int hashCode, string objId);
        object RetrieveObject(string objId, Type type, bool saved);
        object RetrieveObject(int hashCode, string objId, Type type, bool saved);

        int TimeOut { get; set; }
    }
}

