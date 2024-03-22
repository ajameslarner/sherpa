using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using Microsoft.VisualStudio.TestPlatform.ObjectModel.Adapter;
using Microsoft.VisualStudio.TestPlatform.ObjectModel.Logging;
using Sherpa.src.sherpa.Attributes;
using System.Reflection;

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
        var testCases = new List<TestCase>();
        var assembly = Assembly.LoadFrom(assemblyPath);
        var types = assembly.GetTypes();

        foreach (var type in types)
        {
            var methods = type.GetMethods();

            foreach (var method in methods)
            {
                var testAttribute = method.GetCustomAttribute<SherpaMethod>(inherit: false);
                if (testAttribute != null)
                {
                    var testCase = new TestCase(method.Name, new Uri(assemblyPath), assemblyPath)
                    {
                        DisplayName = method.Name,
                        CodeFilePath = method?.DeclaringType?.FullName,
                        LineNumber = 0
                    };

                    testCases.Add(testCase);
                }
            }
        }

        return testCases;
    }

    private void ExecuteTest(TestCase test, IFrameworkHandle frameworkHandle)
    {
        var assembly = Assembly.LoadFrom(test.Source);
        var type = assembly.GetTypes().FirstOrDefault(t => t.GetMethods().Any(m => m.Name == test.FullyQualifiedName));
        var methodInfo = type.GetMethod(test.FullyQualifiedName);

        var testInstance = Activator.CreateInstance(type);

        try
        {
            methodInfo.Invoke(testInstance, null);
            frameworkHandle.RecordResult(new TestResult(test) { Outcome = TestOutcome.Passed });
        }
        catch (Exception ex)
        {
            frameworkHandle.RecordResult(new TestResult(test) { Outcome = TestOutcome.Failed, ErrorMessage = ex.Message });
        }
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