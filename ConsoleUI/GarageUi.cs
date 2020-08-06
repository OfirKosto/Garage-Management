using System;
using System.Text;
using System.Collections.Generic;
using GarageManger.GarageLogic;

namespace GarageManger.ConsoleUI
{
    class GarageUi
    {
        //Data Members
        Garage m_GarageModel = new Garage();

        //Methods
        public void RunGarageUi()
        {
            bool isProgramStillRuning = true;

            while (isProgramStillRuning)
            {
                try
                {
                    Console.Clear();
                    mainMenuFormat();
                    eMainMenuSelection userSelection = returnUserChoiceFromFormat<eMainMenuSelection>();

                    switch (userSelection)
                    {
                        case eMainMenuSelection.AddNewVehicle:
                            {
                                addVehicleToGarage();
                                break;
                            }
                        case eMainMenuSelection.ShowVehicleLicenseList:
                            {
                                vehicleStatusMenuList();
                                break;
                            }
                        case eMainMenuSelection.ChangeVehicleStatus:
                            {
                                changeVehicleStatusInGarage();
                                break;
                            }
                        case eMainMenuSelection.InflateWheels:
                            {
                                inflateWheels();
                                break;
                            }
                        case eMainMenuSelection.RefuelGasVehicle:
                            {
                                refuelGasVehicleInGarage();
                                break;
                            }
                        case eMainMenuSelection.ChargeElectricVehicle:
                            {
                                chargeElectricVehicleInGarage();
                                break;
                            }
                        case eMainMenuSelection.VehicleInfo:
                            {
                                vehicleInfoInGarage();
                                break;
                            }
                        case eMainMenuSelection.Exit:
                            {
                                Console.WriteLine("Exiting the program....");
                                System.Threading.Thread.Sleep(1500);
                                isProgramStillRuning = false;
                                break;
                            }
                    }
                }
                catch (FormatException formatException)
                {
                    Console.WriteLine(formatException.Message);
                    System.Threading.Thread.Sleep(1000);
                }
                catch (ArgumentException argumentException)
                {
                    Console.WriteLine(argumentException.Message);
                    System.Threading.Thread.Sleep(1500);
                }
                catch (ReturnToMenuException)
                {
                    Console.WriteLine("Returning to the main menu...");
                    System.Threading.Thread.Sleep(1500);
                }
            }
        }

        private T printMenuFormatAndReturnUserChoice<T>(string i_InstructionLine)
        {
            bool isInvalidInput = true;
            T enumChoice = default(T);

            while (isInvalidInput)
            {
                try
                {
                    showMenuFormat<T>(i_InstructionLine);
                    enumChoice = returnUserChoiceFromFormat<T>();
                    isInvalidInput = false;
                }
                catch (FormatException formatException)
                {
                    Console.WriteLine(formatException.Message);
                }
            }

            return enumChoice;
        }

        private T returnUserChoiceFromFormat<T>()
        {
            T userSelection;
            string inputStr = Console.ReadLine();
            checkBackToMainMenu(inputStr);

            if (int.TryParse(inputStr, out int choiceNumber) && choiceNumber >= 1 && choiceNumber <= Enum.GetNames(typeof(T)).Length)
            {
                userSelection = (T)Enum.Parse(typeof(T), choiceNumber.ToString());
            }
            else
            {
                throw new FormatException("Invalid input.");
            }

            return userSelection;
        }

        private void showMenuFormat<T>(string i_InstructionLine)
        {
            int lineIndexToShow = 1;
            Console.WriteLine(i_InstructionLine);

            foreach (string choiceToShow in Enum.GetNames(typeof(T)))
            {
                Console.WriteLine(string.Format("{0}. {1}", lineIndexToShow, choiceToShow));
                lineIndexToShow++;
            }
        }

        private void addVehicleToGarage()
        {
            Console.Clear();
            string licensePlateStr = licensePlateInput();
            checkBackToMainMenu(licensePlateStr);

            if (m_GarageModel.IsVehicleInGarage(licensePlateStr))
            {
                m_GarageModel.ChangeVehicleStatus(licensePlateStr, VehicleInGarage.eStatus.InRepair);
                Console.WriteLine("There is already vehicle with this license plate in the garage, his status changed to InRepair now.");
                System.Threading.Thread.Sleep(3000);
            }
            else // new vehicle
            {
                // preparing the vehicle 
                VehicleInGarage vehicleInGarageToAdd;
                VehicleOwner ownerInfo;
                Vehicle vehicleInfo = null;

                ownerInfo = ownerInput();
                vehicleInfo = vehicleInput(licensePlateStr);

                // Create specific vehicle properties:
               if (vehicleInfo is Car)
                {
                    carProperties(vehicleInfo as Car);
                }
               else if (vehicleInfo is Motorcycle)
                {
                    motorcycleProperties(vehicleInfo as Motorcycle);
                }
               else // truck
                {
                    truckProperties(vehicleInfo as Truck);
                }

                // Add the vehicle to the garage:
                vehicleInGarageToAdd = new VehicleInGarage(ownerInfo, vehicleInfo);
                m_GarageModel.AddVehical(vehicleInGarageToAdd);
                Console.WriteLine("Vehicle added successfully.");
                System.Threading.Thread.Sleep(2500);
            }
        }

        private Vehicle vehicleInput(string i_LicensePlate)
        {
            VehicleCreator.eVehicleType vehicleType;
            string vehicleModelName, wheelsProducerName;
            float currentWheelsAirPressure;
            Vehicle vehicleToReturn = null;
            bool isInvalidVehicle = true;

            vehicleType = printMenuFormatAndReturnUserChoice<VehicleCreator.eVehicleType>("Choose vehicle type:");

            Console.WriteLine("Enter vehicle model name:");
            vehicleModelName = Console.ReadLine();
            checkBackToMainMenu(vehicleModelName);

            Console.WriteLine("Enter wheels producer name:");
            wheelsProducerName = Console.ReadLine();
            checkBackToMainMenu(wheelsProducerName);

            while (isInvalidVehicle)
            {
                try
                {
                    currentWheelsAirPressure = garagePositiveFloatVaribalesInput("Enter wheels current air pressure:");
                    // create the vehicle:
                    vehicleToReturn = VehicleCreator.CreateVehicle(vehicleType, vehicleModelName, i_LicensePlate, currentWheelsAirPressure, wheelsProducerName);

                    if (vehicleToReturn.Engine is GasEngine)
                    {
                        vehicleToReturn.Engine.CurrentEnergyQuantity = garagePositiveFloatVaribalesInput("Enter current fuel litters in the tank:");
                    }
                    else // electric engine
                    {
                        vehicleToReturn.Engine.CurrentEnergyQuantity = garagePositiveFloatVaribalesInput("Enter current battery charge in minutes:") / 60;
                    }

                    isInvalidVehicle = false;
                }
                catch(FormatException formatException)
                {
                    Console.WriteLine(formatException.Message);
                }
                catch(ArgumentException argumentException)
                {
                    Console.WriteLine(argumentException.Message);
                }
                catch(ValueOutOfRangeException outOfRangeException)
                {
                    Console.WriteLine(string.Format("{0} (Range: {1} - {2})", outOfRangeException.Message, outOfRangeException.MinValue, outOfRangeException.MaxValue));
                }
            }

            return vehicleToReturn;
        }

        private VehicleOwner ownerInput()
        {
            string name;
            StringBuilder phoneNumber = new StringBuilder();
            bool isInvalidPhoneNumber = true;

            Console.WriteLine("Please enter owner name:");
            name = Console.ReadLine();

            while(isInvalidPhoneNumber)
            {
                try
                {
                    Console.WriteLine("Please enter owner phone number:");
                    phoneNumber.Append(" "); // avoid empty string error
                    phoneNumber.Replace(phoneNumber.ToString(), Console.ReadLine());
                    checkBackToMainMenu(phoneNumber.ToString());

                    if (uint.TryParse(phoneNumber.ToString(), out uint result))
                    {
                        isInvalidPhoneNumber = false;
                    }
                    else
                    {
                        throw new FormatException("Invalid phone number, use only digits.");
                    }
                }
                catch (FormatException formatExeption)
                {
                    Console.WriteLine(formatExeption.Message);
                }
            }

            return new VehicleOwner(name, phoneNumber.ToString());
        }

        private void carProperties(Car o_CarToInitialze)
        {
            o_CarToInitialze.NumberOfDoors = printMenuFormatAndReturnUserChoice<Car.eDoorNumber>("Choose number of doors:");
            o_CarToInitialze.Paint = printMenuFormatAndReturnUserChoice<Car.eCarPaint>("Choose car paint:");
        }

        private void motorcycleProperties(Motorcycle o_MotorcycleToInitialze)
        {
            o_MotorcycleToInitialze.LicenseType = printMenuFormatAndReturnUserChoice<Motorcycle.eLicenseType>("Choose motorcycle license type:");
            o_MotorcycleToInitialze.EngineVolume = engineVolumeInput();

        }

        private void truckProperties(Truck o_TruckToInitialze)
        {
            o_TruckToInitialze.IsCarringHazardousMaterial = isCarringHazardousMaterialInput();
            o_TruckToInitialze.CargoCapacity = garagePositiveFloatVaribalesInput("Enter cargo capacity:");
        }

        private int engineVolumeInput()
        {
            bool isInvalidEngineVolume = true;
            uint engineVolume = 0;

            while (isInvalidEngineVolume)
            {
                try
                {
                    Console.WriteLine("Enter engine volume:");

                    if (uint.TryParse(Console.ReadLine(), out engineVolume))
                    {
                        isInvalidEngineVolume = false;
                    }
                    else
                    {
                        throw new FormatException("Wrong input, engine volume have to be positive integer number.");
                    }
                }
                catch (FormatException formatException)
                {
                    Console.WriteLine(formatException.Message);
                }
            }

            return (int)engineVolume;
        }

        private bool isCarringHazardousMaterialInput()
        {
            bool isInvalidHazardousMaterialInput = true;
            StringBuilder HazardousMaterialAnswer = new StringBuilder();

            while (isInvalidHazardousMaterialInput)
            {
                try
                {
                    Console.WriteLine("Is the truck carring hazardous materials? Y|N");
                    HazardousMaterialAnswer.Append(" "); // avoid empty string error
                    HazardousMaterialAnswer.Replace(HazardousMaterialAnswer.ToString(), Console.ReadLine());
                    checkBackToMainMenu(HazardousMaterialAnswer.ToString());

                    if (HazardousMaterialAnswer.ToString().Equals("Y") || HazardousMaterialAnswer.ToString().Equals("N"))
                    {
                        isInvalidHazardousMaterialInput = false;
                    }
                    else
                    {
                        throw new FormatException("Invalid input.");
                    }
                }
                catch (FormatException formatException)
                {
                    Console.WriteLine(formatException.Message);
                }
            }

            return HazardousMaterialAnswer.ToString().Equals("Y");
        }

        private void vehicleStatusMenuList()
        {
            Console.Clear();
            VehicleInGarage.eStatus vehicleStatusToShow = printMenuFormatAndReturnUserChoice<VehicleInGarage.eStatus>("Choose status:");
            List<string> vehicleListByStatus = m_GarageModel.ListOfVehicleLicensePlatesInGarageByStatus(vehicleStatusToShow);

            // print the list:
            if (vehicleListByStatus.Count != 0)
            {
                Console.WriteLine(string.Format("{0} vehicles license plates:", Enum.GetName(typeof(VehicleInGarage.eStatus), vehicleStatusToShow)));
                vehicleListByStatus.ForEach(Console.WriteLine);
            }
            else
            {
                Console.WriteLine("There are no vehicles with this status at the garage.");
            }
            endOfActionMessage();
        }

        private void changeVehicleStatusInGarage()
        {
            Console.Clear();
            string licensePlate = licensePlateInput();
            VehicleInGarage.eStatus newStatus = printMenuFormatAndReturnUserChoice<VehicleInGarage.eStatus>("Choose new status:");

            m_GarageModel.ChangeVehicleStatus(licensePlate, newStatus);
            Console.WriteLine("Vehicle status changed successfully.");
            System.Threading.Thread.Sleep(2500);
        }

        private void inflateWheels()
        {
            Console.Clear();
            string licensePlate = licensePlateInput();

            m_GarageModel.InflateWheelsToMax(licensePlate);

            Console.WriteLine("The vehicle wheels successfully inflated to maximum.");
            System.Threading.Thread.Sleep(2500);
        }

        private void refuelGasVehicleInGarage()
        {
            Console.Clear();
            bool isInvalidRefuelInput = true;
            string licensePlate = licensePlateInput();
            GasEngine.eFuelType fuelTypeToGas = printMenuFormatAndReturnUserChoice<GasEngine.eFuelType>("Choose fuel type:");
            float fuelAmount;

            while (isInvalidRefuelInput)
            {
                try
                {
                    fuelAmount = garagePositiveFloatVaribalesInput("Enter amount of fuel to add:");

                    m_GarageModel.RefuelGasVehicle(licensePlate, fuelTypeToGas, fuelAmount);
                    isInvalidRefuelInput = false;
                    Console.WriteLine("Vehicle successfully refueld.");
                    System.Threading.Thread.Sleep(2500);
                }
                catch (ValueOutOfRangeException outOfRangeException)
                {
                    Console.WriteLine(string.Format("{0} (Range: {1} - {2})", outOfRangeException.Message, outOfRangeException.MinValue, outOfRangeException.MaxValue));
                }
            }

        }

        private void chargeElectricVehicleInGarage()
        {
            Console.Clear();
            string licensePlate = licensePlateInput();
            bool isInvalidMinutesToChargeInput = true;
            float minutesToCharge;

            while (isInvalidMinutesToChargeInput)
            {
                try
                {
                    minutesToCharge = garagePositiveFloatVaribalesInput("Enter amount of minutes to charge.");
                    m_GarageModel.ChargeElectricVehicle(licensePlate, minutesToCharge);

                    isInvalidMinutesToChargeInput = false;
                    Console.WriteLine("Vehicle battery successfully charged.");
                    System.Threading.Thread.Sleep(2500);
                }
                catch (ValueOutOfRangeException outOfRangeException)
                {
                    Console.WriteLine(string.Format("{0} (Range: {1} - {2})", outOfRangeException.Message, outOfRangeException.MinValue, outOfRangeException.MaxValue));
                }
            }
        }

        private void vehicleInfoInGarage()
        {
            Console.Clear();
            string licensePlate = licensePlateInput();
            Console.WriteLine(m_GarageModel.GetVehicleInfo(licensePlate));

            endOfActionMessage();
        }

        private string licensePlateInput()
        {
            Console.WriteLine("Please enter a license number:");
            return Console.ReadLine();
        }

        private float garagePositiveFloatVaribalesInput(string i_MessageToUser)
        {
            bool isInvalidInput = true;
            float InputVaribaleToReturn = 0;

            while (isInvalidInput)
            {
                try
                {
                    Console.WriteLine(i_MessageToUser);

                    if (float.TryParse(Console.ReadLine(), out InputVaribaleToReturn) && InputVaribaleToReturn >= 0)
                    {
                        isInvalidInput = false;
                    }
                    else
                    {
                        throw new FormatException("Invalid input.");
                    }
                }
                catch (FormatException formatException)
                {
                    Console.WriteLine(formatException.Message);
                }
            }

            return InputVaribaleToReturn;
        }

        private void checkBackToMainMenu(string i_UserInput)
        {
            if (i_UserInput.Equals("Q"))
            {
                throw new ReturnToMenuException();
            }
        }

        private void endOfActionMessage()
        {
            Console.WriteLine("Prease any key to return to the main menu..");
            Console.ReadLine();
        }

        private void mainMenuFormat()
        {
            string mainMenuStr = string.Format(
@"Please choose an option, Press 'Q' at any time to return to the main menu:
1. Add new vehicle.
2. Show vehicles license Plates by status.
3. Change vehicle status.
4. Inflate vehicle wheels.
5. Refuel gas vehicle.
6. Charge electric vehicle battery.
7. Show vehicle information.
8. Exit.");

            Console.WriteLine(mainMenuStr);
        }

        public enum eMainMenuSelection
        {
            AddNewVehicle = 1,
            ShowVehicleLicenseList,
            ChangeVehicleStatus,
            InflateWheels,
            RefuelGasVehicle,
            ChargeElectricVehicle,
            VehicleInfo,
            Exit,
        }
    }
}
