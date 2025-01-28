using System.Reflection.Emit;
using System.Runtime.ConstrainedExecution;
using System;
using System.Drawing;
namespace T3PR1
{
    internal class Program
    {
        const string errorMsg = "Número incorrecte";
        const string jouleString = " J";
        static void Main(string[] args)
        {
            string storedInfo = "Data i hora             Tipus d'energia     Paràmetres de l'energia     Quantitat d'energia\n";
            Menu(storedInfo);
        }
        static void Menu(string storedInfo)
        {
            const string endMsg = "Programa finalitzat";
            const string menu = "Escull la teva opció: \nIniciar simulació(1)\nVeure informe de simulacions(2)\nSortir(3)";
            Console.WriteLine(menu);
            bool flag = false;
            const int minRange = 1;
            const int maxRange = 3;
            int menuChoice = 0;
            while (!flag)
            {
                try
                {
                    menuChoice = int.Parse(Console.ReadLine());
                    if (menuChoice >= minRange && menuChoice <= maxRange)
                    {
                        flag = true;
                    }
                    else
                    {
                        Console.WriteLine(errorMsg);
                    }
                }
                catch (FormatException)
                {
                    Console.WriteLine(errorMsg);
                }
            }
            switch (menuChoice)
            {
                case 1: storedInfo = StoreInfo(storedInfo, simulationStart()); Menu(storedInfo); break;
                case 2: Console.WriteLine(storedInfo); Menu(storedInfo); break;
                case 3: Console.WriteLine(endMsg); break;
                default: Console.WriteLine(errorMsg); Menu(storedInfo); break;
            }
        }
        static string StoreInfo(string storedInfo, string[] simulationResult)
        {
            for (int i = 0; i < simulationResult.Length; i++)
            {
                storedInfo += DateTime.Now + simulationResult[i] + "\n";
            }
            return storedInfo;
        }
        static string[] simulationStart()
        {
            string[] simulations = new string[AmountOfSimulations()];
            return EnergyType(simulations);
        }
        static int AmountOfSimulations()
        {
            const string msgAmountSimulations = "Quantes simulacions vols fer?";
            int amountOfSimulations = 0;
            bool flag = false;
            Console.WriteLine(msgAmountSimulations);
            while (!flag)
            {
                try
                {
                    amountOfSimulations = int.Parse(Console.ReadLine());
                    if (amountOfSimulations >= 0)
                    {
                        flag = true;
                    }
                    else
                    {
                        Console.WriteLine(errorMsg);
                    }
                }
                catch (FormatException)
                {
                    Console.WriteLine(errorMsg);
                }
            }
            return amountOfSimulations;
        }
        static string[] EnergyType(string[] simulation)
        {
            const string energyMenu = "Escull el tipus d'energia: \nEòlica(1)\nHidroelèctrica(2)\nSolar(3)";
            Console.WriteLine(energyMenu);
            bool flag = false;
            const int minRange = 1;
            const int maxRange = 3;
            int energyType = 0;
            while (!flag)
            {
                try
                {
                    energyType = int.Parse(Console.ReadLine());
                    if (energyType >= minRange && energyType <= maxRange)
                    {
                        flag = true;
                    }
                    else
                    {
                        Console.WriteLine(errorMsg);
                    }
                }
                catch (FormatException)
                {
                    Console.WriteLine(errorMsg);
                }
            }
            switch (energyType)
            {
                case 1: return EolicEnergy(simulation);
                case 2: return HydroelectricEnergy(simulation);
                case 3: return SolarEnergy(simulation);
                default: Console.WriteLine(errorMsg); return null;
            }
        }
        static string[] SolarEnergy(string[] simulation)
        {
            //EN HORAS
            const string solarString = "     Solar               ";
            const string solarSpace = "                          ";
            const string enterSunHours = "Introdueix les hores de sol (han de ser més de 0)";
            bool flag = false;
            int sunHours = 0;
            int minRange = 1;
            Console.WriteLine(enterSunHours);
            for (int i = 0; i < simulation.Length; i++)
            {
                sunHours=AskNum(sunHours, minRange);
                Solar solar = new Solar(sunHours);
                simulation[i] = solarString + sunHours + solarSpace + solar.CalculateEnergy() + jouleString;
            }
            return simulation;
        }
        static string[] HydroelectricEnergy(string[] simulation)
        {
            //EN M3
            const string hydroString = "     Hidroelèctrica      ";
            const string hydroSpace = "                          ";
            const string enterVolumeOfFlow = "Introdueix el cabal de l'aigua en m3 (han de ser mínim 20)";
            int volumeOfFlow = 0;
            int minRange = 20;
            Console.WriteLine(enterVolumeOfFlow);
            for (int i = 0; i < simulation.Length; i++)
            {
                volumeOfFlow=AskNum(volumeOfFlow, minRange);
                Hydroelectric hydro = new Hydroelectric(volumeOfFlow);
                simulation[i] = hydroString + volumeOfFlow + hydroSpace + hydro.CalculateEnergy() + jouleString;
            }
            return simulation;
        }
        static string[] EolicEnergy(string[] simulation)
        {
            //EN M/S
            const string eolicString = "     Eòlica              ";
            const string eolicSpace = "                          ";
            const string enterWindSpeed = "Introdueix la velocitat del vent en m/s (ha de ser mínim de 5)";
            int windSpe = 0;
            int minRange = 5;
            Console.WriteLine(enterWindSpeed);
            for (int i = 0; i < simulation.Length; i++)
            {
                windSpe=AskNum(windSpe, minRange);
                Eolic eolic = new Eolic(windSpe);
                simulation[i] = eolicString + windSpe + eolicSpace + eolic.CalculateEnergy() + jouleString;
            }
            return simulation;
        }
        static int AskNum(int num, int minRange)
        {
            bool flag = false;
            while (!flag)
            {
                try
                {
                    num = int.Parse(Console.ReadLine());
                    if (num >= minRange)
                    {
                        flag = true;
                    }
                    else
                    {
                        Console.WriteLine(errorMsg);
                    }
                }
                catch (FormatException)
                {
                    Console.WriteLine(errorMsg);
                }
            }
            return num;
        }
    }
}
