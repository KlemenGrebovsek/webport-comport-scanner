using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MatthiWare.CommandLine.Abstractions.Command;
using webport_comport_scanner.Model;
using webport_comport_scanner.Option;
using webport_comport_scanner.Printer;
using webport_comport_scanner.Scanner;

namespace webport_comport_scanner.Command
{
    /// <summary>
    /// Scans for web ports and their status.
    /// </summary>
    public class ScanWebPortCommand : Command<ProgramOptions, CommandOptions>
    {
        public override void OnConfigure(ICommandConfigurationBuilder<CommandOptions> builder)
        {
            base.OnConfigure(builder);

            builder
                .Name("webPort")
                .Required(false)
                .Description("This command scans web ports.");
        }

        public override async Task OnExecuteAsync(ProgramOptions pOptions, CommandOptions cOptions, CancellationToken cToken)
        {
            Console.WriteLine("Scanning for web ports...");
            
            var portScanner = new WebPortScanner();
            var printer = new PortStatusPrinter(Console.Out);

            try
            {
                var scanResult = await portScanner.ScanAsync(pOptions.MinPort, pOptions.MaxPort, cToken);

                if (pOptions.Status != PortStatus.Any)
                {
                    var stringStatus = pOptions.Status.ToString();
                    
                    await printer.PrintTableAsync(scanResult
                        .Where(x => x.GetStatusString() == stringStatus), cToken);
                }
                else
                {
                    await printer.PrintTableAsync(scanResult, cToken);
                }
            }
            catch (TaskCanceledException) { }
            catch (Exception e)
            {
                Console.WriteLine($"Command 'webPort', ran into exception: {e.Message}");
            }
        }
    }
}