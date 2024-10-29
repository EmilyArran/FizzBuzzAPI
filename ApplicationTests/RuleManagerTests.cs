using Application;
using FizzBuzzDomain.Models;
using FluentAssertions;

namespace ApplicationTests;

public class RuleManagerTests {

	[Fact]
	public void FirstRuleMatchesSoSecondRuleDoesntRun() {
		var sut = new RuleManager();
		var rules = new List<Rules2>();
		var rule1 = new Rules2() {
			Id = 1,
			Modulus = 2,
			Execution_Order = 1,
			Name = "TwoRule"
		};
		var rule2 = new Rules2() {
			Id = 2,
			Modulus = 3,
			Execution_Order = 2,
			Name = "ThreeRule"
		};
		rules.Add(rule1);
		rules.Add(rule2);

		var result = sut.Execute(1, 6, rules);

		result.Should().Be($"1{Environment.NewLine}TwoRule{Environment.NewLine}ThreeRule{Environment.NewLine}TwoRule{Environment.NewLine}5{Environment.NewLine}TwoRule");
	}
	
}