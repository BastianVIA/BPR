using Domain.Entities;

namespace Domain.Repositories;

public interface IPCBARepository
{
    Task CreatePCBA(PCBA pcba);
    Task<PCBA> GetPCBA(string id);
    Task<PCBA> GetLocalPCBA(string id);
    Task UpdatePCBA(PCBA pcba);
}