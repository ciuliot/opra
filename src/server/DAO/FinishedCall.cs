using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Text.Json.Serialization;

namespace OP.RemoteAdvisor.DAO
{
  public class FinishedCall
  {
    [BsonId]
    public ObjectId Id { get; set; }

    [BsonElement("started")]
    public DateTime Started { get; set; }

    [BsonElement("kioskId")]
    [JsonIgnore]
    public ObjectId KioskId { get; set; }

    [JsonPropertyName("kioskId")]
    public string KioskIdString { get { return KioskId.ToString(); } }

    [BsonElement("language")]
    public string Language { get; set; }

    [BsonElement("agentId")]
    [JsonIgnore]
    public ObjectId AgentId { get; set; }

    [JsonPropertyName("agentId")]
    public string AgentIdString { get { return AgentId.ToString(); } }

    [BsonElement("ended")]
    public DateTime Ended { get; set; }

    [BsonElement("workflowType")]
    public string WorkflowType { get; set; }
  }
}