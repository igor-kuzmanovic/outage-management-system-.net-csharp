using OutageManagementSystem.Common;
using System.ServiceModel;

namespace OutageManagementSystem.Client
{
    static class Connection
    {
        private static ChannelFactory<IOutageManagementSystem> factory;

        public static IOutageManagementSystem GenerateChannel()
        {
            factory = new ChannelFactory<IOutageManagementSystem>("OutageManagementSystem");
            return factory.CreateChannel();
        }
    }
}
