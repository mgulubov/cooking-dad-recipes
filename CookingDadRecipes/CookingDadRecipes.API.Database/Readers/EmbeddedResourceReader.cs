namespace CookingDadRecipes.API.Database.Readers
{
    using System;
    using System.Reflection;

    using CookingDadRecipes.API.Database.Interfaces;

    public class EmbeddedResourceReader : IResourceReader
    {
        private readonly Assembly assembly;

        public EmbeddedResourceReader(Assembly assembly)
        {
            this.assembly = assembly;
        }

        public string Read(string resource)
        {
            using Stream resourceStream = this.GetStreamFor(resource);
            using StreamReader reader = new StreamReader(resourceStream);

            return reader.ReadToEnd();
        }

        private Stream GetStreamFor(string resourceName)
        {
            this.ValidateResource(resourceName);
            resourceName = this.FormatResourceName(resourceName);

            return this.assembly.GetManifestResourceStream(resourceName);
        }

        private void ValidateResource(string resourceName)
        {
            if (string.IsNullOrEmpty(resourceName))
            {
                throw new ArgumentNullException(nameof(resourceName), "Cannot be null or empty");
            }

            if (this.assembly.GetManifestResourceInfo(resourceName) == default)
            {
                throw new ArgumentOutOfRangeException(nameof(resourceName), "Does not exist");
            }
        }

        private string FormatResourceName(string resourceName)
        {
            return this.assembly.GetName().Name + "." + resourceName.Replace(" ", "_")
                       .Replace("\\", ".")
                       .Replace("/", ".");
        }
    }
}
