using EFCore.DbContexts;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace EfcoreTests;

public class DbContextsFixutre : IDisposable
{
    readonly SqliteConnection _connection;
    private bool disposedValue;
    readonly OverallDbContext _overallDbContext;
    public DbContextsFixutre()
    {
        var optBuilder = new DbContextOptionsBuilder<OverallDbContext>();
        _connection = new SqliteConnection("Filename=:memory:");
        _connection.Open();
        //.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=coffeeapptestdb;Trusted_Connection=True;").Options;//
        var dbContextOpt = optBuilder.UseSqlite(_connection).Options;
        
        _overallDbContext = new OverallDbContext(dbContextOpt);
        _overallDbContext.Database.EnsureCreated();
    }
    

    

    public SqliteConnection SqliteConnection {get => _connection; }

    public OverallDbContext OverallDbContext {get => _overallDbContext; }

    protected virtual void Dispose(bool disposing)
    {
        if (!disposedValue)
        {
            if (disposing)
            {
                _overallDbContext.Database.EnsureDeleted();
                _overallDbContext.Dispose();
                _connection.Dispose();
            }

            // TODO: free unmanaged resources (unmanaged objects) and override finalizer
            // TODO: set large fields to null
            disposedValue = true;
        }
    }

    // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
    // ~DbContextsFixutre()
    // {
    //     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
    //     Dispose(disposing: false);
    // }

    public void Dispose()
    {
        // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }
}
