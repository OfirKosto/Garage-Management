using System;
using GarageManger.GarageLogic;

namespace GarageManger.ConsoleUI
{
    class Program
    {
        public static void Main()
        {
            try
            {
                GarageUi garageProgram = new GarageUi();
                garageProgram.RunGarageUi();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                System.Threading.Thread.Sleep(3000);
            }
        }
    }
}
