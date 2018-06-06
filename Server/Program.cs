using OutageManagementSystem.Common;
using OutageManagementSystem.Service;
using OutageManagementSystem.Service.Enums;
using System;

namespace OutageManagementSystem.Server
{
    class Program
    {
        private static RepositoryType repositoryType;
        private static GeneratorType generatorType;
        private static IOutageManagementSystem system;
        private static Connection connection;

        static void Main(string[] args)
        {
            StartServer();
        }     

        private static void StartServer()
        {
            repositoryType = ConfigHelper.GetRepositorySetting();
            generatorType = ConfigHelper.GetGeneratorSetting();

            Console.WriteLine("Initializing system...");
            system = Factory.GenerateOMS(repositoryType, generatorType);
            Console.WriteLine("System ready.");

            Console.WriteLine("Creating connection...");
            connection = new Connection(system);
            Console.WriteLine("Connection ready.");

            Console.WriteLine("Starting server...");
            bool result = connection.Open();
            if (result)
            {
                Console.WriteLine("Server online.");

                Console.ReadKey(true);

                Console.WriteLine("Shutting down server...");
                connection.Close();
                Console.WriteLine("Server offline.");
            }
            else
            {
                Console.WriteLine("Server failed to start.");

                Console.ReadKey(true);
            }  
        }
    }
}
