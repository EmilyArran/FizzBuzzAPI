using FizzBuzzDomain.Models;

namespace FizzBuzzDomain.Repository;

public interface IFizzBuzzRepo {
	Task<IEnumerable<Rules2>> GetAllRules();

	Task CreateRule(Rules2 rule);

	Task DeleteRules();

	Task UpdateRule(Rules2 rule);

	Task AddDefaultRules(List<Rules2> rules);
}