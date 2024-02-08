using BloodDonationAPI.Data;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

namespace BloodDonationAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            string? connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

            //authentication

            // Add services to the container.

            //    builder.Services.AddControllers();
            builder.Services.AddControllers();
            //builder.Services.AddTransient<Seed>();
            builder.Services.AddControllers().AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
                    //options.JsonSerializerOptions.MaxDepth = 256;
                    //options.JsonSerializerOptions.WriteIndented = true;
                });
            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
/*            builder.Services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
            });*/

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<iDonorDbContext>(options => options.UseSqlServer(connectionString));
            //builder.Services.AddDbContext<iDonorDbContext>(options => options.UseSqlServer(connectionString));

            builder.Services.AddCors(options =>
            {
                options.AddDefaultPolicy(policy =>
                {
                    policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
                });
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseCors();

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}