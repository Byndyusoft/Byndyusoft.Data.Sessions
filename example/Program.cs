using Byndyusoft.Data.Sessions.Example.Ef;
using Byndyusoft.Data.Sessions.Example.Nh;
using FluentMigrator;
using FluentMigrator.Runner;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using Byndyusoft.Data.Sessions.Example.Dapper;
using Microsoft.Data.Sqlite;

namespace Byndyusoft.Data.Sessions.Example
{
    [Migration(202012291833)]
    public class Migration : ForwardOnlyMigration
    {
        public override void Up()
        {
            Create.Table("peoples")
                .WithColumn("id").AsInt32().NotNullable().Identity().PrimaryKey("peoples_pk")
                .WithColumn("name").AsString(100).NotNullable();
        }
    }

    public static class Program
    {
        public static async Task Main()
        {
            var files = new[] {"nh.db", "ef.db", "dapper.db"};

            foreach (var file in files)
            {
                File.Delete(file);
                await File.Create(file).DisposeAsync();

                var runner = new ServiceCollection()
                    .AddFluentMigratorCore()
                    .ConfigureRunner(rb => rb
                        .AddSQLite()
                        .WithGlobalConnectionString($"data source={file}")
                        .ScanIn(typeof(Program).Assembly).For.Migrations())
                    .AddLogging(log => log.AddFluentMigratorConsole().SetMinimumLevel(LogLevel.Warning))
                    .BuildServiceProvider()
                    .GetRequiredService<IMigrationRunner>();

                runner.MigrateUp();
            }

            var services = new ServiceCollection()
                .AddSessions()
                .AddRedisDataAccess("localhost")
                //.AddNhDataAccess("data source=nh.db")
                .AddEfDataAccess("data source=ef.db")
                .AddDapperDataAccess(SqliteFactory.Instance, "data source=dapper.db;pooling=false")
                .BuildServiceProvider();

            var sessionFactory = services.GetRequiredService<ISessionFactory>();
            //var nhRepository = services.GetRequiredService<NhPeopleRepository>();
            var efRepository = services.GetRequiredService<EfPeopleRepository>();
            var dapperRepository = services.GetRequiredService<DapperPeopleRepository>();

            {
                await using var writeSession = sessionFactory.CreateCommitableSession();

                //await nhRepository.AddAsync(1, "nh_people_1");
                //await nhRepository.AddAsync(2, "nh_people_2");

                await efRepository.AddAsync(1, "ef_people_1");
                await efRepository.AddAsync(2, "ef_people_2");

                await dapperRepository.AddAsync(1, "dapper_people_1");
                await dapperRepository.AddAsync(2, "dapper_people_2");

                await writeSession.CommitAsync();
            }

            {
                await using var failedSession = sessionFactory.CreateCommitableSession();

                //await nhRepository.AddAsync(3, "nh_people_3");
                await efRepository.AddAsync(3, "ef_people_3");
                await dapperRepository.AddAsync(3, "dapper_people_3");

                await failedSession.RollbackAsync();
            }

            {
                await using var readSession = sessionFactory.CreateSession();

                //Console.WriteLine("== NHibernare ==");
                //await foreach (var people in nhRepository.ListAll())
                //    Console.WriteLine(JsonSerializer.Serialize(people));
                
                Console.WriteLine("== EntityFramework ==");
                await foreach (var people in efRepository.ListAll())
                    Console.WriteLine(JsonSerializer.Serialize(people));

                Console.WriteLine("== Dapper ==");
                await foreach (var people in dapperRepository.ListAll())
                    Console.WriteLine(JsonSerializer.Serialize(people));
            }

            foreach (var file in files)
            {
                File.Delete(file);
            }
        }
    }
}