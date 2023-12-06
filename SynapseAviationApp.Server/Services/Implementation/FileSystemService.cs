using SynapseAviationApp.Server.Services.Interfaces;
using System.Reflection;
using System.Text;

namespace SynapseAviationApp.Server.Services.Implementation
{
    public class FileSystemService : IFileSystemService
    {
        public async Task<string> GetFileContentAsync(string fileName)
        {
            try
            {
                //Getting the current assembly's directory path that we need for stored xml model
                var assmblyFullPath = Assembly.GetExecutingAssembly().Location;
                var assmblyDirectoryPath = Path.GetDirectoryName(assmblyFullPath);
                var assmblyDirectoryPathAsArray = assmblyDirectoryPath.Split('\\');

                // Extract the project directory path up to 'bin'
                var fileDirecory = new StringBuilder();
                foreach (var assmblyDirectoryPathAsArrayItem in assmblyDirectoryPathAsArray)
                {
                    if (assmblyDirectoryPathAsArrayItem.Equals("bin"))
                        break;
                    fileDirecory.Append(assmblyDirectoryPathAsArrayItem);
                    fileDirecory.Append('\\');
                }

                var fullFilePath = Path.Combine(fileDirecory.ToString(), "Models", fileName);
                return fullFilePath;
            }
            catch (IOException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}

