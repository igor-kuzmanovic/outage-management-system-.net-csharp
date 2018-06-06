using OutageManagementSystem.Common;
using OutageManagementSystem.Service.Contexts;
using OutageManagementSystem.Service.Enums;
using OutageManagementSystem.Service.Generators;
using OutageManagementSystem.Service.Interfaces;
using OutageManagementSystem.Service.Repositories;
using OutageManagementSystem.Service.Services;

namespace OutageManagementSystem.Service
{
    public static class Factory
    {
        public static IOutageManagementSystem GenerateOMS(RepositoryType repositoryType, GeneratorType generatorType)
        {
            IOutageManagementSystem system;
            IOutageService outageService;
            IOutageRepository outageRepository;
            IReportService reportService;
            IReportGenerator reportGenerator;

            switch (repositoryType)
            {
                case RepositoryType.Live:
                    outageRepository = new OutageRepository(new DatabaseContext());
                    break;
                case RepositoryType.TestValid:
                    outageRepository = new TestOutageRepository(new TestContext());
                    break;
                case RepositoryType.TestInvalid:
                default:
                    outageRepository = new InvalidTestOutageRepository();
                    break;
            }

            switch (generatorType)
            {
                case GeneratorType.Live:
                    reportGenerator = new ReportGenerator();
                    break;
                case GeneratorType.TestValid:
                    reportGenerator = new TestReportGenerator();
                    break;
                case GeneratorType.TestInvalid:
                default:
                    reportGenerator = new InvalidTestReportGenerator();
                    break;
            }

            outageService = new OutageService(outageRepository);
            reportService = new ReportService(reportGenerator);
            system = new OMS(outageService, reportService);

            return system;
        }
    }
}
