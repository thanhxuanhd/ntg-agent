﻿
using Microsoft.KernelMemory;

namespace NTG.Agent.Orchestrator.Knowledge;

public class KernelMemoryKnowledge : IKnowledgeService
{
    private readonly MemoryWebClient _memoryWebClient;

    public KernelMemoryKnowledge()
    {
        _memoryWebClient = new MemoryWebClient("https://localhost:7181", "Blm8d7sFx7arM9EN2QUxGy7yUjCyvRjx");
    }
    public async Task ImportDocument(Stream content, string fileName, Guid agentId, CancellationToken cancellationToken = default)
    {
        await _memoryWebClient.ImportDocumentAsync(content, fileName);
    }

    public Task<string> SearchKnowledgeAsync(string query, Guid agentId, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<string> SearchKnowledgeAsync(string query, Guid agentId, Guid userId, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}
