using System.Diagnostics;
using System.Reflection;

namespace EndToEndTests;

public class TestController
{
    private static TestController? _instance;
    
    private Process _frontendProcess;
    private Process _backendProcess;
    public static int _testRemaining = 0;
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
        ProcessStartInfo startInfo = new ProcessStartInfo
        {
            FileName = "dotnet",
            Arguments = "run --project ..\\..\\..\\..\\Frontend",
            UseShellExecute = false,
            //RedirectStandardOutput = true, // Enable redirection of standard output
            CreateNoWindow = false,
            WorkingDirectory = AppContext.BaseDirectory
        };
        _frontendProcess = Process.Start(startInfo);
    }

    private void StartBackend()
    {
        ProcessStartInfo startInfo = new ProcessStartInfo
        {
            FileName = "dotnet",
            Arguments = "run --project ..\\..\\..\\..\\Backend",
            UseShellExecute = false,
            //RedirectStandardOutput = true, // Enable redirection of standard output
            CreateNoWindow = false,
            WorkingDirectory = AppContext.BaseDirectory
        };
        _backendProcess = Process.Start(startInfo);
    }

    private void KillProcesses()
    {
        _backendProcess.Kill();
        _frontendProcess.Kill();
        // _frontendProcess.WaitForExit();
        //
        // Environment.Exit(0);
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