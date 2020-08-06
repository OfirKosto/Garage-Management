using System;

namespace GarageManger.GarageLogic
{
    public class GasEngine : Engine
    {
        // Date members:
        private readonly eFuelType r_FuelType;

        public GasEngine(float i_MaxFuelQuantity, eFuelType i_FuelType) : base(i_MaxFuelQuantity)
        {
            r_FuelType = i_FuelType;
        }

        //getters/setters:
        public eFuelType FuelType
        {
            get
            {
                return r_FuelType;
            }
        }

        // methods:
        public void Refueling(float i_LittersToFuel, eFuelType i_FuelType)
        {
            if (r_FuelType == i_FuelType)
            {
                if (i_LittersToFuel + m_CurrentEnergyQuantity <= m_MaxEnergyCapacity)
                {
                    m_CurrentEnergyQuantity += i_LittersToFuel;
                }
                else
                {
                    throw new ValueOutOfRangeException(0, m_MaxEnergyCapacity - m_CurrentEnergyQuantity, "The amount of the fuel to fill is too high.");
                }
            }
            else
            {
                throw new ArgumentException("The fuel type is incorrect.");
            }
        }

        public override string ToString()
        {
            return string.Format(
@"Engine type: fuel based.
Fuel type:{0}
Remaining fuel:{1} L", Enum.GetName(typeof(eFuelType), r_FuelType), m_CurrentEnergyQuantity);

        }

        //Nested enums:
        public enum eFuelType
        {
            Octan95 = 1,
            Octan96,
            Octan98,
            Soler,
        }
    }
}
