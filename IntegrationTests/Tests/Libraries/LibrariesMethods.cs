using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Telerik.Sitefinity.Modules.Libraries;
using Telerik.Sitefinity.Libraries.Model;

namespace IntegrationTests.Tests.Libraries
{
    public class LibrariesMethods
    {
        public static void CreateDocumentLibrary(Guid documentLibraryId, string documentLibraryTitle)
        {
            LibrariesManager librariesManager = LibrariesManager.GetManager();
            DocumentLibrary library = librariesManager.GetDocumentLibraries().Where(b => b.Id == documentLibraryId).FirstOrDefault();

            if (library == null)
            {
                //Create the library.
                library = librariesManager.CreateDocumentLibrary(documentLibraryId);

                //Set the properties of the library.
                library.Title = documentLibraryTitle;
                library.DateCreated = DateTime.UtcNow;
                library.LastModified = DateTime.UtcNow;
                library.UrlName = FormatUrlName(documentLibraryTitle);

                //Save the changes.
                librariesManager.SaveChanges();
            }
        }

        public static DocumentLibrary GetDocumentLibrary(Guid documentLibraryId)
        {
            LibrariesManager librariesManager = LibrariesManager.GetManager();
            DocumentLibrary library = librariesManager.GetDocumentLibraries().Where(l => l.Id == documentLibraryId).FirstOrDefault();

            return library;
        }

        public static List<DocumentLibrary> GetAllDocumentLibraries()
        {
            LibrariesManager librariesManager = LibrariesManager.GetManager();

            return librariesManager.GetDocumentLibraries().ToList();
        }

        public static void ModifyDocumentLibrary(Guid documentLibraryId, string newTitle)
        {
            LibrariesManager librariesManager = LibrariesManager.GetManager();

            //Get the library.
            DocumentLibrary library = librariesManager.GetDocumentLibraries().Where(b => b.Id == documentLibraryId).FirstOrDefault();

            if (library != null)
            {

                //Modify the library.
                library.Title = newTitle;
                library.LastModified = DateTime.UtcNow;
                library.Urls.Clear();
                library.UrlName = FormatUrlName(newTitle);

                //Save the changes.
                librariesManager.SaveChanges();
            }
        }

        public static void DeleteDocumentLibrary(Guid documentLibraryId)
        {
            LibrariesManager manager = LibrariesManager.GetManager();

            DocumentLibrary library = manager.GetDocumentLibraries().Where(b => b.Id == documentLibraryId).SingleOrDefault();

            if (library != null)
            {
                //Mark the library to be deleted.
                manager.DeleteDocumentLibrary(library);

                //Save the changes.
                manager.SaveChanges();
            }
        }

        public static string FormatUrlName(string title)
        {
            return Regex.Replace(title.ToLower(), @"[^\w\-\!\$\'\(\)\=\@\d_]+", "-");
        }
    }
}
