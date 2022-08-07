namespace ActiveInactiveUsersReport.src.DirManagment
{
    public static class DirManager
    {
        private static string MasterPath = $"{Directory.GetCurrentDirectory()}/ExternalResources";
        private static Dictionary<string, ManagedDirectory> DirectoriesDict = new Dictionary<string, ManagedDirectory>();
        private static List<string> allowedExtensions = new List<string>() { "xlsx", "csv" };

        private static List<string> knownManagedDirectories = new List<string>()
        {
            "Export",
            "Import",
        };

        public static void InitiateDirectories()
        {
            for(int i = 0; i < knownManagedDirectories.Count; i++)
            {
                DirectoriesDict.Add(knownManagedDirectories[i], new ManagedDirectory(knownManagedDirectories[i], $"{MasterPath}/{knownManagedDirectories[i]}"));
            }
            VerifyManagedDirectories();
        }

        public static void VerifyManagedDirectories()
        {
            for (int i = 0; i < DirectoriesDict.Count; i++)
            {
                if (!Directory.Exists(DirectoriesDict.ElementAt(i).Value.Path))
                {
                    Directory.CreateDirectory(DirectoriesDict.ElementAt(i).Value.Path);
                    Console.WriteLine($"New Directory Created: {DirectoriesDict.ElementAt(i).Value.Name}");
                }
                else
                {
                    Console.WriteLine($"Directory Exists: {DirectoriesDict.ElementAt(i).Value.Name}");
                }
            }
        }
        public static void EmptyDirectory()
        {
            DirectoryInfo directory = new DirectoryInfo(MasterPath);
            foreach (DirectoryInfo dir in directory.GetDirectories())
            {
                foreach(FileInfo file in dir.GetFiles())
                {
                    Console.WriteLine($"{file.Name} removed.");
                    file.Delete();
                }
                Console.WriteLine($"{dir.Name} removed.");
                dir.Delete();
            }
        }
        public static List<string> RecieveFiles()
        {
            List<string> files = new List<string>(Directory
                .EnumerateFiles(DirectoriesDict.GetValueOrDefault("Import")!.Path,"*.*")
                .Where(s => allowedExtensions.Contains(Path.GetExtension(s).TrimStart('.').ToLowerInvariant()) && !s.Contains('~')));
            return files;
        }

        public static string GetDirectoryPath(string managedDirName)
        {
            string? returnedPath = null;
            try
            {
                returnedPath = DirectoriesDict.GetValueOrDefault(managedDirName)?.Path;
            } catch (Exception e) 
            {
                Input.ConsoleInputManager.PrintError($"{e.ToString}\n\nIncorrect Managed Directory Name provided.");
            }
            return returnedPath!;
        }
    }
}
