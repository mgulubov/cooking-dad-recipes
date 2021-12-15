namespace CookingDadRecipes.API.Database.Tests.Unit.Readers
{
    using System.IO;
    using System.Reflection;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;
    using Moq.AutoMock;

    using CookingDadRecipes.API.Database.Interfaces;
    using CookingDadRecipes.API.Database.Readers;
    using CookingDadRecipes.API.Database.Tests.Unit.Mocks;

    [TestClass]
    public class EmbeddedResourceReaderTestCases : ResourceReaderTestCases
    {
        protected override IResourceReader GetResourceReader()
        {
            AutoMocker mocker = new AutoMocker();
            mocker.CreateInstance<AssemblyMock>();
            
            mocker.Use<ManifestResourceInfo>(new ManifestResourceInfo(null, null, ResourceLocation.Embedded));

            SetupAssemblyGetName(mocker);
            SetupGetManifestResourceInfo(mocker);
            SetupGetManifestResourceStream(mocker);

            IResourceReader reader = new EmbeddedResourceReader(mocker.Get<AssemblyMock>());

            return reader;
        }

        private static void SetupGetManifestResourceInfo(AutoMocker mocker)
        {
            mocker
                .GetMock<AssemblyMock>()
                .Setup(a => a.GetManifestResourceInfo(It.Is<string>(s => s == ValueA)))
                .Returns(mocker.Get<ManifestResourceInfo>());
        }

        private static void SetupAssemblyGetName(AutoMocker mocker)
        {
            mocker.Use<AssemblyName>(new AssemblyName(ValueB));
            mocker
                .GetMock<AssemblyMock>()
                .Setup(a => a.GetName())
                .Returns(mocker.Get<AssemblyName>());
        }

        private static void SetupGetManifestResourceStream(AutoMocker mocker)
        {
            Stream stream = GetStreamFor(ValueA);
            string resourceName = FormatStreamResource(mocker);
            mocker
                .GetMock<AssemblyMock>()
                .Setup(a => a.GetManifestResourceStream(It.Is<string>(s => s == resourceName)))
                .Returns(stream);
        }

        private static string FormatStreamResource(AutoMocker mocker)
        {
            string assemblyName = mocker.Get<AssemblyMock>().GetName().ToString();
            string resourceName = $"{assemblyName}.{ValueA}";

            return resourceName;
        }

        private static Stream GetStreamFor(string value)
        {
            MemoryStream stream = new();
            StreamWriter writer = new (stream);
            writer.Write(value);
            writer.Flush();
            stream.Position = 0;

            return stream;
        }
    }
}
