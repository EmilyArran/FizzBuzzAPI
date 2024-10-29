using System.Data;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace FizzBuzzDomain;

public class DapperContext : IDapperContext {
	readonly IConfiguration configuration;
	readonly string connectionString;

	public DapperContext(IConfiguration configuration) {
		this.configuration = configuration;
		connectionString = configuration.GetConnectionString("SqlConnection") ?? "";
	}
	
	public void Dispose() {
	}

	public IDbConnection CreateConnection() => new SqlConnection(connectionString);
}