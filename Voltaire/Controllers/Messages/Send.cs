﻿using Discord.Commands;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Voltaire.Controllers.Messages
{
    class Send
    {
        public static async Task PerformAsync(SocketCommandContext context, string channelName, string message)
        {
            var candidateGuilds = context.Client.Guilds.Where(x => x.Users.ToLookup(u => u.Id)[context.User.Id] != null);
            switch (candidateGuilds.Count())
            {
                case 0:
                    await context.Channel.SendMessageAsync("It doesn't look like you belong to any servers where Voltaire is installed. Please add Voltaire to your desired server.");
                    break;
                case 1:
                    await SendToGuild.LookupAndSendAsync(candidateGuilds.First(), context.Channel, channelName, message);
                    break;
                default:
                    await context.Channel.SendMessageAsync("It looks like you belong to multiple guilds(servers) where Voltaire is installed. Please specify your guild using the following command: `send_guild (guild_name) (channel_name) (message)` ex: `send_guild \"l33t g4amerz\" some-channel you guys suck`");
                    break;
            }
        }
    }
}
