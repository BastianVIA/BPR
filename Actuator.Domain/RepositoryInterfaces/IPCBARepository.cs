using Domain.Entities;

namespace Domain.RepositoryInterfaces;

public interface IPCBARepository
{
    Task CreatePCBA(PCBA pcba);
    Task<PCBA> GetPCBA(string id);
    Task UpdatePCBA(PCBA pcba);
}