using WebApi.DBOperations;
using WebApi.Entities;

namespace TestSetup
{
    public static class Authors
    {
        public static void AddAuthors(this BookStoreDbContext context)
        {
            context.Authors.AddRange(
                new Author{ Name = "Eric", Surname = "Ries", BirthDate = new System.DateTime(1978,9,22) },
                new Author{ Name = "Charlotte Perkins", Surname = "Gilman", BirthDate = new System.DateTime(1860,7,3) },
                new Author{ Name = "Frank", Surname = "Herbert", BirthDate = new System.DateTime(1920,10,8) },
                new Author{ Name = "İvan Aleksandroviç", Surname = "Gonçarov", BirthDate = new System.DateTime(1812,6,18) }
            );            
        }
    }
}