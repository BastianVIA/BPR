using Domain.Entities;

namespace Domain.Repositories;

public interface IPCBARepository
{
    Task CreatePCBA(PCBA pcba);
    Task<PCBA> GetPCBA(int id);
}