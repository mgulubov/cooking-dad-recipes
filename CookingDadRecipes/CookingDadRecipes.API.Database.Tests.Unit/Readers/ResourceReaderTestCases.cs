namespace CookingDadRecipes.API.Database.Tests.Unit.Readers
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using CookingDadRecipes.API.Database.Interfaces;

    [TestClass]
    public abstract class ResourceReaderTestCases
    {
        protected const string ValueA = "VALUE_A";
        protected const string ValueB = "VALUE_B";
        protected const string ValueC = "VALUE_C";
        protected const string NotFoundValue = "NOT_FOUND_VALUE";

        protected IResourceReader resourceReader = default;

        protected abstract IResourceReader GetResourceReader();

        [TestInitialize]
        public void TestInit()
        {
            this.resourceReader = this.GetResourceReader();
        }

        [TestMethod]
        public void TestReadReturnsCorrectValueValidResource()
        {
            string result = this.resourceReader.Read(ValueA);

            Assert.IsNotNull(result);
            Assert.AreEqual(ValueA, result);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestReadThrowsExceptionNullResource()
        {
            string result = this.resourceReader.Read(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestReadThrowsExceptionEmptyResource()
        {
            string result = this.resourceReader.Read(string.Empty);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestReadThrowsExceptionNotFoundResource()
        {
            string result = this.resourceReader.Read(NotFoundValue);
        }
    }
}