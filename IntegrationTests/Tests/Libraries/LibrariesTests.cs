using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using MbUnit.Framework;
using Telerik.Sitefinity.Modules.Libraries;

namespace IntegrationTests.Tests.Libraries
{
    [TestFixture]
    [Description("Tests the methods in the LibrariesMethods.cs file.")]
    public class LibrariesTests
    {
        public const string TestLibraryId = "B9EBDE2B-975C-45E1-98E5-F921E798884B";
        public const string TestLibraryTitle = "Test Library";

        [Test]
        [Description("Verifies that a document library can be created.")]
        [Author("Telerik Developer")]
        public void CreateDocumentLibraryTest()
        {
            Guid newDocumentLibraryId = new Guid(TestLibraryId);
            string expectedTitle = TestLibraryTitle;

            LibrariesMethods.CreateDocumentLibrary(newDocumentLibraryId, expectedTitle);

            var documentLibrary = LibrariesMethods.GetDocumentLibrary(newDocumentLibraryId);

            Assert.IsNotNull(documentLibrary);
            Assert.AreEqual<string>(expectedTitle, documentLibrary.Title);
        }

        [Test]
        [Description("Verifies that a document library can be queried by Id.")]
        [Author("Telerik Developer")]
        public void GetDocumentLibraryTest()
        {
            Guid newDocumentLibraryId = new Guid(TestLibraryId);
            string expectedTitle = TestLibraryTitle;

            LibrariesMethods.CreateDocumentLibrary(newDocumentLibraryId, expectedTitle);

            var documentLibrary = LibrariesMethods.GetDocumentLibrary(newDocumentLibraryId);

            Assert.IsNotNull(documentLibrary);
            Assert.AreEqual<string>(expectedTitle, documentLibrary.Title);
        }

        [Test]
        [Description("Verifies that a document library can be modified.")]
        [Author("Telerik Developer")]
        public void ModifyDocumentLibraryNativeTestDevGuide()
        {
            Guid newDocumentLibraryId = new Guid(TestLibraryId);
            string expectedTitle = TestLibraryTitle;

            LibrariesMethods.CreateDocumentLibrary(newDocumentLibraryId, expectedTitle);

            string newTitle = "Dev Guide New title";
            LibrariesMethods.ModifyDocumentLibrary(newDocumentLibraryId, newTitle);

            var modifiedDocumentLibrary = LibrariesMethods.GetDocumentLibrary(newDocumentLibraryId);

            Assert.AreEqual<string>(newTitle, modifiedDocumentLibrary.Title);
        }

        [Test]
        [Description("Verifies that a document library can be deleted.")]
        [Author("Telerik Developer")]
        public void DeleteDocumentLibraryNativeTestDevGuide()
        {
            Guid newDocumentLibraryId = new Guid(TestLibraryId);
            string expectedTitle = "Test Library";

            LibrariesMethods.CreateDocumentLibrary(newDocumentLibraryId, expectedTitle);

            int initialDocumentLibrariesCount = LibrariesMethods.GetAllDocumentLibraries().Count;

            LibrariesMethods.DeleteDocumentLibrary(newDocumentLibraryId);

            int finalDocumentLibrariesCount = LibrariesMethods.GetAllDocumentLibraries().Count;

            Assert.AreEqual(initialDocumentLibrariesCount - 1, finalDocumentLibrariesCount);
        }

        [TearDown()]
        public void TearDown()
        {
            var libraryId = new Guid(TestLibraryId);
            var library = LibrariesMethods.GetDocumentLibrary(libraryId);

            if (library != null)
            {
                LibrariesMethods.DeleteDocumentLibrary(libraryId);
            }
        }
    }
}
