using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using RazorPagesMovie.Models;

public class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        

        // Add services to the container.
        builder.Services.AddRazorPages();
        builder.Services.AddDbContext<RazorPagesMovieContext>(options =>
        options.UseSqlite(builder.Configuration.GetConnectionString("RazorPagesMovieContext") ?? throw new InvalidOperationException("Connection string 'RazorPagesMovieContext' not found.")));



       builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
      builder.Services.AddEndpointsApiExplorer();
      builder.Services.AddSwaggerGen(c => {
        c.SwaggerDoc("v1",
        new() {Title="Keith's Api" , Version="v1"});
      });

        var app = builder.Build();

        using (var scope = app.Services.CreateScope())
        {
            var services = scope.ServiceProvider;

            SeedData.Initialize(services);
        }


        // Configure the HTTP request pipeline.
      if (!app.Environment.IsDevelopment())
        {
           app.UseExceptionHandler("/Error");
          // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
          app.UseHsts();
        app.UseSwagger();
          app.UseSwaggerUI(c => c.SwaggerEndpoint(
            "/swagger/v1/swagger.json",
            "v1"
          ));
       }

       
        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthorization();

        app.MapRazorPages();
        app.MapControllers();

        app.Run();
    }
}