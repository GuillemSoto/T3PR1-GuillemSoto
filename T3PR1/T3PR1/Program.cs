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
        /// <summary>
        /// Funció per printar el menú i executar cadascuna de les funcions
        /// </summary>
        /// <param name="storedInfo">storedInfo és l'string que emmagatzema la informació de totes les simulacions</param>
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
        /// <summary>
        /// Emmagatzema la informació de la simulació que acaba de passar
        /// </summary>
        /// <param name="storedInfo"></param>
        /// <param name="simulationResult">simulationResult és l'string que es retorna de la simulació que acaba de pasar</param>
        /// <returns>Retorna l'string amb l'informació de totes les sessions</returns>
        static string StoreInfo(string storedInfo, string[] simulationResult)
        {
            for (int i = 0; i < simulationResult.Length; i++)
            {
                storedInfo += DateTime.Now + simulationResult[i] + "\n";
            }
            return storedInfo;
        }
        /// <summary>
        /// Prepara l'array d'strings de la simulació
        /// </summary>
        /// <returns>Retorna l'array amb l'informació de les simulacions</returns>
        static string[] simulationStart()
        {
            string[] simulations = new string[AmountOfSimulations()];
            return EnergyType(simulations);
        }
        /// <summary>
        /// Retorna el numero de simulacions que vols fer en la sessió
        /// </summary>
        static int AmountOfSimulations()
        {
            const string msgAmountSimulations = "Quantes simulacions vols fer?";
            int minRange = 0;
            bool flag = false;
            Console.WriteLine(msgAmountSimulations);
            return AskNum(minRange);
        }
        /// <summary>
        /// S'escull el tipus d'energia i comencen les simulacions amb el tipus corresponent
        /// </summary>
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
        /// <summary>
        /// Amb un for es van creant instàncies de objectes de classe Solar per calcular l'energia que es genera a la simulació i s'emmagatzema en la posició corresponent de l'array
        /// </summary>
        /// <returns>Retorna l'array d'strings amb els paràmetres de la simulació</returns>
        static string[] SolarEnergy(string[] simulation)
        {
            //EN HORAS
            const string solarString = "     Solar               ";
            const string solarSpace = "                          ";
            const string enterSunHours = "Introdueix les hores de sol (han de ser més de 0)";
            int sunHours = 0;
            int minRange = 1;
            Console.WriteLine(enterSunHours);
            for (int i = 0; i < simulation.Length; i++)
            {
                sunHours=AskNum(minRange);
                Solar solar = new Solar(sunHours);
                simulation[i] = solarString + sunHours + solarSpace + solar.CalculateEnergy() + jouleString;
            }
            return simulation;
        }
        /// <summary>
        /// Amb un for es van creant instàncies de objectes de classe Hydroelectric per calcular l'energia que es genera a la simulació i s'emmagatzema en la posició corresponent de l'array
        /// </summary>
        /// <returns>Retorna l'array d'strings amb els paràmetres de la simulació</returns>
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
                volumeOfFlow=AskNum(minRange);
                Hydroelectric hydro = new Hydroelectric(volumeOfFlow);
                simulation[i] = hydroString + volumeOfFlow + hydroSpace + hydro.CalculateEnergy() + jouleString;
            }
            return simulation;
        }
        /// <summary>
        /// Amb un for es van creant instàncies de objectes de classe Eolic per calcular l'energia que es genera a la simulació i s'emmagatzema en la posició corresponent de l'array
        /// </summary>
        /// <returns>Retorna l'array d'strings amb els paràmetres de la simulació</returns>
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
                windSpe=AskNum(minRange);
                Eolic eolic = new Eolic(windSpe);
                simulation[i] = eolicString + windSpe + eolicSpace + eolic.CalculateEnergy() + jouleString;
            }
            return simulation;
        }
        /// <summary>
        /// Demana números per consola i gestiona els errors de format o de rang.
        /// </summary>
        /// <param name="minRange">El rang mínim que pot tenir el número demanat</param>
        /// <returns>Retorna el número demanat</returns>
        static int AskNum(int minRange)
        {
            int num = 0;
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
