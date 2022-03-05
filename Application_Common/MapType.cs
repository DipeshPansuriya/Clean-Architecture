using System;

namespace Application_Common
{
    public class MapItem
    {
        public Type Type { get; private set; }
        public DataRetriveTypeEnum DataRetriveType { get; private set; }
        public string PropertyName { get; private set; }

        public MapItem(Type type, DataRetriveTypeEnum dataRetriveType, string propertyName)
        {
            Type = type;
            DataRetriveType = dataRetriveType;
            PropertyName = propertyName;
        }
    }

    public enum DataRetriveTypeEnum
    {
        ToList,
        FirstOrDefault
    }
}