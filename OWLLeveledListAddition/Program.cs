using System;
using System.Collections.Generic;
using System.Linq;
using Mutagen.Bethesda;
using Mutagen.Bethesda.Synthesis;
using Mutagen.Bethesda.Skyrim;
using Mutagen.Bethesda.FormKeys.SkyrimSE;
using System.Threading.Tasks;
using Noggog;
using System.Text.RegularExpressions;
using Mutagen.Bethesda.Plugins;
using Mutagen.Bethesda.Plugins.Order;
using System.Xml.Linq;
using Mutagen.Bethesda.Plugins.Cache;
using static Mutagen.Bethesda.Skyrim.Furniture;

namespace OWLLeveledListAddition
{

	public class Program
    {
        public static HashSet<FormKey> ArmourBlacklist { get; set; } = new()
        {
             Skyrim.Armor.ArmorAstrid.FormKey
        };

        public static HashSet<FormKey> WeaponBlacklist { get; set; } = new()
        {
            Skyrim.Weapon.FavorAmrenIronSword.FormKey,
            Skyrim.Weapon.FavorGhorbashAxe.FormKey,
            Skyrim.Weapon.FavorNelacarStaffFear.FormKey,
            Skyrim.Weapon.FavorOengulSword.FormKey,
            Skyrim.Weapon.dunAngisBow.FormKey,
            Skyrim.Weapon.dunAnsilvundGhostblade.FormKey,
            Skyrim.Weapon.dunBlindCliffStaffReward.FormKey,
            Skyrim.Weapon.dunBloatedMansKatana.FormKey,
            Skyrim.Weapon.dunBluePalaceWabbajack.FormKey,
            Skyrim.Weapon.dunClearspringTarnBowOfHunt.FormKey,
            Skyrim.Weapon.dunCrystalDriftCaveStaff.FormKey,
            Skyrim.Weapon.dunDarklightSilviaStaff.FormKey,
            Skyrim.Weapon.dunFolgunthurMikrulSword02.FormKey,
            Skyrim.Weapon.dunFolgunthurMikrulSword03.FormKey,
            Skyrim.Weapon.dunFolgunthurMikrulSword04.FormKey,
            Skyrim.Weapon.dunFolgunthurMikrulSword05.FormKey,
            Skyrim.Weapon.dunFolgunthurMikrulSword06.FormKey,
            Skyrim.Weapon.dunFrostmereCryptPaleBlade01.FormKey,
            Skyrim.Weapon.dunFrostmereCryptPaleBlade02.FormKey,
            Skyrim.Weapon.dunFrostmereCryptPaleBlade03.FormKey,
            Skyrim.Weapon.dunFrostmereCryptPaleBlade04.FormKey,
            Skyrim.Weapon.dunFrostmereCryptPaleBlade05.FormKey,
            Skyrim.Weapon.dunGeirmundSigdisBow02.FormKey,
            Skyrim.Weapon.dunGeirmundSigdisBow03.FormKey,
            Skyrim.Weapon.dunGeirmundSigdisBow04.FormKey,
            Skyrim.Weapon.dunGeirmundSigdisBow05.FormKey,
            Skyrim.Weapon.dunGeirmundSigdisBow06.FormKey,
            Skyrim.Weapon.dunGeirmundSigdisBowIllusion.FormKey,
            Skyrim.Weapon.dunHagsEndDagger.FormKey,
            Skyrim.Weapon.dunHalldirsCairnHalldirsStaff.FormKey,
            Skyrim.Weapon.dunHaltedStreamPoachersAxe.FormKey,
            Skyrim.Weapon.dunHuntersBow.FormKey,
            Skyrim.Weapon.dunKatariahScimitar.FormKey,
            Skyrim.Weapon.dunLiarsRetreatLonghammer.FormKey,
            Skyrim.Weapon.dunLostValleyRedoubtAxe.FormKey,
            Skyrim.Weapon.dunMarkarthWizardSpiderControlStaff.FormKey,
            Skyrim.Weapon.dunMarkarthWizardSpiderControlStaffFake.FormKey,
            Skyrim.Weapon.dunMossMotherValdrDagger.FormKey,
            Skyrim.Weapon.dunPinewoodGroveWoodsmansFriend.FormKey,
            Skyrim.Weapon.dunPOITrollsbane.FormKey,
            Skyrim.Weapon.dunRannveigSildsStaff.FormKey,
            Skyrim.Weapon.dunRedEagleSwordBase.FormKey,
            Skyrim.Weapon.dunRedEagleSwordUpgraded.FormKey,
            Skyrim.Weapon.dunRRHerebanesCourage.FormKey,
            Skyrim.Weapon.dunSaarthalStaffJyrikStaff.FormKey,
            Skyrim.Weapon.dunSilentMoonsEnchIronMace01.FormKey,
            Skyrim.Weapon.dunSilentMoonsEnchIronMace02.FormKey,
            Skyrim.Weapon.dunSilentMoonsEnchIronMace03.FormKey,
            Skyrim.Weapon.dunSilentMoonsEnchIronSword01.FormKey,
            Skyrim.Weapon.dunSilentMoonsEnchIronSword02.FormKey,
            Skyrim.Weapon.dunSilentMoonsEnchIronWarAxe01.FormKey,
            Skyrim.Weapon.dunSilentMoonsEnchIronSword03.FormKey,
            Skyrim.Weapon.dunSilentMoonsEnchIronWarAxe02.FormKey,
            Skyrim.Weapon.dunSilentMoonsEnchIronWarAxe03.FormKey,
            Skyrim.Weapon.dunSilentMoonsEnchSteelMace01.FormKey,
            Skyrim.Weapon.dunSilentMoonsEnchSteelMace02.FormKey,
            Skyrim.Weapon.dunSilentMoonsEnchSteelMace03.FormKey,
            Skyrim.Weapon.dunSilentMoonsEnchSteelSword01.FormKey,
            Skyrim.Weapon.dunSilentMoonsEnchSteelSword02.FormKey,
            Skyrim.Weapon.dunSilentMoonsEnchSteelSword03.FormKey,
            Skyrim.Weapon.dunSilentMoonsEnchSteelWarAxe01.FormKey,
            Skyrim.Weapon.dunSilentMoonsEnchSteelWarAxe02.FormKey,
            Skyrim.Weapon.dunSilentMoonsEnchSteelWarAxe03.FormKey,
            Skyrim.Weapon.dunValthumeDragonPriestStaff.FormKey,
            Skyrim.Weapon.dunVolunruudOkin.FormKey,
            Skyrim.Weapon.dunVolunruudEduj.FormKey,
            Skyrim.Weapon.dunVolunruudPickaxe.FormKey,
            Skyrim.Weapon.dunVolunruudRelic02.FormKey,
            Skyrim.Weapon.dunVolunruudRelic01.FormKey,
            Skyrim.Weapon.DB05ElvenBow.FormKey,
            Skyrim.Weapon.DBAlainAegisbane.FormKey,
            Skyrim.Weapon.DBBladeOfWoeAstrid.FormKey,
            Skyrim.Weapon.DBBladeOfWoeReward.FormKey,
            Skyrim.Weapon.DA02Dagger.FormKey,
            Skyrim.Weapon.DA03RuefulAxe.FormKey,
            Skyrim.Weapon.DA06GiantClub.FormKey,
            Skyrim.Weapon.DA06Hammer.FormKey,
            Skyrim.Weapon.DA06Volendrung.FormKey,
            Skyrim.Weapon.DA07MehrunesRazor.FormKey,
            Skyrim.Weapon.DA08EbonyBlade.FormKey,
            Skyrim.Weapon.DA08RealEbonyBlade.FormKey,
            Skyrim.Weapon.DA09Dawnbreaker.FormKey,
            Skyrim.Weapon.DA10MaceofMolagBal.FormKey,
            Skyrim.Weapon.DA10RustyMace.FormKey,
            Skyrim.Weapon.DA14BrokenRose.FormKey,
            Skyrim.Weapon.DA14DremoraGreatswordFire03.FormKey,
            Skyrim.Weapon.DA14SanguineRose.FormKey,
            Skyrim.Weapon.DA15Wabbajack.FormKey,
            Skyrim.Weapon.DA16SkullofCorruption.FormKey,
            Skyrim.Weapon.MG07DraugrMagicAxe.FormKey,
            Skyrim.Weapon.MG07DraugrMagicBow.FormKey,
            Skyrim.Weapon.MG07DraugrMagicSword.FormKey,
            Skyrim.Weapon.MG07StaffofMagnus.FormKey,
            Skyrim.Weapon.MGRArniel02Staff.FormKey,
            Skyrim.Weapon.MGRitual05Dagger.FormKey,
            Skyrim.Weapon.MGRKeening.FormKey,
            Skyrim.Weapon.MGRKeeningNonPlayable.FormKey,
            Skyrim.Weapon.MQ105PhantomSword.FormKey,
            Skyrim.Weapon.MQ203AkaviriKatana1.FormKey,
            Skyrim.Weapon.MQ203AkaviriKatana2.FormKey,
            Skyrim.Weapon.MQ203AkaviriKatana3.FormKey,
            Skyrim.Weapon.MQ203AkaviriKatana4.FormKey,
            Skyrim.Weapon.MQ203AkaviriKatana5.FormKey,
            Skyrim.Weapon.MQ303DragonPriestStaff.FormKey,
            Skyrim.Weapon.MQ304DraugrBattleAxeTsun01.FormKey,
            Skyrim.Weapon.MQ304DraugrBattleAxeTsun02.FormKey,
            Skyrim.Weapon.MQ304DraugrBattleAxeTsun03.FormKey,
            Skyrim.Weapon.MQ304DraugrBattleAxeTsun04.FormKey,
            Skyrim.Weapon.MS02Shiv.FormKey,
            Skyrim.Weapon.MS06Staff.FormKey,
            Skyrim.Weapon.POIMageBorvirsDagger.FormKey,
            Skyrim.Weapon.POIMageRundisDagger.FormKey,
            Skyrim.Weapon.T03Nettlebane.FormKey,
            Skyrim.Weapon.TG07Chillrend001.FormKey,
            Skyrim.Weapon.TG07Chillrend002.FormKey,
            Skyrim.Weapon.TG07Chillrend003.FormKey,
            Skyrim.Weapon.TG07Chillrend004.FormKey,
            Skyrim.Weapon.TG07Chillrend005.FormKey,
            Skyrim.Weapon.TG07Chillrend006.FormKey,
            Skyrim.Weapon.SSDRocksplinterPickaxe.FormKey,
            Skyrim.Weapon.C06BladeOfYsgramor.FormKey,
            Skyrim.Weapon.C00GiantClub.FormKey,
            Skyrim.Weapon.NightingaleBlade01.FormKey,
            Skyrim.Weapon.NightingaleBlade02.FormKey,
            Skyrim.Weapon.NightingaleBlade03.FormKey,
            Skyrim.Weapon.NightingaleBlade04.FormKey,
            Skyrim.Weapon.NightingaleBlade05.FormKey,
            Skyrim.Weapon.NightingaleBladeNPC.FormKey,
            Skyrim.Weapon.NightingaleBow01.FormKey,
            Skyrim.Weapon.NightingaleBow02.FormKey,
            Skyrim.Weapon.NightingaleBow03.FormKey,
            Skyrim.Weapon.NightingaleBow04.FormKey,
            Skyrim.Weapon.NightingaleBow05.FormKey,
            Skyrim.Weapon.NightingaleBowNPC.FormKey,
            Dawnguard.Weapon.DLC1AkaviriSword.FormKey,
            Dawnguard.Weapon.DLC1AurielsBow.FormKey,
            Dawnguard.Weapon.DLC1FrostGiantClub.FormKey,
            Dawnguard.Weapon.DLC1HarkonsSword.FormKey,
            Dawnguard.Weapon.DLC1LD_AetherialStaff.FormKey,
            Dawnguard.Weapon.DLC1LD_AetheriumCrestNP.FormKey,
            Dawnguard.Weapon.DLC1LD_KatriaBow.FormKey,
            Dawnguard.Weapon.DLC1LD_KatriaBowNP.FormKey,
            Dawnguard.Weapon.DLC1LD_SkyforgeSteelDaggerNP.FormKey,
            Dawnguard.Weapon.DLC1PrelateDagger.FormKey,
            Dawnguard.Weapon.DLC1PrelateMace.FormKey,
            Dawnguard.Weapon.DLC1RuunvaldStaff.FormKey,
            Dragonborn.Weapon.dlc2MerchBowOfTheStagPrince.FormKey,
            Dragonborn.Weapon.DLC2BloodskalBlade.FormKey,
            Dragonborn.Weapon.DLC2dunBrodirGroveStormfang.FormKey,
            Dragonborn.Weapon.DLC2dunHaknirScimitar01.FormKey,
            Dragonborn.Weapon.DLC2dunHaknirScimitar01NP.FormKey,
            Dragonborn.Weapon.DLC2dunHaknirScimitar02.FormKey,
            Dragonborn.Weapon.DLC2dunHaknirScimitar02NP.FormKey,
            Dragonborn.Weapon.DLC2dunKolbjornRalisPickaxe.FormKey,
            Dragonborn.Weapon.DLC2Horksbane.FormKey,
            Dragonborn.Weapon.DLC2RR01FalxWarhammer.FormKey,
            Dragonborn.Weapon.DLC2RR03NordPickaxe.FormKey,
            Dragonborn.Weapon.DLC2MKMiraakFightStaff1.FormKey,
            Dragonborn.Weapon.DLC2MKMiraakFightStaff2.FormKey,
            Dragonborn.Weapon.DLC2MKMiraakFightStaff3.FormKey,
            Dragonborn.Weapon.DLC2MKMiraakFightStaff4.FormKey,
            Dragonborn.Weapon.DLC2MKMiraakFightStaff5.FormKey,
            Dragonborn.Weapon.DLC2MKMiraakFightStaff6.FormKey,
            Dragonborn.Weapon.DLC2MKMiraakFightSword1.FormKey,
            Dragonborn.Weapon.DLC2MKMiraakFightSword2.FormKey,
            Dragonborn.Weapon.DLC2MKMiraakFightSword3.FormKey,
            Dragonborn.Weapon.DLC2MKMiraakFightSword4.FormKey,
            Dragonborn.Weapon.DLC2MKMiraakFightSword5.FormKey,
            Dragonborn.Weapon.DLC2MKMiraakFightSword6.FormKey,
            Dragonborn.Weapon.DLC2MKMiraakStaff1.FormKey,
            Dragonborn.Weapon.DLC2MKMiraakStaff2.FormKey,
            Dragonborn.Weapon.DLC2MKMiraakStaff3.FormKey,
            Dragonborn.Weapon.DLC2MKMiraakSword1.FormKey,
            Dragonborn.Weapon.DLC2MKMiraakSword2.FormKey,
            Dragonborn.Weapon.DLC2MKMiraakSword3.FormKey,
            Dragonborn.Weapon.DLC2MiraakStaff.FormKey,
            Dragonborn.Weapon.DLC2KagrumezFateBow01.FormKey
        };

        public static Lazy<Settings> _settings = null!;
        public static Settings Settings => _settings.Value;

        public static async Task<int> Main(string[] args)
        {
            return await SynthesisPipeline.Instance
                .AddPatch<ISkyrimMod, ISkyrimModGetter>(RunPatch)
                .SetAutogeneratedSettings("Settings", "settings.json", out _settings)
                .SetTypicalOpen(GameRelease.SkyrimSE, "SynOWLLeveledListsAdditions.esp")
                .Run(args);
        }

        public static void RunPatch(IPatcherState<ISkyrimMod, ISkyrimModGetter> state)
        {
            static string GetSpecialTypeFromKeywords(IWeaponGetter wpn)
            {
                string type = "";

                if (wpn.HasKeyword(Skyrim.Keyword.WeapTypeDagger)
                    || wpn.HasKeyword(Skyrim.Keyword.WeapTypeSword)
                    || wpn.HasKeyword(Skyrim.Keyword.WeapTypeMace)
                    || wpn.HasKeyword(Skyrim.Keyword.WeapTypeWarAxe))
                {
                    type = "1H";
                }
                else if (wpn.HasKeyword(Skyrim.Keyword.WeapTypeWarhammer)
                        || wpn.HasKeyword(Skyrim.Keyword.WeapTypeBattleaxe)
                        || wpn.HasKeyword(Skyrim.Keyword.WeapTypeGreatsword))
                {
                    type = "2H";
                }
                else if (wpn.HasKeyword(Skyrim.Keyword.WeapTypeBow))
                {
                    type = "Bow";
                }
                else if (wpn.HasKeyword(Skyrim.Keyword.VendorItemArrow))
                {
                    type = "Arrow";
                }

                return type;
            }

            // Get OWL main
            if (!state.LoadOrder.TryGetValue("Open World Loot.esp", out var OWL))
            {
                System.Console.WriteLine("'Open World Loot.esp' not found");
                return;
            }

            System.Console.WriteLine("'Open World Loot.esp' found");

            // check if Legacy of the Dragonborn is there
            if (state.LoadOrder.TryGetValue("LegacyoftheDragonborn.esm", out var DBM))
            {
                System.Console.WriteLine("Legacy of the dragonborn was found! It will be ignored!");
            }


            // Initialise the blacklists
            foreach (var weapon in Settings.BlacklistedWeapons)
            {
                WeaponBlacklist.Add(weapon.FormKey);
            }
            foreach (var armour in Settings.BlacklistedArmours)
            {
                ArmourBlacklist.Add(armour.FormKey);
            }

            // Vanilla mods
            HashSet<ModKey> vanillaMods = new() {
                Skyrim.ModKey,
                Update.ModKey,
                Dawnguard.ModKey,
                HearthFires.ModKey,
                Dragonborn.ModKey
            };

            // Weapon material keywords
            HashSet<IFormLinkGetter<IKeywordGetter>> weaponMaterialKeywords = new()
            {
                Skyrim.Keyword.WeapMaterialIron,
                Skyrim.Keyword.WeapMaterialSteel,
                Skyrim.Keyword.WeapMaterialOrcish,
                Skyrim.Keyword.WeapMaterialDwarven,
                Skyrim.Keyword.WeapMaterialElven,
                Skyrim.Keyword.WeapMaterialGlass,
                Skyrim.Keyword.WeapMaterialEbony,
                Skyrim.Keyword.WeapMaterialDaedric,
                Skyrim.Keyword.WeapMaterialImperial,
                Skyrim.Keyword.WeapMaterialSilver,
                //Dragonborn.Keyword.WeapMaterialForsworn,
                Skyrim.Keyword.WeapMaterialFalmer,
                Skyrim.Keyword.WeapMaterialDraugrHoned,
                Skyrim.Keyword.WeapMaterialDraugr,
                Skyrim.Keyword.WeapMaterialSilver,
                Dawnguard.Keyword.DLC1WeapMaterialDragonbone,
                Dragonborn.Keyword.DLC2WeaponMaterialNordic,
                Dragonborn.Keyword.DLC2WeaponMaterialStalhrim
            };

            // Weapon type keywords
            HashSet<IFormLinkGetter<IKeywordGetter>> weaponTypeKeywords = new()
            {
                Skyrim.Keyword.WeapTypeDagger,
                Skyrim.Keyword.WeapTypeSword,
                Skyrim.Keyword.WeapTypeMace,
                Skyrim.Keyword.WeapTypeWarAxe,
                Skyrim.Keyword.WeapTypeBow,
                Skyrim.Keyword.WeapTypeWarhammer,
                Skyrim.Keyword.WeapTypeBattleaxe,
                Skyrim.Keyword.WeapTypeGreatsword,
                Skyrim.Keyword.VendorItemArrow
            };

            // Armour material keywords
            HashSet<IFormLinkGetter<IKeywordGetter>> armourMaterialKeywords = new()
            {
                Skyrim.Keyword.ArmorMaterialIron,
                Skyrim.Keyword.ArmorMaterialSteel,
                Skyrim.Keyword.ArmorMaterialSteelPlate,
                Skyrim.Keyword.ArmorMaterialOrcish,
                Skyrim.Keyword.ArmorMaterialDwarven,
                Skyrim.Keyword.ArmorMaterialElven,
                Skyrim.Keyword.ArmorMaterialElvenGilded,
                Skyrim.Keyword.ArmorMaterialGlass,
                Skyrim.Keyword.ArmorMaterialEbony,
                Skyrim.Keyword.WeapMaterialFalmer,
                Skyrim.Keyword.ArmorMaterialHide,
                Skyrim.Keyword.ArmorMaterialLeather,
                Skyrim.Keyword.ArmorMaterialScaled,
                Skyrim.Keyword.ArmorMaterialDaedric,
                Skyrim.Keyword.ArmorMaterialDragonplate,
                Skyrim.Keyword.ArmorMaterialDragonscale,
                Skyrim.Keyword.ArmorMaterialImperialHeavy,
                Skyrim.Keyword.ArmorMaterialImperialStudded,
                Skyrim.Keyword.ArmorMaterialImperialLight,
                Skyrim.Keyword.ArmorMaterialIronBanded,
                Skyrim.Keyword.ArmorMaterialStormcloak,
                Dragonborn.Keyword.DLC2ArmorMaterialNordicHeavy,
                Dragonborn.Keyword.DLC2ArmorMaterialNordicLight,
                Dragonborn.Keyword.DLC2ArmorMaterialStalhrimHeavy,
                Dragonborn.Keyword.DLC2ArmorMaterialStalhrimLight,
                Dragonborn.Keyword.DLC2ArmorMaterialChitinHeavy,
                Dragonborn.Keyword.DLC2ArmorMaterialChitinLight,
                Dragonborn.Keyword.DLC2ArmorMaterialBonemoldHeavy,
                Dragonborn.Keyword.DLC2ArmorMaterialBonemoldLight
            };

            // Armour type keywords
            HashSet<IFormLinkGetter<IKeywordGetter>> armourTypeKeywords = new()
            {
                Skyrim.Keyword.ArmorBoots,
                Skyrim.Keyword.ArmorCuirass,
                Skyrim.Keyword.ArmorShield,
                Skyrim.Keyword.ArmorGauntlets,
                Skyrim.Keyword.ArmorHelmet,
                Skyrim.Keyword.WeapTypeBow
            };

            // Counters
            int count1 = 0;
            int count2 = 0;
            int count3 = 0;

            System.Console.WriteLine("Starting Patching!");

            // Create a mod-independent list of entries to add to the OWL lists
            Dictionary<string, HashSet<LeveledItemEntry>> leveledItemsToAdd = new();

            // Create a mod-dependent list of entries to add to the OWL lists
            Dictionary<Tuple<ModKey, string>, HashSet<LeveledItemEntry>> leveledItemsToAddPerMod = new();


            // Ignore vanilla
            var loadorder = state.LoadOrder.PriorityOrder;
            if (Settings.IgnoreVanilla)
            {
                loadorder = state.LoadOrder.PriorityOrder.Where(x => !vanillaMods.Contains(x.ModKey));
            }

            // Ignore blacklisted mods
            if(Settings.BlacklistedMods.Count > 0)
            {
                loadorder = loadorder.Where(x => !Settings.BlacklistedMods.Contains(x.ModKey));
            }

            // Iterate on all weapons
            foreach (var weaponGetter in loadorder.WinningOverrides<IWeaponGetter>())
            {
                // Ignore no keywords
                if (weaponGetter.Keywords is null) continue;

                // Ignore enchanted
                if (!weaponGetter.ObjectEffect.IsNull) continue;

                // Ignore daedric artifacts
                if (weaponGetter.HasKeyword(Skyrim.Keyword.VendorItemDaedricArtifact)) continue;

                // Ignore non playable
                if (weaponGetter.Data is not null && weaponGetter.Data.Flags.HasFlag(WeaponData.Flag.NonPlayable)) continue;

                // Ignore can't drop
                if (weaponGetter.Data is not null && weaponGetter.Data.Flags.HasFlag(WeaponData.Flag.CantDrop)) continue;

                // Ignore Legacy of the dragonborn items
                if (DBM is not null && weaponGetter.FormKey.ModKey.Equals(DBM.ModKey)) continue;


                // Ignore vanilla
                if (Settings.IgnoreVanilla && vanillaMods.Contains(weaponGetter.FormKey.ModKey)) continue;

                // Ignore the blacklisted mods
                if (Settings.BlacklistedMods.Contains(weaponGetter.FormKey.ModKey)) continue;

                // Ignore blacklisted weapons
                if (WeaponBlacklist.Contains(weaponGetter.FormKey)) continue;


                string material = "";
                string type = "";

                // Dawnguard
                if (weaponGetter.HasKeyword(Dawnguard.Keyword.DLC1DawnguardItem))
                {
                    material = "Dawnguard";

                    type = GetSpecialTypeFromKeywords(weaponGetter);
                }
                // Silver
                else if (weaponGetter.HasKeyword(Skyrim.Keyword.WeapMaterialSilver))
                {
                    material = "Silver";

                    type = GetSpecialTypeFromKeywords(weaponGetter);
                }
                // Draugr 
                else if (weaponGetter.HasKeyword(Skyrim.Keyword.WeapMaterialDraugr))
                {
                    material = "Draugr";

                    type = GetSpecialTypeFromKeywords(weaponGetter);
                }
                // Draugr Honed
                else if (weaponGetter.HasKeyword(Skyrim.Keyword.WeapMaterialDraugrHoned))
                {
                    material = "DraugrNordHero";

                    type = GetSpecialTypeFromKeywords(weaponGetter);
                }
                // Forsworn
                else if (weaponGetter.EditorID is not null && weaponGetter.EditorID.Contains("forsworn", StringComparison.OrdinalIgnoreCase))
                {
                    material = "Forsworn";

                    type = GetSpecialTypeFromKeywords(weaponGetter);
                }
                // Imperial
                else if (weaponGetter.HasKeyword(Skyrim.Keyword.WeapMaterialImperial)
                    || (weaponGetter.EditorID is not null && weaponGetter.EditorID.Contains("imperial", StringComparison.OrdinalIgnoreCase)))
                {
                    material = "Imperial";

                    type = GetSpecialTypeFromKeywords(weaponGetter);
                }

                else
                {
                    // Search all keywords
                    foreach (var keyword in weaponGetter.Keywords)
                    {
                        if (weaponMaterialKeywords.Contains(keyword))
                        {
                            var kw = keyword.TryResolve(state.LinkCache);
                            if (kw is null || kw.EditorID is null) continue;

                            material = kw.EditorID.Replace("WeapMaterial", "").Replace("DLC2WeaponMaterial", "").Replace("DLC1WeapMaterial","");
                        }
                        else if (weaponTypeKeywords.Contains(keyword))
                        {
                            var kw = keyword.TryResolve(state.LinkCache);
                            if (kw is null || kw.EditorID is null) continue;

                            type = kw.EditorID.Replace("WeapType", "").Replace("VendorItem", "");
                        }
                    }

                }

                // Ignore if either the material or the type is null
                if (material == "" || type == "")
                {
                    //System.Console.WriteLine("> keywords not found: " + material + "/" + type);
                    continue;
                }

                if(Settings.Debug)
                    System.Console.WriteLine("> keywords found ");
                
                // Form the keys for the dictionaries
                string key = material + "_" + type;
                var tuple = new Tuple<ModKey, string>(weaponGetter.FormKey.ModKey, key.ToLower());

                // Create a new leveled item entry
                LeveledItemEntry entry = new()
                {
                    Data = new()
                    {
                        Count = 1,
                        Level = 1,
                        Reference = new FormLink<IWeaponGetter>(weaponGetter.FormKey)
                    }
                };

                // Add the new entry in the mod-dependent dictionary
                leveledItemsToAddPerMod.TryGetValue(tuple, out var entryList);
                if (entryList is null)
                {
                    // new lvlentry
                    leveledItemsToAddPerMod.Add(tuple, new HashSet<LeveledItemEntry>() { entry });
                }
                else
                {
                    if (!entryList.Contains(entry))
                        entryList.Add(entry);
                }

                // Add the entry also to the mod-independent dictionary
                leveledItemsToAdd.TryGetValue(key.ToLower(), out var hash);
                if(hash is null)
                {
                    leveledItemsToAdd.TryAdd(key.ToLower(), new HashSet<LeveledItemEntry>() { entry });
                }
                else
                {
                    hash.Add(entry);
                }

                count1++;
            }
            System.Console.WriteLine("Done with Weapons!");


            if (Settings.DoArmours)
            {
                System.Console.WriteLine("Retrieving armors...");
                // Iterate on all weapons
                foreach (var armourGetter in loadorder.WinningOverrides<IArmorGetter>())
                {
                    // Ignore no keywords
                    if (armourGetter.Keywords is null) continue;

                    // Ignore daedric artifacts
                    if (armourGetter.HasKeyword(Skyrim.Keyword.VendorItemDaedricArtifact)) continue;

                    // Ignore enchanted
                    if (!armourGetter.ObjectEffect.IsNull) continue;

                    // Ignore clothing
                    if (armourGetter.HasKeyword(Skyrim.Keyword.VendorItemClothing)) continue;

                    // Ignore non playable
                    if (armourGetter.MajorFlags.HasFlag(Armor.MajorFlag.NonPlayable)) continue;

                    // Ignore Legacy of the dragonborn items
                    if (DBM is not null && armourGetter.FormKey.ModKey.Equals(DBM.ModKey)) continue;


                    // Ignore vanilla
                    if (Settings.IgnoreVanilla && vanillaMods.Contains(armourGetter.FormKey.ModKey)) continue;

                    // Ignore the blacklisted mods
                    if (Settings.BlacklistedMods.Contains(armourGetter.FormKey.ModKey)) continue;

                    // Ignore blacklisted armours
                    if (ArmourBlacklist.Contains(armourGetter.FormKey)) continue;


                    string material = "";
                    string type = "";

                    // Search all keywords
                    foreach (var keyword in armourGetter.Keywords)
                    {
                        if (armourMaterialKeywords.Contains(keyword))
                        {
                            var kw = keyword.TryResolve(state.LinkCache);
                            if (kw is null || kw.EditorID is null) continue;

                            material = kw.EditorID.Replace("ArmorMaterial", "").Replace("DLC2ArmorMaterial", "").Replace("DLC1ArmorMaterial", "");
                        }
                        else if (armourTypeKeywords.Contains(keyword))
                        {
                            var kw = keyword.TryResolve(state.LinkCache);
                            if (kw is null || kw.EditorID is null) continue;

                            type = kw.EditorID.Replace("Armor", "").Replace("VendorItem", "");
                        }
                    }

                    if (material == "" || type == "")
                    {
                        //System.Console.WriteLine("> keywords not found: " + material + "/" + type);
                        continue;
                    }
                    //System.Console.WriteLine("> keywords found ");

                    // Form the keys for the dictionaries
                    string key = material + "_" + type;
                    var tuple = new Tuple<ModKey, string>(armourGetter.FormKey.ModKey, key.ToLower());

                    // Create a new leveled item entry
                    LeveledItemEntry entry = new()
                    {
                        Data = new()
                        {
                            Count = 1,
                            Level = 1,
                            Reference = new FormLink<IArmorGetter>(armourGetter.FormKey)
                        }
                    };

                    // Add the new entry in the mod-dependent dictionary
                    leveledItemsToAddPerMod.TryGetValue(tuple, out var entryList);
                    if (entryList is null)
                    {
                        // new lvlentry
                        leveledItemsToAddPerMod.Add(tuple, new HashSet<LeveledItemEntry>() { entry });
                    }
                    else
                    {
                        if (!entryList.Contains(entry))
                            entryList.Add(entry);
                    }

                    // Add the entry also to the mod-independent dictionary
                    leveledItemsToAdd.TryGetValue(key.ToLower(), out var hash);
                    if (hash is null)
                    {
                        leveledItemsToAdd.TryAdd(key.ToLower(), new HashSet<LeveledItemEntry>() { entry });
                    }
                    else
                    {
                        hash.Add(entry);
                    }

                    count2++;
                }
                System.Console.WriteLine("Done with Armors!");
            }
            

            // Iterate on the mod-dependent dictionary, to create new leveled lists for the bigger ones
            System.Console.WriteLine("Creating new leveled lists...");
            foreach (var lvlentry in leveledItemsToAddPerMod)
            {
                if (lvlentry.Value.Count > Settings.MinAmountLeveledList)
                {
                    // Create a whole new leveled list
                    var lv = state.PatchMod.LeveledItems.AddNew();
                    
                    // Check if it is a weapon or armour
                    var t1 = lvlentry.Value.First().Data?.Reference.TryResolve<IWeaponGetter>(state.LinkCache);
                    var t2 = lvlentry.Value.First().Data?.Reference.TryResolve<IArmorGetter>(state.LinkCache);
                    if(t1 is null && t2 is null) continue;

                    string recordType = "Armor_";
                    if(t1 is not null)
                    {
                        recordType = "Weapon_";
                    }           

                    // Set the new leveled list values
                    lv.EditorID = "OWL_" + recordType + lvlentry.Key.Item2 + "_" + lvlentry.Key.Item1.Name.ToLower();
                    lv.ChanceNone = 0;
                    lv.Flags.SetFlag(LeveledItem.Flag.CalculateForEachItemInCount, true);
                    lv.Flags.SetFlag(LeveledItem.Flag.CalculateFromAllLevelsLessThanOrEqualPlayer, true);
                    lv.Entries = new();
                    lv.Entries.AddRange(lvlentry.Value);

                    // Create a new leveled list entry with that newly created leveled list
                    LeveledItemEntry entry = new()
                    {
                        Data = new()
                        {
                            Count = 1,
                            Level = 1,
                            Reference = new FormLink<IWeaponGetter>(lv.FormKey)
                        }
                    };

                    // Remove all single entries from the mod-independent list
                    leveledItemsToAdd.TryGetValue(lvlentry.Key.Item2, out var hash);
                    if (hash is null) continue;
                    foreach(var value in lvlentry.Value)
                    {
                        hash.Remove(value);
                    }

                    // Add the newly created leveled list to the the mod-independent list
                    if (hash is null)
                    {
                        leveledItemsToAdd.TryAdd(lvlentry.Key.Item2, new HashSet<LeveledItemEntry>() { entry });
                    }
                    else
                    {
                        hash.Add(entry);
                    }

                    count3++;
                }
            }
            System.Console.WriteLine("Created " + count3 + " new leveled lists!");


            // Iterate on OWL leveled lists
            System.Console.WriteLine("Starting to fill the OWL leveled lists...");
            foreach (var lvlListGetter in state.LoadOrder.PriorityOrder.Where(x => x.ModKey.Equals(OWL.ModKey)).WinningOverrides<ILeveledItemGetter>())
            {
                if (lvlListGetter.EditorID is null) continue;

                if (lvlListGetter.EditorID.StartsWith("OWL_"))
                {
                    var split = lvlListGetter.EditorID.Split('_');
                    string material = "";
                    string type = "";

                    var count = split.Length;

                    // CHECK FOR UNUSED LAST SPLIT

                    // Check that the size of split 
                    if (split.Length != 4) continue;

                    // Form the key
                    material = split[2];
                    type = split[3];
                    string key = material + "_" + type;

                    // Get the items to add to the list
                    leveledItemsToAdd.TryGetValue(key.ToLower(), out var hash);
                    if (hash is null) continue;

                    // Get the leveled list
                    var modifiedList = state.PatchMod.LeveledItems.GetOrAddAsOverride(lvlListGetter);

                    // Add all items to the leveled lists, if not already present
                    foreach(var hashEntry in hash)
                    {
                        if (modifiedList.Entries is not null && modifiedList.Entries.Contains(hashEntry))
                        {
                            if(Settings.Debug)
                                System.Console.WriteLine("Leveled list entry already exists, skipping!");
                        }
                        else
                        {
                            modifiedList.Entries?.Add(hashEntry);
                        }
                    }
                    
                }
            }
            System.Console.WriteLine("Done filling OWL leveled lists!");
            System.Console.WriteLine(count1 + " weapons and " + count2 + " armours were distributed into OWL's leveled lists.");


            System.Console.WriteLine("All done!");
        }
    }
}
