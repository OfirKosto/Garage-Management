using System;

namespace GarageManger.GarageLogic
{
    public class ElecrticEngine : Engine
    {
        // methods:
        public ElecrticEngine(float i_MaxBatteryLifeTime) : base(i_MaxBatteryLifeTime) { }

        public void ChargeBattery(float i_HoursToCharge)
        {
            if (m_CurrentEnergyQuantity + i_HoursToCharge <= m_MaxEnergyCapacity)
            {
                m_CurrentEnergyQuantity += i_HoursToCharge;
            }
            else
            {
                throw new ValueOutOfRangeException(0, (m_MaxEnergyCapacity - m_CurrentEnergyQuantity) * 60, "The amount of hours to charge is too high.");
            }
        }

        public override string ToString()
        {
            return string.Format(
@"Engine type: Battery Based.
Current hours that left in the battery:{0} H", m_CurrentEnergyQuantity);

        }
    }
}
