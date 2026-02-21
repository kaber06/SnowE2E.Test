using NUnit.Framework;

namespace SnowE2E.Test.Helper
{
    public static class TestDataHelper
    {
        public static List<string> CreatedRITMNumbers = new List<string>();

        public static void CloseRITM()
        {
            try
            {
                foreach (var ritmNumber in CreatedRITMNumbers)
                {
                    APIHelper.CloseRITM(ritmNumber);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error during RITM cleanup: {ex.Message}");
            }
        }
    }
}