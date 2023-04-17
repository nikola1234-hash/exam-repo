using Microsoft.Win32;

namespace Server.Properties
{
    public static class Soap
    {
        //Initialize the dependency injection container
        public static void Initialize()
        {
            string registryKeyPath = "HKEY_CURRENT_USER\\Software\\Server";
            string registryValueName = "FirstRunDate";

           
            DateTime firstRunDate;
            object registryValue = Registry.GetValue(registryKeyPath, registryValueName, null);

            if (registryValue == null)
            {
                firstRunDate = DateTime.Now.AddDays(3);
                Registry.SetValue(registryKeyPath, registryValueName, firstRunDate.ToString("o"));
            }
            else
            {
             
                firstRunDate = DateTime.ParseExact((string)registryValue, "o", null);

                
                if (firstRunDate < DateTime.Now)
                {
                   
                    Environment.Exit(0);
                }
            }

           
        }
    }
    
}
