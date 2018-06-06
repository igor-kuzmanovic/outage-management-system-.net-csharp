using OutageManagementSystem.Common.Models;
using System;

namespace OutageManagementSystem.Service.Validators
{
    static class OutageValidator
    {
        public static bool Validate(Outage outage, out string message)
        {
            message = string.Empty;

            if (string.IsNullOrWhiteSpace(outage.Description))
            {
                message = "Outage description cannot be empty.";
                return false;
            }
            else if (DateTime.Compare(outage.CreationDate, DateTime.Now) > 0)
            {
                message = "Outage creation date cannot be in the future.";
                return false;
            }
            else if (string.IsNullOrWhiteSpace(outage.ElementName))
            {
                message = "Outage faulty element name cannot be empty.";
                return false;
            }
            else if (outage.Latitude < -90 || outage.Latitude > 90)
            {
                message = "Outage faulty element latitude must be in the range [-90, 90].";
                return false;
            }
            else if (outage.Longitude < -180 || outage.Longitude > 180)
            {
                message = "Outage faulty element latitude must be in the range [-180, 180].";
                return false;
            }
            else
            {
                foreach (var action in outage.Actions)
                {
                    if (string.IsNullOrWhiteSpace(action.Description))
                    {
                        message = "Outage executed action description cannot be empty.";
                        return false;
                    }
                    else if (DateTime.Compare(action.ExecutionDate, DateTime.Now) > 0)
                    {
                        message = "Outage executed action execution date cannot be in the future.";
                        return false;
                    }
                }
            }

            return true;
        }
    }
}
