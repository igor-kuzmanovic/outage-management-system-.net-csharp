using OutageManagementSystem.Service.Enums;
using System;
using System.Configuration;

namespace OutageManagementSystem.Server
{
    static class ConfigHelper
    {
        public static RepositoryType GetRepositorySetting()
        {
            RepositoryType repositoryType;
            try
            {
                string repositoryTypeSetting = ConfigurationManager.AppSettings["repositoryType"];
                bool result = Enum.TryParse(repositoryTypeSetting, true, out repositoryType);
                if (!result)
                {
                    repositoryType = RepositoryType.Live;
                }
            }
            catch (ConfigurationErrorsException)
            {
                repositoryType = RepositoryType.Live;
            }
            return repositoryType;
        }

        public static GeneratorType GetGeneratorSetting()
        {
            GeneratorType generatorType;
            try
            {
                string generatorTypeSetting = ConfigurationManager.AppSettings["generatorType"];
                bool result = Enum.TryParse(generatorTypeSetting, true, out generatorType);
                if (!result)
                {
                    generatorType = GeneratorType.Live;
                }
            }
            catch (ConfigurationErrorsException)
            {
                generatorType = GeneratorType.Live;
            }
            return generatorType;
        }
    }
}
