using System;
using System.Collections.Generic;
using System.Text;

namespace ValorStoreBot
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
    public class Datastore
    {
        public string uuid { get; set; }
        public string displayName { get; set; }
        public string description { get; set; }
        public string displayIcon { get; set; }
        public string displayIcon2 { get; set; }
        public string verticalPromoImage { get; set; }
        public string assetPath { get; set; }
    }

    public class Rootstore
    {
        public int status { get; set; }
        public Datastore data { get; set; }
    }


    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
    public class Datammr
    {
        public int currenttier { get; set; }
        public string currenttierpatched { get; set; }
        public int ranking_in_tier { get; set; }
        public int mmr_change_to_last_game { get; set; }
        public int elo { get; set; }
    }

    public class Rootmmr
    {
        public string status { get; set; }
        public Datammr data { get; set; }
    }



    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
    public class Item2
    {
        public string ItemTypeID { get; set; }
        public string ItemID { get; set; }
        public int Amount { get; set; }
    }

    public class Item
    {
        public Item Item1 { get; set; }
        public int BasePrice { get; set; }
        public string CurrencyID { get; set; }
        public double DiscountPercent { get; set; }
        public bool IsPromoItem { get; set; }
    }

    public class Bundle
    {
        public string ID { get; set; }
        public string DataAssetID { get; set; }
        public string CurrencyID { get; set; }
        public List<Item> Items { get; set; }
    }

    public class FeaturedBundle
    {
        public Bundle Bundle { get; set; }
        public int BundleRemainingDurationInSeconds { get; set; }
    }

    public class SkinsPanelLayout
    {
        public List<string> SingleItemOffers { get; set; }
        public int SingleItemOffersRemainingDurationInSeconds { get; set; }
    }

    public class store
    {
        public FeaturedBundle FeaturedBundle { get; set; }
        public SkinsPanelLayout SkinsPanelLayout { get; set; }
    }

    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
    public class Data
    {
        public string branch { get; set; }
        public string version { get; set; }
        public string buildVersion { get; set; }
        public DateTime buildDate { get; set; }
    }

    public class version
    {
        public int status { get; set; }
        public Data data { get; set; }
    }

    // IDList myDeserializedClass = JsonConvert.DeserializeObject<IDList>(myJsonResponse); 
    public class Character
    {
        public string Name { get; set; }
        public string ID { get; set; }
        public string AssetName { get; set; }
        public bool IsEnabled { get; set; }
    }

    public class Map
    {
        public string Name { get; set; }
        public string ID { get; set; }
        public string AssetName { get; set; }
        public bool IsEnabled { get; set; }
    }

    public class Chroma
    {
        public string Name { get; set; }
        public string ID { get; set; }
        public string AssetName { get; set; }
        public bool IsEnabled { get; set; }
    }

    public class Skin
    {
        public string Name { get; set; }
        public string ID { get; set; }
        public string AssetName { get; set; }
        public bool IsEnabled { get; set; }
    }

    public class SkinLevel
    {
        public string Name { get; set; }
        public string ID { get; set; }
        public string AssetName { get; set; }
        public bool IsEnabled { get; set; }
    }

    public class Attachment
    {
        public string Name { get; set; }
        public string ID { get; set; }
        public string AssetName { get; set; }
        public bool IsEnabled { get; set; }
    }

    public class Equip
    {
        public string Name { get; set; }
        public string ID { get; set; }
        public string AssetName { get; set; }
        public bool IsEnabled { get; set; }
    }

    public class Theme
    {
        public string Name { get; set; }
        public string ID { get; set; }
        public string AssetName { get; set; }
        public bool IsEnabled { get; set; }
    }

    public class GameMode
    {
        public string Name { get; set; }
        public string ID { get; set; }
        public string AssetName { get; set; }
        public bool IsEnabled { get; set; }
    }

    public class Spray
    {
        public string Name { get; set; }
        public string ID { get; set; }
        public string AssetName { get; set; }
        public bool IsEnabled { get; set; }
    }

    public class SprayLevel
    {
        public string Name { get; set; }
        public string ID { get; set; }
        public string AssetName { get; set; }
        public bool IsEnabled { get; set; }
    }

    public class Charm
    {
        public string Name { get; set; }
        public string ID { get; set; }
        public string AssetName { get; set; }
        public bool IsEnabled { get; set; }
    }

    public class CharmLevel
    {
        public string Name { get; set; }
        public string ID { get; set; }
        public string AssetName { get; set; }
        public bool IsEnabled { get; set; }
    }

    public class PlayerCard
    {
        public string Name { get; set; }
        public string ID { get; set; }
        public string AssetName { get; set; }
        public bool IsEnabled { get; set; }
    }

    public class PlayerTitle
    {
        public string Name { get; set; }
        public string ID { get; set; }
        public string AssetName { get; set; }
        public bool IsEnabled { get; set; }
    }

    public class StorefrontItem
    {
        public string Name { get; set; }
        public string ID { get; set; }
        public string AssetName { get; set; }
        public bool IsEnabled { get; set; }
    }

    public class Season
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public bool IsEnabled { get; set; }
        public bool IsActive { get; set; }
        public bool DevelopmentOnly { get; set; }
    }

    public class CompetitiveSeason
    {
        public string ID { get; set; }
        public string SeasonID { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public bool DevelopmentOnly { get; set; }
    }

    public class IDList
    {
        public List<Character> Characters { get; set; }
        public List<Map> Maps { get; set; }
        public List<Chroma> Chromas { get; set; }
        public List<Skin> Skins { get; set; }
        public List<SkinLevel> SkinLevels { get; set; }
        public List<Attachment> Attachments { get; set; }
        public List<Equip> Equips { get; set; }
        public List<Theme> Themes { get; set; }
        public List<GameMode> GameModes { get; set; }
        public List<Spray> Sprays { get; set; }
        public List<SprayLevel> SprayLevels { get; set; }
        public List<Charm> Charms { get; set; }
        public List<CharmLevel> CharmLevels { get; set; }
        public List<PlayerCard> PlayerCards { get; set; }
        public List<PlayerTitle> PlayerTitles { get; set; }
        public List<StorefrontItem> StorefrontItems { get; set; }
        public List<Season> Seasons { get; set; }
        public List<CompetitiveSeason> CompetitiveSeasons { get; set; }
    }
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
    public class BundleData
    {
        public string uuid { get; set; }
        public string displayName { get; set; }
        public string description { get; set; }
        public string displayIcon { get; set; }
        public string displayIcon2 { get; set; }
        public string verticalPromoImage { get; set; }
        public string assetPath { get; set; }
    }

    public class BundleRoot
    {
        public int status { get; set; }
        public BundleData data { get; set; }
    }
}
