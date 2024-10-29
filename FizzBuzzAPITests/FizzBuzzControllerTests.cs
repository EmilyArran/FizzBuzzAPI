using Application;
using FizzBuzzAPI16.Controllers;
using FizzBuzzDomain.Models;
using FizzBuzzDomain.Repository;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace FizzBuzzAPI16Tests;

public class FizzBuzzControllerTests {
	[Fact]
	public async Task GetAllFizzBuzzRulesCallsAndReturnsResults() {
		var fizzBuzzRepo = new Mock<IFizzBuzzRepo>();
		var rules = new List<Rules2>();
		fizzBuzzRepo.Setup(x => x.GetAllRules()).ReturnsAsync(rules);
		var sut = new FizzBuzzController(fizzBuzzRepo.Object, Mock.Of<IRuleManager>(), Mock.Of<IGenerateDefaultRules>());

		var result = await sut.GetAllRules() as OkObjectResult;

		result.Should().BeOfType<OkObjectResult>();
		result.Should().NotBeNull();
		result.Value.Should().Be(rules);
	}

	[Fact]
	public async Task RunFizzBUzzCallRuleManagerAndReturnResults() {
		var fizzBuzzRepo = new Mock<IFizzBuzzRepo>();
		var ruleManager = new Mock<IRuleManager>();
		var rules = new List<Rules2>();
		fizzBuzzRepo.Setup(x => x.GetAllRules()).ReturnsAsync(rules);
		ruleManager.Setup(x => x.Execute(1, 100, rules)).Returns("FizzBuzz");
		var sut = new FizzBuzzController(fizzBuzzRepo.Object, ruleManager.Object, Mock.Of<IGenerateDefaultRules>());

		var result = await sut.RunFizzBuzz(1, 100) as OkObjectResult;

		result.Should().BeOfType<OkObjectResult>();
		result.Should().NotBeNull();
		result.Value.Should().Be("FizzBuzz");
	}

	[Fact]
	public async Task CreateRuleCallFizzBuzzRepoReturnResults() {
		var fizzBuzzRepo = new Mock<IFizzBuzzRepo>();
		var testRule = new Rules2() {
			Name = "TestRule",
			Execution_Order = 1,
			Modulus = 2
		};
		fizzBuzzRepo.Setup(x => x.CreateRule(testRule));
		var sut = new FizzBuzzController(fizzBuzzRepo.Object, Mock.Of<IRuleManager>(), Mock.Of<IGenerateDefaultRules>());

		var result = await sut.CreateRule(testRule);
		
		fizzBuzzRepo.Verify(x => x.CreateRule(testRule));
		result.Should().BeOfType<OkResult>();
	}

	[Fact]
	public async Task DeleteAllRulesCallFizzBuzzRepoReturnOkResult() {
		var fizzBuzzRepo = new Mock<IFizzBuzzRepo>();
		fizzBuzzRepo.Setup(x => x.DeleteRules());
		var sut = new FizzBuzzController(fizzBuzzRepo.Object, Mock.Of<IRuleManager>(), Mock.Of<IGenerateDefaultRules>());

		var result = await sut.DeleteRules();
		
		fizzBuzzRepo.Verify(x => x.DeleteRules(), Times.Once);
		result.Should().BeOfType<OkResult>();
	}

	[Fact]
	public async Task UpdateRuleCallsFizzBuzzRepoReturnsOkResult() {
		var fizzBuzzRepo = new Mock<IFizzBuzzRepo>();
		var sut = new FizzBuzzController(fizzBuzzRepo.Object, Mock.Of<IRuleManager>(), Mock.Of<IGenerateDefaultRules>());
		var rule = new Rules2() {
			Id = 15,
			Name = "Emily",
			Modulus = 3,
			Execution_Order = 15
		};

		var result = await sut.UpdateRule(rule);
		fizzBuzzRepo.Verify(x => x.UpdateRule(rule), Times.Once);
		result.Should().BeOfType<OkResult>();
	}

	[Fact]
	public async Task AddDefaultRulesCallFizzBuzzRepoAndReturnOkResult() {
		var fizzBuzzRepo = new Mock<IFizzBuzzRepo>();
		var generateRules = new Mock<IGenerateDefaultRules>();
		var rules = new List<Rules2>();
		generateRules.Setup(x => x.Execute()).Returns(rules);

		var sut = new FizzBuzzController(fizzBuzzRepo.Object, Mock.Of<IRuleManager>(), generateRules.Object);

		var result = await sut.AddDefaultRules();
		fizzBuzzRepo.Verify(x => x.AddDefaultRules(rules), Times.Once);
		result.Should().BeOfType<OkResult>();
	}
}