using FluentMigrator;

namespace CookingDadRecipes.API.Database.Interfaces
{
    public interface IDatabaseMigration : IMigration
    {
        string BaseDirectory { get; }

        void Up();

        void Down();
    }
}