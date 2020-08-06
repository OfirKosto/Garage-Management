using System;

namespace GarageManger.GarageLogic
{
    public struct VehicleOwner
    {
        // Date members
        private readonly string r_Name;
        private readonly string r_PhoneNumber;

        public VehicleOwner(string i_Name, string i_PhoneNumber)
        {
            r_Name = i_Name;
            r_PhoneNumber = i_PhoneNumber;
        }

        // getters/setters
        public string Name
        {
            get
            {
                return r_Name;
            }
        }

        public string PhoneNumber
        {
            get
            {
                return r_PhoneNumber;
            }
        }

        // methods:
        public override string ToString()
        {
            return string.Format(
@"Owner details:
Name:{0}
Phone number:{1}", r_Name, r_PhoneNumber);

        }
    }
}
