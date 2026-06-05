using Microsoft.EntityFrameworkCore;
using System.Text;

namespace InstaClone.Infrastructure.Settings;

internal static class ModelBuilderExtensions
{
    public static void UseSnakeCaseNamingConvention(this ModelBuilder modelBuilder)
    {
        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            var currentTableName = entityType.GetTableName();
            if (!string.IsNullOrEmpty(currentTableName))
            {
                entityType.SetTableName(ToSnakeCase(currentTableName));
            }

            foreach (var property in entityType.GetProperties())
            {
                var currentColumnName = property.GetColumnName() ?? property.Name;
                property.SetColumnName(ToSnakeCase(currentColumnName));
            }

            foreach (var index in entityType.GetIndexes())
            {
                var dbName = index.GetDatabaseName();
                if (!string.IsNullOrEmpty(dbName))
                    index.SetDatabaseName(ToSnakeCase(dbName));
            }

            foreach (var key in entityType.GetKeys())
            {
                var name = key.GetName();
                if (!string.IsNullOrEmpty(name))
                    key.SetName(ToSnakeCase(name));
            }
        }
    }

    private static string ToSnakeCase(string name)
    {
        if (string.IsNullOrEmpty(name))
            return name;

        var sb = new StringBuilder();
        var previousCategoryIsUpper = false;
        for (int i = 0; i < name.Length; i++)
        {
            var c = name[i];
            if (char.IsUpper(c))
            {
                if (i > 0 && !previousCategoryIsUpper)
                    sb.Append('_');
                sb.Append(char.ToLowerInvariant(c));
                previousCategoryIsUpper = true;
            }
            else
            {
                sb.Append(c);
                previousCategoryIsUpper = false;
            }
        }
        return sb.ToString();
    }
}