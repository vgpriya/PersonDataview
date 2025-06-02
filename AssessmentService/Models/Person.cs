//using System.Text.Json.Serialization;

using System.Text.Json.Serialization;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;

namespace AssessmentService.Models;

public class Person
{
    [BsonId]
    [BsonElement("_id")]
    [BsonRepresentation(BsonType.ObjectId)]
    //[JsonIgnore] // <- Add this
    [JsonPropertyName("id")]
    public string Id { get; set; }
    // [BsonId]
    // [BsonElement("_id")]
    // public BsonObjectId Id { get; set; }
    [JsonPropertyName("name")]
    public string Name { get; set; }
    [JsonPropertyName("company")]
    public string Company { get; set; }
    [JsonPropertyName("email")]
    public string Email { get; set; }
    [JsonPropertyName("phone")]
    public string Phone { get; set; }
    [JsonPropertyName("address")]
    public string Address { get; set; }
}