using FizzBuzzDomain.Models;

namespace Application;

public class RuleManager : IRuleManager {
	public string Execute(int firstNumber, int secondNumber, IEnumerable<Rules2> rules) {
		var result = "";
		for (int currentNumber = firstNumber; currentNumber <= secondNumber; currentNumber++) {
			var ruleMatched = false;
			foreach (var rule in rules) {
				if (currentNumber % rule.Modulus == 0) {
					ruleMatched = true;
					result += rule.Name;
					if (currentNumber != secondNumber) {
						result += Environment.NewLine;
					}
					break;
				}
			}

			if (! ruleMatched) {
				result += currentNumber;
				if (currentNumber != secondNumber) {
					result += Environment.NewLine;
				}
			}
		}

		return result;
	}
}