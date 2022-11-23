using System.Data;
using Oracle.ManagedDataAccess.Client;

namespace TestPaginated.Factory;

public class OracleSqlFactory
{
    private readonly string? _connectionString;
    
    public OracleSqlFactory(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("OracleConnection");
    }
    
    public OracleConnection SqlConnection()
    {
        return new OracleConnection(_connectionString);
    }
}