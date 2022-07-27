using DSharpPlus;
using DSharpPlus.Entities;
using DSharpPlus.Interactivity;
using DSharpPlus.Interactivity.Enums;
using DSharpPlus.Interactivity.Extensions;

namespace FarmVille_api.src.Main.Model.Utilities
{
    public class EmbedUtilities
    {
        public async Task sendPagination(DiscordChannel channel, string message,
                                        DiscordUser user, DiscordClient client) {

            //create the left and right emojis 
            PaginationEmojis buttons = new PaginationEmojis
            {
                Left = DiscordEmoji.FromName(client, ":arrow_left:"),
                Right = DiscordEmoji.FromName(client, ":arrow_right:"),
                SkipLeft = null,
                SkipRight = null,
                Stop = null

            };

            var interactivity = client.GetInteractivity();
            var pages = interactivity.GeneratePagesInContent(message);

            //send the pages
            await interactivity.SendPaginatedMessageAsync(channel, user, pages, emojis: buttons,
                PaginationBehaviour.Ignore,
                PaginationDeletion.KeepEmojis);

        } 
    }

    

}