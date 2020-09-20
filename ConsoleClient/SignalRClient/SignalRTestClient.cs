using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using ElevatorLib.Dtos;
using Microsoft.AspNetCore.SignalR.Client;
using static System.Console;

namespace ConsoleClient.SignalRClient
{
    public class SignalRTestClient
    {
        private const string Endpoint = "http://localhost:5100/elevator";

        public async  Task Run()
        {
            WriteLine("Press any key to exit.");
            var connection = new HubConnectionBuilder()
                .WithUrl(Endpoint).Build();
            connection.On<BuildingActionSnaphotDto>("SendBuildingState", buildingSnapshot =>
            {
                WriteLine("Received message");
                WriteLine(JsonSerializer.Serialize(buildingSnapshot));
            });
            await connection.StartAsync().ConfigureAwait(false);
            ReadLine();
        }
    }
}
