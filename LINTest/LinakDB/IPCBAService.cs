using LINTest.Models;

namespace LINTest.LinakDB;

public interface IPCBAService
{
    public PCBAModel GetPCBA(string uid);
}