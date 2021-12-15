namespace CookingDadRecipes.API.Database.Tests.Unit.Migrations
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using CookingDadRecipes.API.Database.Interfaces;
    using System.Data.Common;
    using Microsoft.Data.Sqlite;

    [TestClass]
    public abstract class DatabaseMigrationTestCase
    {
        protected const string ValueA = "VALUE_A";
        protected const string ValueB = "VALUE_B";
        protected const string ValueC = "VALUE_C";

        protected IDatabaseMigration databaseMigration = default;
        protected DbConnection dbConnection = default;

        protected abstract IDatabaseMigration GetDatabaseMigration();

        [TestInitialize]
        public void TestInit()
        {
            this.databaseMigration = this.GetDatabaseMigration();
            this.dbConnection = CreateInMemoryDatabase();
        }

        [TestCleanup]
        public void TestCleanup()
        {
            this.dbConnection.Close();
            this.dbConnection.Dispose();
        }

        [TestMethod]
        public void TestBaseDirectoryReturnsCorrectValue()
        {
            string result = this.databaseMigration.BaseDirectory;

            Assert.IsNotNull(result);
            Assert.AreEqual(ValueA, result);
        }

        protected static DbConnection CreateInMemoryDatabase()
        {
            SqliteConnection connection = new("Filename=:memory:");
            connection.Open();

            return connection;
        }
    }
}