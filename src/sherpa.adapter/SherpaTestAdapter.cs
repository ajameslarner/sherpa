using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using Microsoft.VisualStudio.TestPlatform.ObjectModel.Adapter;
using Microsoft.VisualStudio.TestPlatform.ObjectModel.Logging;

namespace Sherpa.Adapter;

[ExtensionUri("executor://SherpaTestAdapter")]
[DefaultExecutorUri("executor://SherpaTestAdapter")]
public class SherpaTestAdapter : ITestExecutor, ITestDiscoverer
{
    public void DiscoverTests(IEnumerable<string> sources, IDiscoveryContext discoveryContext, IMessageLogger logger, ITestCaseDiscoverySink discoverySink)
    {
        foreach (var source in sources)
        {
            var tests = DiscoverTestsInAssembly(source);
            foreach (var test in tests)
            {
                discoverySink.SendTestCase(test);
            }
        }
    }
    public void RunTests(IEnumerable<string> sources, IRunContext runContext, IFrameworkHandle frameworkHandle)
    {
        foreach (var source in sources)
        {
            var tests = DiscoverTestsInAssembly(source);
            foreach (var test in tests)
            {
                ExecuteTest(test, frameworkHandle);
            }
        }
    }
    private IEnumerable<TestCase> DiscoverTestsInAssembly(string assemblyPath)
    {
        // TODO: Implement logic to discover tests in the assembly
        var testCases = new List<TestCase>
        {
            CreateTestCase("TestMethod1", assemblyPath),
            CreateTestCase("TestMethod2", assemblyPath)
        };


        return testCases;
    }
    private void ExecuteTest(TestCase test, IFrameworkHandle frameworkHandle)
    {
        // TODO: Implement logic to execute the test and report the result
        var result = new TestResult(test)
        {
            Outcome = TestOutcome.Passed
        };
        frameworkHandle.RecordResult(result);
    }
    private TestCase CreateTestCase(string methodName, string assemblyPath)
    {
        var testCase = new TestCase(methodName, new Uri(assemblyPath), assemblyPath)
        {
            DisplayName = methodName
        };
        return testCase;
    }

    public void RunTests(IEnumerable<TestCase>? tests, IRunContext? runContext, IFrameworkHandle? frameworkHandle)
    {
        throw new NotImplementedException();
    }

    public void Cancel()
    {
        throw new NotImplementedException();
    }
}