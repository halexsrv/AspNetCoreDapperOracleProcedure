using System.Data;
using Dapper;
using Dapper.Oracle;
using Microsoft.AspNetCore.Mvc;
using Oracle.ManagedDataAccess.Client;

namespace TestPaginated.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EmployeesController
{
    private readonly string? _connectionString;
    
    public EmployeesController(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("OracleConnection");
    }
    
    [HttpGet]
    public IEnumerable<dynamic> Get()
    {
        using (var connection = new OracleConnection(_connectionString))
        {
            return connection.Query("select * from people");
        }
        
        // return "employees";
    }

    [HttpGet("Procedure")]
    public IEnumerable<dynamic> GetProcedure(int id)
    {
        using (var connection = new OracleConnection(_connectionString))
        {
            var procedure = "GetEmpPckg.GetEmp";
            
            // var values = new { p_dep = 20 };
            
            // var dynamicParameters = new DynamicParameters();
            var dynamicParameters = new OracleDynamicParameters();
            
            // dynamicParameters.AddDynamicParams(values);
            
            dynamicParameters.Add("p_dep", id);
            dynamicParameters.Add("p_ref", dbType: OracleMappingType.RefCursor, direction: ParameterDirection.Output);
            
            var results = connection.Query(procedure, dynamicParameters, commandType: CommandType.StoredProcedure).ToList();

            return results;
        }
    }
}
