using OutageManagementSystem.Common;
using System;
using System.ServiceModel;

namespace OutageManagementSystem.Server
{
    class Connection
    {
        private readonly ServiceHost host;

        public Connection(IOutageManagementSystem outageManagementSystem)
        {
            host = new ServiceHost(outageManagementSystem);
        }

        public bool Open()
        {
            try
            {
                host.Open();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public void Close()
        {
            try
            {
                host.Close();
            }
            catch (Exception)
            {
                host.Abort();
            }
        }
    }
}
