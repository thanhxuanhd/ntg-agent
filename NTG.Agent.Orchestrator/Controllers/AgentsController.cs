﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NTG.Agent.Orchestrator.Agents;
using NTG.Agent.Orchestrator.Data;
using NTG.Agent.Orchestrator.Extentions;
using NTG.Agent.Shared.Dtos.Agents;
using NTG.Agent.Shared.Dtos.Chats;

namespace NTG.Agent.Orchestrator.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AgentsController : ControllerBase
{
    private readonly IAgentService _agentService;
    private readonly AgentDbContext _agentDbContext;

    public AgentsController(IAgentService agentService, AgentDbContext agentDbContext)
    {
        _agentService = agentService ?? throw new ArgumentNullException(nameof(agentService));
        _agentDbContext = agentDbContext ?? throw new ArgumentNullException(nameof(agentDbContext));
    }

    [HttpPost("chat")]
    public async IAsyncEnumerable<PromptResponse> ChatAsync([FromBody] PromptRequest promptRequest)
    {
        Guid? userId = User.GetUserId();

        await foreach (var response in _agentService.ChatStreamingAsync(userId, promptRequest))
        {
            yield return new PromptResponse(response);
        }
    }

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetAgentsAsync()
    {
        var agents = await _agentDbContext.Agents
            .Select(x => new AgentListItem(x.Id, x.Name))
            .ToListAsync();
        return Ok(agents);
    }
}
