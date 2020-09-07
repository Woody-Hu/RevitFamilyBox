﻿using MongoDB.Bson;
using System.Text.Json;

namespace RevitFamilyBox.FamilyManagementService.Server.DAL.Models
{
  public class FamilyInfoEntity
  {
    public ObjectId Id { get; set; }

    public string Name { get; set; }

    public string Parameters { get; set; }


    public int UserId { get; set; }

    public int VersionId { get; set; }

    public int FileBlobId { get; set; }
  }
}
