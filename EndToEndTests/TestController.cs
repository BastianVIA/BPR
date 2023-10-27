using System.Diagnostics;
using System.Reflection;

namespace EndToEndTests;

public class TestController
{
    private static TestController? _instance;
    
    private Process _frontendProcess;
    private Process _backendProcess;
    private static int _testRemaining;
    public static TestController Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new TestController();
            }

            return _instance;
        }
    }

    private TestController()
    {
        SetRemainingTests();
        StartProcesses();
    }

    private void SetRemainingTests()
    {
        Assembly assembly = Assembly.LoadFrom("EndToEndTests.dll");
        var methods = assembly.GetTypes()
            .SelectMany(t => t.GetMethods()
                .Where(m => m.GetCustomAttributes(typeof(TestAttribute), false).Any()))
            .ToArray().Length;
        _testRemaining = methods;
    }

    private void StartProcesses()
    {
        StartFrontend();
        StartBackend();
    }

    private void StartFrontend()
    {
        ProcessStartInfo startInfo = GetStartInfo("Frontend");
        _frontendProcess = Process.Start(startInfo);
    }

    private void StartBackend()
    {
        ProcessStartInfo startInfo = GetStartInfo("Backend");
        _backendProcess = Process.Start(startInfo);
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
        _backendProcess.Kill();
        _frontendProcess.Kill();
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