using System.IO;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using System.Net;
using Newtonsoft.Json;
using System;
using ValorantAuth;
using System.Collections.Generic;
using ValorStoreBot;

namespace _02_commands_framework.Modules
{
    public class PublicModule : ModuleBase<SocketCommandContext>
    {
        Program program = new Program();
        [Command("!getoffers")]
        public async Task offersAsync([Remainder] string text)
        {
            IUserMessage responce = await ReplyAsync("Getting store...");
            //Get username and password
            string[] arguments = text.Split(' ');
            string data = Program.ShopRequest(arguments[0], arguments[1]);
            if (data == "nullerror")
            {
                await responce.DeleteAsync();
                await ReplyAsync("Invalid credentials");
            }
            else
            {
                store storeobj = JsonConvert.DeserializeObject<store>(data);
                //Get version to get ID list
                string json = GetRequest("https://valorant-api.com/v1/version");
                version version = JsonConvert.DeserializeObject<version>(json);
                //Get ID list
                string skinJson = Authentication.getSkins(version.data.branch + "-shipping-" + version.data.buildVersion + "-" + version.data.version.Substring(version.data.version.Length - 6));
                IDList idClass = JsonConvert.DeserializeObject<IDList>(skinJson);
                List<string> gunnames = new List<string>();
                //For each skin code, go through the ID list and find the object.
                foreach (string id in storeobj.SkinsPanelLayout.SingleItemOffers)
                {
                    foreach (SkinLevel skin in idClass.SkinLevels)
                    {
                        if (skin.ID.Contains(id, StringComparison.OrdinalIgnoreCase))
                        {
                            gunnames.Add(skin.Name);
                            break;
                        }
                        else
                        {
                            continue;
                        }
                    }
                }
                String[] str = gunnames.ToArray();
                TimeSpan t = TimeSpan.FromSeconds(storeobj.SkinsPanelLayout.SingleItemOffersRemainingDurationInSeconds);
                string timeLeft = string.Format("{0:D2} hours, {1:D2} minutes, and {2:D2} seconds",
                                t.Hours,
                                t.Minutes,
                                t.Seconds);
                await responce.DeleteAsync();
                await ReplyAsync("Your shop contains the "  + str[0] + ", " + str[1] + ", " + str[2] + ", and the " + str[3] + ". There are " +  timeLeft + " left until the shop changes.");
            }
        }

        [Command("!getfeatured")]
        public async Task StoreAsync()
        {
            IUserMessage responce = await ReplyAsync("Getting store...");
            string data = Program.ShopRequest("dev_account_username", "dev_account_password");
            if (data == "nullerror")
            {
                await responce.DeleteAsync();
                await ReplyAsync("Invalid credentials");
            }
            else
            {
                store storeobj = JsonConvert.DeserializeObject<store>(data);
                string json = GetRequest("https://valorant-api.com/v1/bundles/" + storeobj.FeaturedBundle.Bundle.DataAssetID);
                BundleRoot bundle = JsonConvert.DeserializeObject<BundleRoot>(json);
                if (!File.Exists(bundle.data.uuid + ".png"))
                {
                    using (var client = new WebClient())
                    {
                        client.DownloadFile(new Uri(bundle.data.displayIcon), bundle.data.uuid + ".png");
                    }
                }
                TimeSpan t = TimeSpan.FromSeconds(storeobj.FeaturedBundle.BundleRemainingDurationInSeconds);
                string timeLeft = string.Format("{0:D2} days, {1:D2} hours, {2:D2} minutes, and {3:D2} seconds.", 
                                t.Days,
                                t.Hours,
                                t.Minutes,
                                t.Seconds);
                await responce.DeleteAsync();
                await ReplyAsync("Right now, the " + bundle.data.displayName + " bundle is in the shop. The bundle will be in the shop for " + timeLeft);
                await responce.Channel.SendFileAsync(bundle.data.uuid + ".png");
            }
        }


        [Command("!help")]
        public async Task helpAsync()
        {
            await ReplyAsync("Type \"!getoffers {username} {password}\" to get your Valorant daily shop. You can also type \"!getfeatured\" to get the featured bundle.");
        }
        public string GetRequest(string uri)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);
            request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;

            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                return reader.ReadToEnd();
            }
        }
    }
}
