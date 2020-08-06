using System;
using System.Text;

namespace GarageManger.GarageLogic
{
    public class Car : Vehicle
    {
        // Data Members:
        private const int k_NumberOfCarWheels = 4;
        private const int k_CarMaxPsi = 32;
        private eCarPaint m_Paint;
        private eDoorNumber m_NumberOfDoors;

        public Car(string i_ModelName, string i_LicensePlate, float i_CurrentAirPressure, string i_ProducerName) : base(i_ModelName, i_LicensePlate)
        {
            setWheels(k_NumberOfCarWheels, k_CarMaxPsi, i_CurrentAirPressure, i_ProducerName);
        }

        // getters/setters:
        public eCarPaint Paint
        {
            get
            {
                return m_Paint;
            }
            set
            {
                m_Paint = value;
            }
        }

        public eDoorNumber NumberOfDoors
        {
            get
            {
                return m_NumberOfDoors;
            }
            set
            {
                m_NumberOfDoors = value;
            }
        }

        // methods:
        public override string ToString()
        {
            return string.Format(
@"Vehicle type:Car
License plate:{0}
Model name:{1}
Color:{2}
Doors number:{3} 
Engine details:
{4}
Wheels details:
{5}", r_LicensePlate, r_ModelName, m_Paint, m_NumberOfDoors, m_Engine.ToString(), getWheelsInfo());

        }

        // Nested enums:
        public enum eCarPaint
        {
            Red = 1,
            White,
            Black,
            Silver,
        }

        public enum eDoorNumber
        {
            Two = 1,
            Three,
            Four,
            Five,
        }
    }
}
