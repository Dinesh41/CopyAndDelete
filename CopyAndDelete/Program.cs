// See https://aka.ms/new-console-template for more information
Console.WriteLine("BackupHelper");
Console.Write($"Enter Source Path:");
//Collect inputs
var sourcePath = Console.ReadLine();
Console.Write($"Entere Target Path:");
var targetPath = Console.ReadLine();

//Validation for the path
if (String.IsNullOrEmpty(sourcePath)) throw new NullReferenceException("Source Path is empty");
if (String.IsNullOrEmpty(targetPath)) throw new NullReferenceException("Target Path is empty");
if (!Directory.Exists(sourcePath))
    throw new DirectoryNotFoundException($"Source Path not found: {sourcePath}");

//Perform the copy and delete
CopyAndDeleteDirectory(sourcePath, targetPath, true);

Console.WriteLine("Press any key to exit");
Console.ReadKey(true);



static void CopyAndDeleteDirectory(string sourceDir, string destinationDir, bool recursive)
{
    // Get information about the source directory
    var dir = new DirectoryInfo(sourceDir);

    // Cache directories before we start copying
    DirectoryInfo[] dirs = dir.GetDirectories();

    // Create the destination directory
    Directory.CreateDirectory(destinationDir);

    // Get the files in the source directory and copy to the destination directory
    foreach (FileInfo file in dir.GetFiles())
    {
        string targetFilePath = Path.Combine(destinationDir, file.Name);
        file.CopyTo(targetFilePath);
        Console.WriteLine($"File Copied: {file.Name}");
        //Delete the source file
        file.Delete();
    }

    // If recursive and copying subdirectories, recursively call this method
    if (recursive)
    {
        foreach (DirectoryInfo subDir in dirs)
        {
            string newDestinationDir = Path.Combine(destinationDir, subDir.Name);
            Console.WriteLine($"Copying sub directory: {subDir.Name}");
            CopyAndDeleteDirectory(subDir.FullName, newDestinationDir, true);
        }
    }
    dir.Delete();
    Console.WriteLine($"Directory Deleted: {dir.Name}");
}