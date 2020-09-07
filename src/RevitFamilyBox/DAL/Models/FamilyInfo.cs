using MongoDB.Bson;

namespace RevitFamilyBox.DAL.Models
{
  public class FamilyInfo
  {
    public ObjectId Id { get; set; }

    public string Name { get; set; }

    /// <summary>
    /// parameters, as json format
    /// </summary>
    public string Parameters { get; set; }

    public int UserId { get; set; }

    public int VersionId { get; set; }

    public int FileBlobId { get; set; }
  }
}
