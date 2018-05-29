using System;
using Umbraco.Core;
using Umbraco.Core.Models.PublishedContent;
using Umbraco.Core.PropertyEditors;
using Umbraco.Web;

namespace Sum.Umbraco.App_Plugins.SumNullablePropEditors
{
    public class NullableNumberPropertyConverter : IPropertyValueConverterMeta
    {
        public bool IsConverter(PublishedPropertyType propertyType)
        {
            return propertyType.PropertyEditorAlias == "sum.NullableNum";
        }

        public object ConvertDataToSource(PublishedPropertyType propertyType, object source, bool preview)
        {
            var attempt = source.TryConvertTo<decimal?>();
            if (attempt.Success)
            {
                return attempt.Result;
            }

            return null;
        }

        public object ConvertSourceToObject(PublishedPropertyType propertyType, object source, bool preview)
        {

            if (source == null || UmbracoContext.Current == null)
            {
                return null;
            }
            return ConvertDataToSource(propertyType, source, preview);
        }

        public object ConvertSourceToXPath(PublishedPropertyType propertyType, object source, bool preview)
        {
            return source.ToString();
        }

        public Type GetPropertyValueType(PublishedPropertyType propertyType)
        {
            return typeof(decimal?);
        }

        public PropertyCacheLevel GetPropertyCacheLevel(PublishedPropertyType propertyType, PropertyCacheValue cacheValue)
        {
            PropertyCacheLevel returnLevel;
            switch (cacheValue)
            {
                case PropertyCacheValue.Object:
                    returnLevel = PropertyCacheLevel.ContentCache;
                    break;
                case PropertyCacheValue.Source:
                    returnLevel = PropertyCacheLevel.Content;
                    break;
                case PropertyCacheValue.XPath:
                    returnLevel = PropertyCacheLevel.Content;
                    break;
                default:
                    returnLevel = PropertyCacheLevel.None;
                    break;
            }

            return returnLevel;
        }

    }
}