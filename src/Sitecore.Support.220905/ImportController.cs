using System.Collections.Generic;
using Sitecore.ListManagement.Services;
using Sitecore.Services.Core;
using System.Web.Http;
using Sitecore.ListManagement.Services.Model;
using Sitecore.ListManagement;
using Sitecore.ListManagement.ContentSearch.Model;
using Sitecore.Web.Http.Filters;

namespace Sitecore.Support.ListManagement.Services
{
    [ContactListLockedExceptionFilter, AccessDeniedExceptionFilter, UnauthorizedAccessExceptionFilter, SitecoreAuthorize(Roles = @"sitecore\List Manager Editors"), AnalyticsDisabledAttributeFilter, ServicesController("ListManagement.Import")]
    public class ImportController : Sitecore.ListManagement.Services.ImportController
    {
        private static List<string> activeImports = new List<string>();
        private readonly ListManager<ContactList, ContactData> listManager;

        public ImportController() : base()
        {
        }


        public ImportController(ListManager<ContactList, ContactData> listManager) : base(listManager)
        {
            this.listManager = listManager;
        }

        [HttpPost, ValidateHttpAntiForgeryToken]
        public  ImportResultModel ImportContactsFromMediaLibrarySupport(Dictionary<string, int> mapping, string id)
        {
            ImportResultModel result = null;
            if (!activeImports.Contains(id))
            {              
                activeImports.Add(id);
                result = base.ImportContactsFromMediaLibrary(mapping, id);
                activeImports.Remove(id);
            }
            return result;
        } 

        [HttpPost, ValidateHttpAntiForgeryToken]
        public ImportResultModel ImportContactsFromMediaLibraryAndCreateListSupport(Dictionary<string, int> mapping, string id)
        {
            ImportResultModel result = null;
            if (!activeImports.Contains(id))
            {
                activeImports.Add(id);
                result = base.ImportContactsFromMediaLibraryAndCreateList(mapping, id);
                activeImports.Remove(id);
            }
            return result;
        }
    }
}