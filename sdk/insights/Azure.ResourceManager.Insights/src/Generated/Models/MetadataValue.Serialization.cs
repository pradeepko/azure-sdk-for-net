// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

// <auto-generated/>

#nullable disable

using System.Text.Json;
using Azure.Core;

namespace Azure.ResourceManager.Insights.Models
{
    public partial class MetadataValue
    {
        internal static MetadataValue DeserializeMetadataValue(JsonElement element)
        {
            Optional<LocalizableString> name = default;
            Optional<string> value = default;
            foreach (var property in element.EnumerateObject())
            {
                if (property.NameEquals("name"))
                {
                    name = LocalizableString.DeserializeLocalizableString(property.Value);
                    continue;
                }
                if (property.NameEquals("value"))
                {
                    value = property.Value.GetString();
                    continue;
                }
            }
            return new MetadataValue(name.Value, value.Value);
        }
    }
}
