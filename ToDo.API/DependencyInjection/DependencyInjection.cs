using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ToDo.API.Databases;
using ToDo.API.Repositories;
using ToDo.API.Services;

namespace ToDo.API.DependencyInjection
{
    public static class DependencyInjection
    {
        public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            services.AddDbContext<TodoDbContext>(options =>
                options.UseNpgsql(connectionString));


            services.AddTransient<ITodoService, TodoService>();
            services.AddTransient<ITodoRepository, TodoRepository>();

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        }
    }
}
