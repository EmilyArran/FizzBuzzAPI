using Dapper;
using FizzBuzzDomain.Models;

namespace FizzBuzzDomain.Repository;

public class FizzBuzzRepo : IFizzBuzzRepo {
	readonly IDapperContext context;

	public FizzBuzzRepo(IDapperContext context) {
		this.context = context;
	}

	public async Task<IEnumerable<Rules2>> GetAllRules() {
		var query = "SELECT * FROM Rules2";

		using (var connection = context.CreateConnection()) {
			var rules = await connection.QueryAsync<Rules2>(query);
			return rules.ToList();
		}
	}

	public async Task CreateRule(Rules2 rule) {
		var query = "INSERT INTO Rules2 "
		            + "VALUES (@Name, @Execution_Order, @Modulus)";
		using (var connection = context.CreateConnection()) {
			await connection.ExecuteAsync(query, rule);
		}
	}

	public async Task DeleteRules() {
		var query = "DELETE FROM Rules2";
		using (var connection = context.CreateConnection()) {
			await connection.ExecuteAsync(query);
		}
	}

	public async Task UpdateRule(Rules2 rule) {
		var query = "UPDATE Rules2 SET [name] = @Name, execution_order = @Execution_Order, modulus = @Modulus WHERE id = @Id";
		using (var connection = context.CreateConnection()) {
			await connection.ExecuteAsync(query, rule);
		}
	}

	public async Task AddDefaultRules(List<Rules2> rules) {
		foreach (var rule in rules) {
			var query = "INSERT INTO Rules2"
			            + "VALUES (@Name, @Execution_Order, @Modulus)";
			using (var connection = context.CreateConnection()) {
				await connection.ExecuteAsync(query, rule);
			}
		}
	}
}