﻿using CourseSales.Catalog.Api.Options;
using MongoDB.Driver;

namespace CourseSales.Catalog.Api.Repositories
{
    public static class RepositoryExt
    {
        public static IServiceCollection AddDataBaseServiceExt(this IServiceCollection services) {

            services.AddSingleton<IMongoClient, MongoClient>(sp =>
            {
                var options = sp.GetRequiredService<MongoOption>();
                return new MongoClient(options.ConnectionString);
            });
            services.AddScoped<AppDbContext>(sp =>
            {
                var mongoClient = sp.GetRequiredService<IMongoClient>();
                var options = sp.GetRequiredService<MongoOption>();
                return AppDbContext.Create(mongoClient.GetDatabase(options.DataBase));
            });

            return services;

        }
    }
}
