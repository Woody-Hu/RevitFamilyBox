using System;
using System.Collections.Generic;
using System.Text.Json;

namespace RevitFamilyBox.FamilyManagementService.Server.Domain
{
    public class FamilyInfo
    {
        public string Id { get; set; }

        public string Name { get; set; }

        /// <summary>
        /// parameters, as json format
        /// </summary>
        public JsonDocument Parameters { get; set; }

        public int UserId { get; set; }

        public int VersionId { get; set; }

        public int FileBlobId { get; set; }

        public IEnumerable<string> ParentIds { get; set; }
    }
}
