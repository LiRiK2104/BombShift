using System;
using Newtonsoft.Json;
using Shop.Items.Controlling;

namespace Shop.Items
{
    public class ItemConverter : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (value == null)
                writer.WriteNull();
            else if (value is Item item)
                writer.WriteValue(item.Id);
            else
                throw new InvalidOperationException("Unhandled case for ItemConverter. Check to see if this converter has been applied to the wrong serialization type.");
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            switch (reader.TokenType)
            {
                case JsonToken.Integer:
                    if (int.TryParse(reader.Value.ToString(), out int id) && 
                        ItemsDataBase.Instance.Core.TryGetItem(id, out Item item))
                        return item;
                    else
                        return null;
                
                case JsonToken.Null:
                    return null;
                
                default:
                    throw new InvalidOperationException("Unhandled case for ItemConverter. Check to see if this converter has been applied to the wrong serialization type.");
            }
        }

        public override bool CanConvert(Type objectType) => objectType == typeof(Item);
    }
}
