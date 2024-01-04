// See https://aka.ms/new-console-template for more information
using System.Diagnostics;

Console.WriteLine("app started");
int httpCount = -1;
int httpsCount = -1;
while (true)
{
    var p1 = new Process();
    p1.StartInfo.FileName = "cmd.exe";
    p1.StartInfo.Arguments = @"/c netstat -an | find /C ""443""";
    p1.StartInfo.CreateNoWindow = true;
    p1.StartInfo.RedirectStandardError = true;
    p1.StartInfo.RedirectStandardOutput = true;
    p1.StartInfo.RedirectStandardInput = false;
    p1.OutputDataReceived += (a, b) =>
    {
        if (b.Data != null && int.TryParse(b.Data, out int count))
            httpsCount = count;
    };
    var p2 = new Process();
    p2.StartInfo.FileName = "cmd.exe";
    p2.StartInfo.Arguments = @"/c netstat -an | find /C ""80""";
    p2.StartInfo.CreateNoWindow = true;
    p2.StartInfo.RedirectStandardError = true;
    p2.StartInfo.RedirectStandardOutput = true;
    p2.StartInfo.RedirectStandardInput = false;
    p2.OutputDataReceived += (a, b) =>
    {
        if (b.Data != null && int.TryParse(b.Data, out int count))
            httpCount = count;
    };
    p1.Start();
    p2.Start();
    p1.BeginErrorReadLine();
    p1.BeginOutputReadLine();
    p2.BeginErrorReadLine();
    p2.BeginOutputReadLine();
    p1.WaitForExit();
    p2.WaitForExit();
    Console.WriteLine($"Number Of http Connetion: {httpCount}");
    Console.WriteLine($"Number Of https Connetion: {httpsCount}");
    Thread.Sleep(5000);
}