using System;
using System.Data;
using System.Text.Json.Serialization;

namespace DataApiService.Models
{
    /// <summary>
    /// Событие (CommandResults)
    /// </summary>
    public class CommandResults
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonIgnore]
        public DateTime Date { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("parameter_name1")]
        public string ParameterOne { get; set; }

        [JsonPropertyName("parameter_name2")]
        public string ParameterTwo { get; set; }

        [JsonPropertyName("parameter_name3")]
        public string ParameterThree { get; set; }

        [JsonPropertyName("parameter_default_value1")]
        public object ParameterOneDefault { get; set; }

        [JsonPropertyName("parameter_default_value2")]
        public object ParameterTwoDefault { get; set; }

        [JsonPropertyName("parameter_default_value3")]
        public object ParameterThreeDefault { get; set; }

        [JsonPropertyName("visible")]
        public bool Visible { get; set; }
    }

    public class SendCommandResult
    {
        [JsonPropertyName("terminal_id")]
        public int TerminalId { get; set; }

        [JsonPropertyName("user_name")]
        public string UserName { get; set; }

        [JsonPropertyName("command_id")]
        public int CommandId { get; set; }

        [JsonIgnore]
        public string CommandName { get; set; }

        [JsonPropertyName("parameter1")]
        public int Parameter1 { get; set; }

        [JsonPropertyName("parameter2")]
        public int Parameter2 { get; set; }

        [JsonPropertyName("parameter3")]
        public int Parameter3 { get; set; }
        
        [JsonPropertyName("state")]
        public int State { get; set; }

        [JsonPropertyName("state_name")]
        public string StateName { get; set; }

        [JsonPropertyName("time_created")]
        public string TimeCreated { get; set; }

        [JsonPropertyName("time_delivered")]
        public string TimeDelivered { get; set; }

        [JsonPropertyName("id")]
        public int Id { get; set; }
    }
}
