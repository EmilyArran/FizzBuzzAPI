using System.Data;

namespace FizzBuzzDomain;

public interface IDapperContext {
	public IDbConnection CreateConnection();
}