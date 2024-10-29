using FizzBuzzDomain.Models;

namespace Application;

public interface IRuleManager {
	string Execute(int firstNumber, int secondNumber, IEnumerable<Rules2> rules);
}