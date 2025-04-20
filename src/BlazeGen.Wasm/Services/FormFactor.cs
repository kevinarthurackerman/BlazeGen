using BlazeGen.Shared.Services;

namespace BlazeGen.Wasm.Services
{
    public class FormFactor : IFormFactor
    {
        public string GetFormFactor()
        {
            return "WASM";
        }

        public string GetPlatform()
        {
            return Environment.OSVersion.ToString();
        }
    }
}
