using System.Diagnostics;

namespace EndToEndTests;

public class TestController
{
    private static int _testRemaining;
    private Process? _frontendProcess;
    private Process? _backendProcess;
    
    public TestController()
    {
        SetRemainingTests();
        try
        {
            StartProcesses();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            Environment.Exit(0);
        }
    }

    private void SetRemainingTests()
    {
        _testRemaining = NUnit.Framework.Internal.TestExecutionContext.CurrentContext.CurrentTest.TestCaseCount;
    }
    
    private void StartProcesses()
    {
        StartBackend();
        StartFrontend();
    }

    private void StartFrontend()
    {
        var startInfo = GetStartInfo("Frontend");
        _frontendProcess = Process.Start(startInfo)!;
        Thread.Sleep(500);
        if (_frontendProcess.HasExited)
        {
            throw new Exception("Frontend could not start");
        }
    }

    private void StartBackend()
    {
        var startInfo = GetStartInfo("Backend");
        _backendProcess = Process.Start(startInfo)!;
        Thread.Sleep(500);
        if (_backendProcess.HasExited)
        {
            throw new Exception("Backend could not start");
        }
    }
    
    private ProcessStartInfo GetStartInfo(string project)
    {
        return new ProcessStartInfo
        {
            FileName = "dotnet",
            Arguments = $"run --project ..\\..\\..\\..\\{project}",
            UseShellExecute = false,
            WorkingDirectory = AppContext.BaseDirectory
        };
    }

    private void KillProcesses()
    {
        _backendProcess!.Kill();
        _frontendProcess!.Kill();
    }

    public void TestDone()
    {
        _testRemaining--;
        if (_testRemaining == 0)
        {
            KillProcesses();
        }
    }
}