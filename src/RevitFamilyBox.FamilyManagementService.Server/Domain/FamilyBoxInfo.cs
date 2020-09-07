using System;
using System.Collections.Generic;
using System.Text.Json;

namespace RevitFamilyBox.FamilyManagementService.Server.Domain
{
    public class FamilyBoxInfo
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public JsonDocument Parameters { get; set; }

        public IEnumerable<string> ParentIds { get; set; }

        public FamilyBoxInfo()
        {
        }
    }
}
