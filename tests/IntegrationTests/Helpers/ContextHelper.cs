using AutoFixture;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace IntegrationTests.Helpers
{
    public static class ContextHelper
    {
        public static Domain.Entities.Task SeedTask(ApplicationDbContext context)
        {
            Fixture _autoFixture = new Fixture();
            var task = _autoFixture.Create<Domain.Entities.Task>();
            task.Id = 0;
            context.Tasks.Add(task);
            context.SaveChanges();

            context.Entry(task).State = EntityState.Detached;

            return task;
        }
    }
}
