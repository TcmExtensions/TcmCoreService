using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TcmCoreService.CommunicationManagement;
using TcmCoreService.ContentManagement;
using Tridion.ContentManager.CoreService.Client;

namespace TcmCoreService.Test
{
    [TestClass]
    public class ClientTest : IDisposable
    {
        private Client mClient;

        public ClientTest()
        {
            mClient = new Client(new Uri("http://localhost"), "user", "tridion");

            Assert.AreEqual("7.1.0", mClient.ApiVersion);
        }

        [TestMethod]
        public void ComponentRead()
        {
            Component component = mClient.GetComponent("tcm:5-54");

            // Component Loaded
            Assert.IsNotNull(component, "Component is null");

            // Normal component
            Assert.AreEqual(component.ComponentType, ComponentType.Normal);

            // Component Title is correct
            Assert.AreEqual(component.Title, "Club sandwich");

            // Component Schema loaded
            Assert.IsNotNull(component.Schema);

            Assert.AreEqual("Sandwich", component.Schema.Title);
        }

        [TestMethod]
        public void ComponentTemplateRead()
        {
            ComponentTemplate componentTemplate = mClient.GetComponentTemplate("tcm:4-322-32");

            // ComponentTemplate Loaded
            Assert.IsNotNull(componentTemplate, "ComponentTemplate is null");

            // ComponentTemplate Title is correct
            Assert.AreEqual("Summary", componentTemplate.Title);
        }

        [TestMethod]
        public void SchemaRead()
        {
            Schema schema = mClient.GetSchema("tcm:3-1099-8");
            
            // Component Loaded
            Assert.IsNotNull(schema, "Schema is null");

            // Component Schema
            Assert.AreEqual(schema.SchemaPurpose, SchemaPurpose.Component);

            // Component Title is correct
            Assert.AreEqual("Sandwich", schema.Title);
        }

        [TestMethod]
        public void FolderRead()
        {
            Folder folder = mClient.GetFolder("tcm:2-2-2");

            // Folder Loaded
            Assert.IsNotNull(folder, "Folder is null");

            // Folder Title is correct
            Assert.AreEqual("Building Blocks", folder.Title);
        }

        [TestMethod]
        public void StructureGroupRead()
        {
            StructureGroup structureGroup = mClient.GetStructureGroup("tcm:2-3-4");

            // Structure group Loaded
            Assert.IsNotNull(structureGroup, "Structure Group is null");

            // Structure Group Title is correct
            Assert.AreEqual("Website Root", structureGroup.Title);
        }

        [TestMethod]
        public void PageRead()
        {
            Page page = mClient.GetPage("tcm:6-19-64");

            // Page Loaded
            Assert.IsNotNull(page, "Page is null");

            // Page Title is correct
            Assert.AreEqual("Welcome to globaldeli.com", page.Title);
        }

        [TestMethod]
        public void PageTemplateRead()
        {
            PageTemplate pageTemplate = mClient.GetPageTemplate("tcm:4-293-128");

            // PageTemplate Loaded
            Assert.IsNotNull(pageTemplate, "PageTemplate is null");

            // PageTemplate Title is correct
            Assert.AreEqual("Content Page", pageTemplate.Title);
        }

        [TestMethod]
        public void PublicationRead()
        {
            Publication publication = mClient.GetPublication("tcm:0-2-1");

            // Publication Loaded
            Assert.IsNotNull(publication, "Publication is null");

            // Publication Title is correct
            Assert.AreEqual("00 Empty Parent", publication.Title);
        }

        [TestMethod]
        public void CategoryRead()
        {
            Category category = mClient.GetCategory("tcm:5-121-512");

            // Category Loaded
            Assert.IsNotNull(category, "Category is null");

            // Category Title is correct
            Assert.AreEqual("Countries", category.Title);
        }

        [TestMethod]
        public void KeywordRead()
        {
            Keyword keyword = mClient.GetKeyword("tcm:5-60-1024");
            
            // Keyword Loaded
            Assert.IsNotNull(keyword, "Keyword is null");

            // Keyword Title is correct
            Assert.AreEqual("Cheese", keyword.Title);
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (mClient != null)
                    mClient.Dispose();
            }
        }
    }
}
