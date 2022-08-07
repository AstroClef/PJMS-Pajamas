namespace ActiveInactiveUsersReport.src.DirManagment
{
    internal class ManagedDirectory
    {
        public string Name { get; set; }
        public string Path { get; set; }

        public ManagedDirectory(string name, string path)
        {
            Name = name;
            Path = path;
        }
    }
}
