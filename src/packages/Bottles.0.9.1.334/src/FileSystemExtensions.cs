using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;
using Bottles.Diagnostics;
using FubuCore;

namespace Bottles
{
    public static class FileSystemExtensions
    {

        public static bool PackageManifestExists(this IFileSystem fileSystem, string directory)
        {
            return fileSystem.FileExists(directory, PackageManifest.FILE);
        }

        public static PackageManifest LoadPackageManifestFrom(this IFileSystem fileSystem, string directory)
        {
            return fileSystem.TryFindManifest(directory, PackageManifest.FILE)
                   ?? fileSystem.TryFindManifest(directory, PackageManifest.FILE);
        }

        public static PackageManifest TryFindManifest(this IFileSystem system, string directory, string fileName)
        {
            if (fileName.IsEmpty()) return null;

            var path = FileSystem.Combine(directory, fileName);
            if (system.FileExists(path))
            {
                var manifest = system.LoadFromFile<PackageManifest>(path);
                manifest.ManifestFileName = path;

                return manifest;
            }

            return null;
        }


        public static T LoadFromStream<T>(this IFileSystem system, Stream fileStream) where T : new()
        {
            var xmlSerializer = new XmlSerializer(typeof (T));
            try
            {
                return (T) xmlSerializer.Deserialize(fileStream);
            }
            catch (Exception ex)
            {
                throw new ApplicationException(
                    "Unable to deserialize the contents of stream into an instance of type {0}".ToFormat(typeof (T)));
            }
        }

        public static string FindBinaryDirectory(this IFileSystem fileSystem, string directory, string target)
        {
            var binFolder = directory.AppendPath("bin");
            var compileTargetFolder = binFolder.AppendPath(target);
            if (fileSystem.DirectoryExists(compileTargetFolder))
            {
                binFolder = compileTargetFolder;
            }
            else
            {
                LogWriter.Current.Trace("'{0}' did not exist.", compileTargetFolder);
            }

            LogWriter.Current.Trace("  Looking for binaries at " + binFolder);

            return binFolder;
        }

		public static IEnumerable<string> FindAssemblyNames(this IFileSystem fileSystem, string directory)
        {
            var fileSet = new FileSet{
                DeepSearch = false,
                Include = "*.dll;*.exe"
            };

            return fileSystem.FindFiles(directory, fileSet).Select(Path.GetFileNameWithoutExtension);
        }
    }
}