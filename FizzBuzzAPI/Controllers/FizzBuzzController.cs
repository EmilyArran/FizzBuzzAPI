using System.Runtime.InteropServices;
using Application;
using FizzBuzzDomain.Models;
using FizzBuzzDomain.Repository;
using Microsoft.AspNetCore.Mvc;

namespace FizzBuzzAPI16.Controllers;

public class FizzBuzzController : ControllerBase {
	readonly IFizzBuzzRepo fizzBuzzRepo;
	readonly IRuleManager ruleManager;
	readonly IGenerateDefaultRules generateDefaultRules;
	
	public FizzBuzzController(IFizzBuzzRepo fizzBuzzRepo, IRuleManager ruleManager, IGenerateDefaultRules generateDefaultRules) {
		this.fizzBuzzRepo = fizzBuzzRepo;
		this.ruleManager = ruleManager;
		this.generateDefaultRules = generateDefaultRules;
	}

	[HttpGet]
	[Route("GetAllRules")]
	public async Task<OkObjectResult> GetAllRules() {
		var rules = await fizzBuzzRepo.GetAllRules();
		return new OkObjectResult(rules);
	}
	
	[HttpGet]
	[Route("GetFizzBuzz")]
	public async Task<OkObjectResult> RunFizzBuzz(int firstNumber, int secondNumber) {
		var rules = await fizzBuzzRepo.GetAllRules();
		var result = ruleManager.Execute(firstNumber, secondNumber, rules);
		return new OkObjectResult(result);
	}
	
	[HttpPost]
	[Route("CreateRule")]
	public async Task<IActionResult> CreateRule([FromBody]Rules2 rule) {
		await fizzBuzzRepo.CreateRule(rule);
		return new OkResult();
	}
	
	[HttpDelete]
	[Route("DeleteRules")]
	public async Task<object> DeleteRules() {
		await fizzBuzzRepo.DeleteRules();
		return new OkResult();
	}
	
	[HttpPut]
	[Route("UpdateRules")]
	public async Task<object> UpdateRule(Rules2 rule) {
		await fizzBuzzRepo.UpdateRule(rule);
		return new OkResult();
	}
	
	[HttpPost]
	[Route("AddDefaultRules")]
	public async Task<IActionResult> AddDefaultRules() {
		var rules = generateDefaultRules.Execute();
		await fizzBuzzRepo.AddDefaultRules(rules);
		return new OkResult();
	}
}