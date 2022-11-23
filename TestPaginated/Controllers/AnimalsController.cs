using Dapper;
using Dapper.Oracle;
using Oracle.ManagedDataAccess.Client;
using Microsoft.AspNetCore.Mvc;
using TestPaginated.Factory;
using TestPaginated.Models;

namespace TestPaginated.Factory;


    
    [Route("api/[controller]")]
    [ApiController]
    public class AnimalsController : ControllerBase
    {
        private readonly OracleConnection _connection;
        public AnimalsController(IConfiguration configuration)
        {
            _connection = new OracleSqlFactory(configuration).SqlConnection();
        }
        
        // GET: api/Animals
        [HttpGet]
        public ActionResult Get()
        {
            return Ok(_connection.Query<Animal>("select * from animals"));
            
            // return new string[] { "value1", "value2" };
        }

        // GET: api/Animals/5
        [HttpGet("{id}", Name = "Get")]
        public ActionResult Get(int id)
        {
            // return "value";

            var dynamicParameters = new OracleDynamicParameters();
            dynamicParameters.Add("id", id);

            var result = _connection.QueryFirstOrDefault<Animal>("select * from animals where id = :id", dynamicParameters);

            if (result == null)
            {
                return NotFound();
            }
            
            return Ok(result);
        }

        // POST: api/Animals
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/Animals/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/Animals/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }

