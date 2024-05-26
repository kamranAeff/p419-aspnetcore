using WebIntroEmpty.Models.Entities;

namespace WebIntroEmpty.Models.Contexts
{
    public static class DataSeed
    {
        internal static IApplicationBuilder Seed(this IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<DataContext>();

                var list = new[] {
                        new Student { Id = 1, Name = "Aqil", Surname = "Abbasov", GroupNo = "P622" },
                        new Student { Id = 2, Name = "Emin", Surname = "Zeynalov", GroupNo = "P622" },
                        new Student { Id = 3, Name = "Taleh", Surname = "Cabbarli", GroupNo = "P622" },
                        new Student { Id = 4, Name = "Huseyn", Surname = "Sukurov", GroupNo = "P622" }
                };

                db.Students.AddRange(list);
                db.SaveChanges();
            }

            return app;
        }
    }
}
