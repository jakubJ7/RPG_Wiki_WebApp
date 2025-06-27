using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RPG_Wiki_WebApp.Data;
using RPG_Wiki_WebApp.Models.Item;
using RPG_Wiki_WebApp.Models.Item.Armor;
using RPG_Wiki_WebApp.Models.Item.Misc;
using RPG_Wiki_WebApp.Models.Item.Weapon;

namespace RPG_Wiki_WebApp.Models
{
    public class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new RPG_Wiki_WebAppContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<RPG_Wiki_WebAppContext>>()))
            {
                InitItemRarity(context);
                InitWeaponType(context);
                InitWeapon(context);
                InitArmorTypes(context);
                InitArmors(context);
                InitMiscTypes(context);
                InitMiscs(context);
            }
        }

        public static async Task SeedRolesAsync(IServiceProvider services)
        {
            var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();

            string[] roles = { "Admin", "User" };

            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
            }

            // Sprawdzenie, czy użytkownik "admin" już istnieje
            var adminUser = await userManager.FindByNameAsync("admin");
            if (adminUser == null)
            {
                adminUser = new ApplicationUser
                {
                    UserName = "admin",
                    Email = "admin@example.com",
                    Nickname = "Administrator"
                };

                var result = await userManager.CreateAsync(adminUser, "admin");

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(adminUser, "Admin");
                    Console.WriteLine("Admin dodany");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        Console.WriteLine($"Błąd: {error.Description}");
                    }
                }
            }
        }

        /// <summary>
        /// Tworzenie danych dla przedmiotów pozostałych
        /// </summary>
        private static void InitMiscs(RPG_Wiki_WebAppContext context)
        {
            // Sprawdzenie czy są
            if (context.Misc.Any())
            {
                return; // DB has been seeded
            }

            context.Misc.AddRange(
                // Golden Skull Token
                new Item.Misc.Misc
                {
                    ItemCode = "misc_goldenskulltoken",
                    Name = "Golden Skull Token",
                    Value = 21,
                    ItemRarity = FindItemRarityByName(context, "Unique"),
                    MiscType = FindMiscTypeByName(context, "Currency")
                },
                // Gold Coin
                new Item.Misc.Misc
                {
                    ItemCode = "misc_goldcoin",
                    Name = "Gold Coin",
                    Value = 1,
                    ItemRarity = FindItemRarityByName(context, "Unique"),
                    MiscType = FindMiscTypeByName(context, "Currency")
                },
                // Rubysilver Ingot
                new Item.Misc.Misc
                {
                    ItemCode = "misc_rubysilveringot",
                    Name = "Rubysilver Ingot",
                    Value = 2,
                    ItemRarity = FindItemRarityByName(context, "Unique"),
                    MiscType = FindMiscTypeByName(context, "Ingot")
                },
                // Gold Ingot
                new Item.Misc.Misc
                {
                    ItemCode = "misc_goldingot",
                    Name = "Gold Ingot",
                    Value = 2,
                    ItemRarity = FindItemRarityByName(context, "Unique"),
                    MiscType = FindMiscTypeByName(context, "Ingot")
                },
                // Cobalt Ingot
                new Item.Misc.Misc
                {
                    ItemCode = "misc_cobaltingot",
                    Name = "Cobalt Ingot",
                    Value = 2,
                    ItemRarity = FindItemRarityByName(context, "Unique"),
                    MiscType = FindMiscTypeByName(context, "Ingot")
                },
                // Ruby Ore
                new Item.Misc.Misc
                {
                    ItemCode = "misc_rubyore",
                    Name = "Ruby Ore",
                    Value = 2,
                    ItemRarity = FindItemRarityByName(context, "Unique"),
                    MiscType = FindMiscTypeByName(context, "Ore")
                },
                // Gold Ore
                new Item.Misc.Misc
                {
                    ItemCode = "misc_goldore",
                    Name = "Gold Ore",
                    Value = 2,
                    ItemRarity = FindItemRarityByName(context, "Unique"),
                    MiscType = FindMiscTypeByName(context, "Ore")
                },
                // Cobalt Ore
                new Item.Misc.Misc
                {
                    ItemCode = "misc_cobaltore",
                    Name = "Cobalt Ore",
                    Value = 2,
                    ItemRarity = FindItemRarityByName(context, "Unique"),
                    MiscType = FindMiscTypeByName(context, "Ore")
                },
                // Simple Gold Bangle
                new Item.Misc.Misc
                {
                    ItemCode = "misc_simplegoldbangle_c",
                    Name = "Simple Gold Bangle",
                    Value = 3,
                    ItemRarity = FindItemRarityByName(context, "Common"),
                    MiscType = FindMiscTypeByName(context, "Treasure")
                },
                new Item.Misc.Misc
                {
                    ItemCode = "misc_simplegoldbangle_u",
                    Name = "Simple Gold Bangle",
                    Value = 9,
                    ItemRarity = FindItemRarityByName(context, "Uncommon"),
                    MiscType = FindMiscTypeByName(context, "Treasure")
                },
                new Item.Misc.Misc
                {
                    ItemCode = "misc_simplegoldbangle_e",
                    Name = "Simple Gold Bangle",
                    Value = 19,
                    ItemRarity = FindItemRarityByName(context, "Epic"),
                    MiscType = FindMiscTypeByName(context, "Treasure")
                },
                new Item.Misc.Misc
                {
                    ItemCode = "misc_simplegoldbangle_r",
                    Name = "Simple Gold Bangle",
                    Value = 32,
                    ItemRarity = FindItemRarityByName(context, "Rare"),
                    MiscType = FindMiscTypeByName(context, "Treasure")
                },
                new Item.Misc.Misc
                {
                    ItemCode = "misc_simplegoldbangle_l",
                    Name = "Simple Gold Bangle",
                    Value = 54,
                    ItemRarity = FindItemRarityByName(context, "Legendary"),
                    MiscType = FindMiscTypeByName(context, "Treasure")
                },
                // Gold Band
                new Item.Misc.Misc
                {
                    ItemCode = "misc_goldband_c",
                    Name = "Gold Band",
                    Value = 3,
                    ItemRarity = FindItemRarityByName(context, "Common"),
                    MiscType = FindMiscTypeByName(context, "Treasure")
                },
                new Item.Misc.Misc
                {
                    ItemCode = "misc_goldband_u",
                    Name = "Gold Band",
                    Value = 9,
                    ItemRarity = FindItemRarityByName(context, "Uncommon"),
                    MiscType = FindMiscTypeByName(context, "Treasure")
                },
                new Item.Misc.Misc
                {
                    ItemCode = "misc_goldband_e",
                    Name = "Gold Band",
                    Value = 18,
                    ItemRarity = FindItemRarityByName(context, "Epic"),
                    MiscType = FindMiscTypeByName(context, "Treasure")
                },
                new Item.Misc.Misc
                {
                    ItemCode = "misc_goldband_r",
                    Name = "Gold Band",
                    Value = 32,
                    ItemRarity = FindItemRarityByName(context, "Rare"),
                    MiscType = FindMiscTypeByName(context, "Treasure")
                },
                new Item.Misc.Misc
                {
                    ItemCode = "misc_goldband_l",
                    Name = "Gold Band",
                    Value = 49,
                    ItemRarity = FindItemRarityByName(context, "Legendary"),
                    MiscType = FindMiscTypeByName(context, "Treasure")
                },
                // Gem Ring
                new Item.Misc.Misc
                {
                    ItemCode = "misc_gemring_c",
                    Name = "Gem Ring",
                    Value = 3,
                    ItemRarity = FindItemRarityByName(context, "Common"),
                    MiscType = FindMiscTypeByName(context, "Treasure")
                },
                new Item.Misc.Misc
                {
                    ItemCode = "misc_gemring_u",
                    Name = "Gem Ring",
                    Value = 9,
                    ItemRarity = FindItemRarityByName(context, "Uncommon"),
                    MiscType = FindMiscTypeByName(context, "Treasure")
                },
                new Item.Misc.Misc
                {
                    ItemCode = "misc_gemring_e",
                    Name = "Gem Ring",
                    Value = 19,
                    ItemRarity = FindItemRarityByName(context, "Epic"),
                    MiscType = FindMiscTypeByName(context, "Treasure")
                },
                new Item.Misc.Misc
                {
                    ItemCode = "misc_gemring_r",
                    Name = "Gem Ring",
                    Value = 32,
                    ItemRarity = FindItemRarityByName(context, "Rare"),
                    MiscType = FindMiscTypeByName(context, "Treasure")
                },
                new Item.Misc.Misc
                {
                    ItemCode = "misc_gemring_l",
                    Name = "Gem Ring",
                    Value = 54,
                    ItemRarity = FindItemRarityByName(context, "Legendary"),
                    MiscType = FindMiscTypeByName(context, "Treasure")
                },
                // Blue Sapphire
                new Item.Misc.Misc
                {
                    ItemCode = "misc_bluesapphire_c",
                    Name = "Blue Sapphire",
                    Value = 8,
                    ItemRarity = FindItemRarityByName(context, "Common"),
                    MiscType = FindMiscTypeByName(context, "Gem")
                },
                new Item.Misc.Misc
                {
                    ItemCode = "misc_bluesapphire_u",
                    Name = "Blue Sapphire",
                    Value = 13,
                    ItemRarity = FindItemRarityByName(context, "Uncommon"),
                    MiscType = FindMiscTypeByName(context, "Gem")
                },
                new Item.Misc.Misc
                {
                    ItemCode = "misc_bluesapphire_e",
                    Name = "Blue Sapphire",
                    Value = 22,
                    ItemRarity = FindItemRarityByName(context, "Epic"),
                    MiscType = FindMiscTypeByName(context, "Gem")
                },
                new Item.Misc.Misc
                {
                    ItemCode = "misc_bluesapphire_r",
                    Name = "Blue Sapphire",
                    Value = 38,
                    ItemRarity = FindItemRarityByName(context, "Rare"),
                    MiscType = FindMiscTypeByName(context, "Gem")
                },
                new Item.Misc.Misc
                {
                    ItemCode = "misc_bluesapphire_l",
                    Name = "Blue Sapphire",
                    Value = 63,
                    ItemRarity = FindItemRarityByName(context, "Legendary"),
                    MiscType = FindMiscTypeByName(context, "Gem")
                },
                // Diamond
                new Item.Misc.Misc 
                {
                    ItemCode = "misc_diamond_c",
                    Name = "Diamond",
                    Value = 9,
                    ItemRarity = FindItemRarityByName(context, "Common"),
                    MiscType = FindMiscTypeByName(context, "Gem")
                },
                new Item.Misc.Misc
                {
                    ItemCode = "misc_diamond_u",
                    Name = "Diamond",
                    Value = 14,
                    ItemRarity = FindItemRarityByName(context, "Uncommon"),
                    MiscType = FindMiscTypeByName(context, "Gem")
                },
                new Item.Misc.Misc
                {
                    ItemCode = "misc_diamond_e",
                    Name = "Diamond",
                    Value = 24,
                    ItemRarity = FindItemRarityByName(context, "Epic"),
                    MiscType = FindMiscTypeByName(context, "Gem")
                },
                new Item.Misc.Misc
                {
                    ItemCode = "misc_diamond_r",
                    Name = "Diamond",
                    Value = 41,
                    ItemRarity = FindItemRarityByName(context, "Rare"),
                    MiscType = FindMiscTypeByName(context, "Gem")
                },
                new Item.Misc.Misc
                {
                    ItemCode = "misc_diamond_l",
                    Name = "Diamond",
                    Value = 68,
                    ItemRarity = FindItemRarityByName(context, "Legendary"),
                    MiscType = FindMiscTypeByName(context, "Gem")
                }
                );

            context.SaveChanges();
        }

        /// <summary>
        /// Tworzenie danych dla przedmiotów pozostałych
        /// </summary>
        private static void InitMiscTypes(RPG_Wiki_WebAppContext context)
        {
            // Sprawdzenie czy są
            if (context.MiscType.Any())
            {
                return; // DB has been seeded
            }

            context.MiscType.AddRange(
                new Item.Misc.MiscType { Name = "Gem" },
                new Item.Misc.MiscType { Name = "Treasure" },
                new Item.Misc.MiscType { Name = "Ore" },
                new Item.Misc.MiscType { Name = "Currency" },
                new Item.Misc.MiscType { Name = "Ingot" }
                );

            context.SaveChanges();
        }

        /// <summary>
        /// Znajdowanie typu przedmiotu
        /// </summary>
        private static MiscType FindMiscTypeByName(RPG_Wiki_WebAppContext context, string miscTypeName)
        {
            var miscType = context.MiscType.FirstOrDefault(type => type.Name == miscTypeName);
            return miscType;
        }

        /// <summary>
        /// Tworzenie danych dla pancerzy
        /// </summary>
        private static void InitArmors(RPG_Wiki_WebAppContext context)
        {
            // Sprawdzenie czy są
            if (context.Armor.Any())
            {
                return; // DB has been seeded
            }

            context.Armor.AddRange(
                // Tattered Cloak
                new Item.Armor.Armor
                {
                    ItemCode = "armor_tatteredcloak_c",
                    Name = "Tattered Cloak",
                    Value = 8,
                    DamageReduction = 6,
                    ItemRarity = FindItemRarityByName(context, "Common"),
                    ArmorType = FindArmorTypeByName(context, "Back")
                },
                new Item.Armor.Armor
                {
                    ItemCode = "armor_tatteredcloak_u",
                    Name = "Tattered Cloak",
                    Value = 12,
                    DamageReduction = 7,
                    ItemRarity = FindItemRarityByName(context, "Uncommon"),
                    ArmorType = FindArmorTypeByName(context, "Back")
                },
                new Item.Armor.Armor
                {
                    ItemCode = "armor_tatteredcloak_r",
                    Name = "Tattered Cloak",
                    Value = 24,
                    DamageReduction = 8,
                    ItemRarity = FindItemRarityByName(context, "Rare"),
                    ArmorType = FindArmorTypeByName(context, "Back")
                }
                , new Item.Armor.Armor
                {
                    ItemCode = "armor_tatteredcloak_e",
                    Name = "Tattered Cloak",
                    Value = 36,
                    DamageReduction = 11,
                    ItemRarity = FindItemRarityByName(context, "Epic"),
                    ArmorType = FindArmorTypeByName(context, "Back")
                },
                new Item.Armor.Armor
                {
                    ItemCode = "armor_tatteredcloak_l",
                    Name = "Tattered Cloak",
                    Value = 68,
                    DamageReduction = 14,
                    ItemRarity = FindItemRarityByName(context, "Legendary"),
                    ArmorType = FindArmorTypeByName(context, "Back")
                },
                // Adventurer Cloak
                new Item.Armor.Armor
                {
                    ItemCode = "armor_adventurercloak_c",
                    Name = "Adventurer Cloak",
                    Value = 8,
                    DamageReduction = 6,
                    ItemRarity = FindItemRarityByName(context, "Common"),
                    ArmorType = FindArmorTypeByName(context, "Back")
                },
                new Item.Armor.Armor
                {
                    ItemCode = "armor_adventurercloak_u",
                    Name = "Adventurer Cloak",
                    Value = 12,
                    DamageReduction = 7,
                    ItemRarity = FindItemRarityByName(context, "Uncommon"),
                    ArmorType = FindArmorTypeByName(context, "Back")
                },
                new Item.Armor.Armor
                {
                    ItemCode = "armor_adventurercloak_r",
                    Name = "Adventurer Cloak",
                    Value = 24,
                    DamageReduction = 8,
                    ItemRarity = FindItemRarityByName(context, "Rare"),
                    ArmorType = FindArmorTypeByName(context, "Back")
                }
                , new Item.Armor.Armor
                {
                    ItemCode = "armor_adventurercloak_e",
                    Name = "Adventurer Cloak",
                    Value = 36,
                    DamageReduction = 11,
                    ItemRarity = FindItemRarityByName(context, "Epic"),
                    ArmorType = FindArmorTypeByName(context, "Back")
                },
                new Item.Armor.Armor
                {
                    ItemCode = "armor_adventurercloak_l",
                    Name = "Adventurer Cloak",
                    Value = 68,
                    DamageReduction = 14,
                    ItemRarity = FindItemRarityByName(context, "Legendary"),
                    ArmorType = FindArmorTypeByName(context, "Back")
                },
                // Lightfoot Boots
                new Item.Armor.Armor
                {
                    ItemCode = "armor_lightfootboots_c",
                    Name = "Lightfoot Boots",
                    Value = 8,
                    DamageReduction = 4,
                    ItemRarity = FindItemRarityByName(context, "Common"),
                    ArmorType = FindArmorTypeByName(context, "Foot")
                },
                new Item.Armor.Armor
                {
                    ItemCode = "armor_lightfootboots_u",
                    Name = "Lightfoot Boots",
                    Value = 12,
                    DamageReduction = 6,
                    ItemRarity = FindItemRarityByName(context, "Uncommon"),
                    ArmorType = FindArmorTypeByName(context, "Foot")
                },
                new Item.Armor.Armor
                {
                    ItemCode = "armor_lightfootboots_r",
                    Name = "Lightfoot Boots",
                    Value = 24,
                    DamageReduction = 7,
                    ItemRarity = FindItemRarityByName(context, "Rare"),
                    ArmorType = FindArmorTypeByName(context, "Foot")
                }
                , new Item.Armor.Armor
                {
                    ItemCode = "armor_lightfootboots_e",
                    Name = "Lightfoot Boots",
                    Value = 36,
                    DamageReduction = 9,
                    ItemRarity = FindItemRarityByName(context, "Epic"),
                    ArmorType = FindArmorTypeByName(context, "Foot")
                },
                new Item.Armor.Armor
                {
                    ItemCode = "armor_lightfootboots_l",
                    Name = "Lightfoot Boots",
                    Value = 68,
                    DamageReduction = 11,
                    ItemRarity = FindItemRarityByName(context, "Legendary"),
                    ArmorType = FindArmorTypeByName(context, "Foot")
                },
                // Heavy Gauntlets
                new Item.Armor.Armor
                {
                    ItemCode = "armor_heavygauntlets_c",
                    Name = "Heavy Gauntlets",
                    Value = 8,
                    DamageReduction = 19,
                    ItemRarity = FindItemRarityByName(context, "Common"),
                    ArmorType = FindArmorTypeByName(context, "Hands")
                },
                new Item.Armor.Armor
                {
                    ItemCode = "armor_heavygauntlets_u",
                    Name = "Heavy Gauntlets",
                    Value = 12,
                    DamageReduction = 22,
                    ItemRarity = FindItemRarityByName(context, "Uncommon"),
                    ArmorType = FindArmorTypeByName(context, "Hands")
                },
                new Item.Armor.Armor
                {
                    ItemCode = "armor_heavygauntlets_r",
                    Name = "Heavy Gauntlets",
                    Value = 24,
                    DamageReduction = 25,
                    ItemRarity = FindItemRarityByName(context, "Rare"),
                    ArmorType = FindArmorTypeByName(context, "Hands")
                }
                , new Item.Armor.Armor
                {
                    ItemCode = "armor_heavygauntlets_e",
                    Name = "Heavy Gauntlets",
                    Value = 36,
                    DamageReduction = 27,
                    ItemRarity = FindItemRarityByName(context, "Epic"),
                    ArmorType = FindArmorTypeByName(context, "Hands")
                },
                new Item.Armor.Armor
                {
                    ItemCode = "armor_heavygauntlets_l",
                    Name = "Heavy Gauntlets",
                    Value = 68,
                    DamageReduction = 30,
                    ItemRarity = FindItemRarityByName(context, "Legendary"),
                    ArmorType = FindArmorTypeByName(context, "Hands")
                },
                // Gloves of Utility
                new Item.Armor.Armor
                {
                    ItemCode = "armor_glovesofutility_c",
                    Name = "Gloves of Utility",
                    Value = 12,
                    DamageReduction = 8,
                    ItemRarity = FindItemRarityByName(context, "Common"),
                    ArmorType = FindArmorTypeByName(context, "Hands")
                },
                new Item.Armor.Armor
                {
                    ItemCode = "armor_glovesofutility_u",
                    Name = "Gloves of Utility",
                    Value = 24,
                    DamageReduction = 10,
                    ItemRarity = FindItemRarityByName(context, "Uncommon"),
                    ArmorType = FindArmorTypeByName(context, "Hands")
                },
                new Item.Armor.Armor
                {
                    ItemCode = "armor_glovesofutility_r",
                    Name = "Gloves of Utility",
                    Value = 36,
                    DamageReduction = 14,
                    ItemRarity = FindItemRarityByName(context, "Rare"),
                    ArmorType = FindArmorTypeByName(context, "Hands")
                }
                , new Item.Armor.Armor
                {
                    ItemCode = "armor_glovesofutility_e",
                    Name = "Gloves of Utility",
                    Value = 60,
                    DamageReduction = 15,
                    ItemRarity = FindItemRarityByName(context, "Epic"),
                    ArmorType = FindArmorTypeByName(context, "Hands")
                },
                new Item.Armor.Armor
                {
                    ItemCode = "armor_glovesofutility_l",
                    Name = "Gloves of Utility",
                    Value = 120,
                    DamageReduction = 18,
                    ItemRarity = FindItemRarityByName(context, "Legendary"),
                    ArmorType = FindArmorTypeByName(context, "Hands")
                },
                // Loose Trousers
                new Item.Armor.Armor
                {
                    ItemCode = "armor_loosetrousers_c",
                    Name = "Loose Trousers",
                    Value = 12,
                    DamageReduction = 10,
                    ItemRarity = FindItemRarityByName(context, "Common"),
                    ArmorType = FindArmorTypeByName(context, "Legs")
                },
                new Item.Armor.Armor
                {
                    ItemCode = "armor_loosetrousers_u",
                    Name = "Loose Trousers",
                    Value = 24,
                    DamageReduction = 12,
                    ItemRarity = FindItemRarityByName(context, "Uncommon"),
                    ArmorType = FindArmorTypeByName(context, "Legs")
                },
                new Item.Armor.Armor
                {
                    ItemCode = "armor_loosetrousers_r",
                    Name = "Loose Trousers",
                    Value = 36,
                    DamageReduction = 16,
                    ItemRarity = FindItemRarityByName(context, "Rare"),
                    ArmorType = FindArmorTypeByName(context, "Legs")
                }
                , new Item.Armor.Armor
                {
                    ItemCode = "armor_loosetrousers_e",
                    Name = "Loose Trousers",
                    Value = 60,
                    DamageReduction = 19,
                    ItemRarity = FindItemRarityByName(context, "Epic"),
                    ArmorType = FindArmorTypeByName(context, "Legs")
                },
                new Item.Armor.Armor
                {
                    ItemCode = "armor_loosetrousers_l",
                    Name = "Loose Trousers",
                    Value = 120,
                    DamageReduction = 22,
                    ItemRarity = FindItemRarityByName(context, "Legendary"),
                    ArmorType = FindArmorTypeByName(context, "Legs")
                },
                // Cloth Pants
                new Item.Armor.Armor
                {
                    ItemCode = "armor_clothpants_c",
                    Name = "Cloth Pants",
                    Value = 12,
                    DamageReduction = 16,
                    ItemRarity = FindItemRarityByName(context, "Common"),
                    ArmorType = FindArmorTypeByName(context, "Legs")
                },
                new Item.Armor.Armor
                {
                    ItemCode = "armor_clothpants_u",
                    Name = "Cloth Pants",
                    Value = 24,
                    DamageReduction = 18,
                    ItemRarity = FindItemRarityByName(context, "Uncommon"),
                    ArmorType = FindArmorTypeByName(context, "Legs")
                },
                new Item.Armor.Armor
                {
                    ItemCode = "armor_clothpants_r",
                    Name = "Cloth Pants",
                    Value = 36,
                    DamageReduction = 20,
                    ItemRarity = FindItemRarityByName(context, "Rare"),
                    ArmorType = FindArmorTypeByName(context, "Legs")
                }
                , new Item.Armor.Armor
                {
                    ItemCode = "armor_clothpants_e",
                    Name = "Cloth Pants",
                    Value = 60,
                    DamageReduction = 22,
                    ItemRarity = FindItemRarityByName(context, "Epic"),
                    ArmorType = FindArmorTypeByName(context, "Legs")
                },
                new Item.Armor.Armor
                {
                    ItemCode = "armor_clothpants_l",
                    Name = "Cloth Pants",
                    Value = 120,
                    DamageReduction = 24,
                    ItemRarity = FindItemRarityByName(context, "Legendary"),
                    ArmorType = FindArmorTypeByName(context, "Legs")
                },
                // Oracle Robe
                new Item.Armor.Armor
                {
                    ItemCode = "armor_oraclerobe_c",
                    Name = "Oracle Robe",
                    Value = 12,
                    DamageReduction = 31,
                    ItemRarity = FindItemRarityByName(context, "Common"),
                    ArmorType = FindArmorTypeByName(context, "Chest")
                },
                new Item.Armor.Armor
                {
                    ItemCode = "armor_oraclerobe_u",
                    Name = "Oracle Robe",
                    Value = 24,
                    DamageReduction = 33,
                    ItemRarity = FindItemRarityByName(context, "Uncommon"),
                    ArmorType = FindArmorTypeByName(context, "Chest")
                },
                new Item.Armor.Armor
                {
                    ItemCode = "armor_oraclerobe_r",
                    Name = "Oracle Robe",
                    Value = 36,
                    DamageReduction = 34,
                    ItemRarity = FindItemRarityByName(context, "Rare"),
                    ArmorType = FindArmorTypeByName(context, "Chest")
                }
                , new Item.Armor.Armor
                {
                    ItemCode = "armor_oraclerobe_e",
                    Name = "Oracle Robe",
                    Value = 60,
                    DamageReduction = 36,
                    ItemRarity = FindItemRarityByName(context, "Epic"),
                    ArmorType = FindArmorTypeByName(context, "Chest")
                },
                new Item.Armor.Armor
                {
                    ItemCode = "armor_oraclerobe_l",
                    Name = "Oracle Robe",
                    Value = 120,
                    DamageReduction = 39,
                    ItemRarity = FindItemRarityByName(context, "Legendary"),
                    ArmorType = FindArmorTypeByName(context, "Chest")
                },
                // Frock
                new Item.Armor.Armor
                {
                    ItemCode = "armor_frock_c",
                    Name = "Frock",
                    Value = 12,
                    DamageReduction = 28,
                    ItemRarity = FindItemRarityByName(context, "Common"),
                    ArmorType = FindArmorTypeByName(context, "Chest")
                },
                new Item.Armor.Armor
                {
                    ItemCode = "armor_frock_u",
                    Name = "Frock",
                    Value = 24,
                    DamageReduction = 30,
                    ItemRarity = FindItemRarityByName(context, "Uncommon"),
                    ArmorType = FindArmorTypeByName(context, "Chest")
                },
                new Item.Armor.Armor
                {
                    ItemCode = "armor_frock_r",
                    Name = "Frock",
                    Value = 36,
                    DamageReduction = 33,
                    ItemRarity = FindItemRarityByName(context, "Rare"),
                    ArmorType = FindArmorTypeByName(context, "Chest")
                }
                , new Item.Armor.Armor
                {
                    ItemCode = "armor_frock_e",
                    Name = "Frock",
                    Value = 60,
                    DamageReduction = 34,
                    ItemRarity = FindItemRarityByName(context, "Epic"),
                    ArmorType = FindArmorTypeByName(context, "Chest")
                },
                new Item.Armor.Armor
                {
                    ItemCode = "armor_frock_l",
                    Name = "Frock",
                    Value = 120,
                    DamageReduction = 36,
                    ItemRarity = FindItemRarityByName(context, "Legendary"),
                    ArmorType = FindArmorTypeByName(context, "Chest")
                },
                // Champion Armor
                new Item.Armor.Armor
                {
                    ItemCode = "armor_championarmor_c",
                    Name = "Champion Armor",
                    Value = 12,
                    DamageReduction = 65,
                    ItemRarity = FindItemRarityByName(context, "Common"),
                    ArmorType = FindArmorTypeByName(context, "Chest")
                },
                new Item.Armor.Armor
                {
                    ItemCode = "armor_championarmor_u",
                    Name = "Champion Armor",
                    Value = 24,
                    DamageReduction = 69,
                    ItemRarity = FindItemRarityByName(context, "Uncommon"),
                    ArmorType = FindArmorTypeByName(context, "Chest")
                },
                new Item.Armor.Armor
                {
                    ItemCode = "armor_championarmor_r",
                    Name = "Champion Armor",
                    Value = 36,
                    DamageReduction = 71,
                    ItemRarity = FindItemRarityByName(context, "Rare"),
                    ArmorType = FindArmorTypeByName(context, "Chest")
                }
                , new Item.Armor.Armor
                {
                    ItemCode = "armor_championarmor_e",
                    Name = "Champion Armor",
                    Value = 60,
                    DamageReduction = 74,
                    ItemRarity = FindItemRarityByName(context, "Epic"),
                    ArmorType = FindArmorTypeByName(context, "Chest")
                },
                new Item.Armor.Armor
                {
                    ItemCode = "armor_championarmor_l",
                    Name = "Champion Armor",
                    Value = 120,
                    DamageReduction = 79,
                    ItemRarity = FindItemRarityByName(context, "Legendary"),
                    ArmorType = FindArmorTypeByName(context, "Chest")
                },
                // Forest Hood
                new Item.Armor.Armor
                {
                    ItemCode = "armor_foresthood_c",
                    Name = "Forest Hood",
                    Value = 8,
                    DamageReduction = 14,
                    ItemRarity = FindItemRarityByName(context, "Common"),
                    ArmorType = FindArmorTypeByName(context, "Head")
                },
                new Item.Armor.Armor
                {
                    ItemCode = "armor_foresthood_u",
                    Name = "Forest Hood",
                    Value = 12,
                    DamageReduction = 15,
                    ItemRarity = FindItemRarityByName(context, "Uncommon"),
                    ArmorType = FindArmorTypeByName(context, "Head")
                },
                new Item.Armor.Armor
                {
                    ItemCode = "armor_foresthood_r",
                    Name = "Forest Hood",
                    Value = 24,
                    DamageReduction = 17,
                    ItemRarity = FindItemRarityByName(context, "Rare"),
                    ArmorType = FindArmorTypeByName(context, "Head")
                }
                , new Item.Armor.Armor
                {
                    ItemCode = "armor_foresthood_e",
                    Name = "Forest Hood",
                    Value = 40,
                    DamageReduction = 19,
                    ItemRarity = FindItemRarityByName(context, "Epic"),
                    ArmorType = FindArmorTypeByName(context, "Head")
                },
                new Item.Armor.Armor
                {
                    ItemCode = "armor_foresthood_l",
                    Name = "Forest Hood",
                    Value = 80,
                    DamageReduction = 21,
                    ItemRarity = FindItemRarityByName(context, "Legendary"),
                    ArmorType = FindArmorTypeByName(context, "Head")
                },
                // Chapel De Fer
                new Item.Armor.Armor
                {
                    ItemCode = "armor_chapeldefer_c",
                    Name = "Chapel De Fer",
                    Value = 8,
                    DamageReduction = 23,
                    ItemRarity = FindItemRarityByName(context, "Common"),
                    ArmorType = FindArmorTypeByName(context, "Head")
                },
                new Item.Armor.Armor
                {
                    ItemCode = "armor_chapeldefer_u",
                    Name = "Chapel De Fer",
                    Value = 12,
                    DamageReduction = 25,
                    ItemRarity = FindItemRarityByName(context, "Uncommon"),
                    ArmorType = FindArmorTypeByName(context, "Head")
                },
                new Item.Armor.Armor
                {
                    ItemCode = "armor_chapeldefer_r",
                    Name = "Chapel De Fer",
                    Value = 24,
                    DamageReduction = 26,
                    ItemRarity = FindItemRarityByName(context, "Rare"),
                    ArmorType = FindArmorTypeByName(context, "Head")
                }
                , new Item.Armor.Armor
                {
                    ItemCode = "armor_chapeldefer_e",
                    Name = "Chapel De Fer",
                    Value = 40,
                    DamageReduction = 29,
                    ItemRarity = FindItemRarityByName(context, "Epic"),
                    ArmorType = FindArmorTypeByName(context, "Head")
                },
                new Item.Armor.Armor
                {
                    ItemCode = "armor_chapeldefer_l",
                    Name = "Chapel De Fer",
                    Value = 80,
                    DamageReduction = 30,
                    ItemRarity = FindItemRarityByName(context, "Legendary"),
                    ArmorType = FindArmorTypeByName(context, "Head")
                },
                // Armet
                new Item.Armor.Armor 
                {
                    ItemCode = "armor_armet_c",
                    Name = "Armet",
                    Value = 8,
                    DamageReduction = 33,
                    ItemRarity = FindItemRarityByName(context, "Common"),
                    ArmorType = FindArmorTypeByName(context, "Head")
                },
                new Item.Armor.Armor
                {
                    ItemCode = "armor_armet_u",
                    Name = "Armet",
                    Value = 12,
                    DamageReduction = 35,
                    ItemRarity = FindItemRarityByName(context, "Uncommon"),
                    ArmorType = FindArmorTypeByName(context, "Head")
                },
                new Item.Armor.Armor
                {
                    ItemCode = "armor_armet_r",
                    Name = "Armet",
                    Value = 24,
                    DamageReduction = 37,
                    ItemRarity = FindItemRarityByName(context, "Rare"),
                    ArmorType = FindArmorTypeByName(context, "Head")
                }
                , new Item.Armor.Armor
                {
                    ItemCode = "armor_armet_e",
                    Name = "Armet",
                    Value = 40,
                    DamageReduction = 38,
                    ItemRarity = FindItemRarityByName(context, "Epic"),
                    ArmorType = FindArmorTypeByName(context, "Head")
                },
                new Item.Armor.Armor
                {
                    ItemCode = "armor_armet_l",
                    Name = "Armet",
                    Value = 80,
                    DamageReduction = 40,
                    ItemRarity = FindItemRarityByName(context, "Legendary"),
                    ArmorType = FindArmorTypeByName(context, "Head")
                }
                );
            
            context.SaveChanges();
        }

        /// <summary>
        /// Tworzenie typow dla pancerzy
        /// </summary>
        private static void InitArmorTypes(RPG_Wiki_WebAppContext context)
        {
            // Sprawdzenie czy są
            if (context.ArmorType.Any())
            {
                return; // DB has been seeded
            }

            context.ArmorType.AddRange(
                new Item.Armor.ArmorType { Name = "Head" },
                new Item.Armor.ArmorType { Name = "Chest" },
                new Item.Armor.ArmorType { Name = "Legs" },
                new Item.Armor.ArmorType { Name = "Hands" },
                new Item.Armor.ArmorType { Name = "Foot" },
                new Item.Armor.ArmorType { Name = "Back" }
                );

            context.SaveChanges();
        }

        /// <summary>
        /// Znajdowanie typu broni
        /// </summary>
        private static ArmorType FindArmorTypeByName(RPG_Wiki_WebAppContext context, string armorTypeName)
        {
            var armorType = context.ArmorType.FirstOrDefault(type => type.Name == armorTypeName);
            return armorType;
        }

        /// <summary>
        /// Tworzenie danych z broniami
        /// </summary>
        private static void InitWeapon(RPG_Wiki_WebAppContext context)
        {
            // Sprawdzenie czy są 
            if (context.Weapon.Any())
            {
                return;   // DB has been seeded
            }

            context.Weapon.AddRange(
                // Longbow
                new Item.Weapon.Weapon
                {
                    ItemCode = "weapon_longbow_c",
                    Name = "Longbow",
                    Value = 8,
                    MinDamage = 34,
                    MaxDamage = 35,
                    ItemRarity = FindItemRarityByName(context, "Common"),
                    WeaponType = FindWeaponTypeByName(context, "Bow")
                },
                new Item.Weapon.Weapon
                {
                    ItemCode = "weapon_longbow_u",
                    Name = "Longbow",
                    Value = 12,
                    MinDamage = 36,
                    MaxDamage = 37,
                    ItemRarity = FindItemRarityByName(context, "Uncommon"),
                    WeaponType = FindWeaponTypeByName(context, "Bow")
                },
                new Item.Weapon.Weapon
                {
                    ItemCode = "weapon_longbow_r",
                    Name = "Longbow",
                    Value = 24,
                    MinDamage = 38,
                    MaxDamage = 40,
                    ItemRarity = FindItemRarityByName(context, "Rare"),
                    WeaponType = FindWeaponTypeByName(context, "Bow")
                },
                new Item.Weapon.Weapon
                {
                    ItemCode = "weapon_longbow_e",
                    Name = "Longbow",
                    Value = 48,
                    MinDamage = 41,
                    MaxDamage = 42,
                    ItemRarity = FindItemRarityByName(context, "Epic"),
                    WeaponType = FindWeaponTypeByName(context, "Bow")
                },
                new Item.Weapon.Weapon
                {
                    ItemCode = "weapon_longbow_l",
                    Name = "Longbow",
                    Value = 120,
                    MinDamage = 43,
                    MaxDamage = 46,
                    ItemRarity = FindItemRarityByName(context, "Legendary"),
                    WeaponType = FindWeaponTypeByName(context, "Bow")
                },
                // Morning Star
                new Item.Weapon.Weapon
                {
                    ItemCode = "weapon_morningstar_c",
                    Name = "Morning Star",
                    Value = 8,
                    MinDamage = 33,
                    MaxDamage = 34,
                    ItemRarity = FindItemRarityByName(context, "Common"),
                    WeaponType = FindWeaponTypeByName(context, "Mace")
                },
                new Item.Weapon.Weapon
                {
                    ItemCode = "weapon_morningstar_u",
                    Name = "Morning Star",
                    Value = 12,
                    MinDamage = 35,
                    MaxDamage = 36,
                    ItemRarity = FindItemRarityByName(context, "Uncommon"),
                    WeaponType = FindWeaponTypeByName(context, "Mace")
                },
                new Item.Weapon.Weapon
                {
                    ItemCode = "weapon_morningstar_r",
                    Name = "Morning Star",
                    Value = 24,
                    MinDamage = 37,
                    MaxDamage = 39,
                    ItemRarity = FindItemRarityByName(context, "Rare"),
                    WeaponType = FindWeaponTypeByName(context, "Mace")
                },
                new Item.Weapon.Weapon
                {
                    ItemCode = "weapon_morningstar_e",
                    Name = "Morning Star",
                    Value = 48,
                    MinDamage = 40,
                    MaxDamage = 42,
                    ItemRarity = FindItemRarityByName(context, "Epic"),
                    WeaponType = FindWeaponTypeByName(context, "Mace")
                },
                new Item.Weapon.Weapon
                {
                    ItemCode = "weapon_morningstar_l",
                    Name = "Morning Star",
                    Value = 120,
                    MinDamage = 43,
                    MaxDamage = 46,
                    ItemRarity = FindItemRarityByName(context, "Legendary"),
                    WeaponType = FindWeaponTypeByName(context, "Mace")
                },
                // Flanged Mace
                new Item.Weapon.Weapon
                {
                    ItemCode = "weapon_flangedmace_c",
                    Name = "Flanged Mace",
                    Value = 8,
                    MinDamage = 29,
                    MaxDamage = 30,
                    ItemRarity = FindItemRarityByName(context, "Common"),
                    WeaponType = FindWeaponTypeByName(context, "Mace")
                },
                new Item.Weapon.Weapon
                {
                    ItemCode = "weapon_flangedmace_u",
                    Name = "Flanged Mace",
                    Value = 12,
                    MinDamage = 31,
                    MaxDamage = 32,
                    ItemRarity = FindItemRarityByName(context, "Uncommon"),
                    WeaponType = FindWeaponTypeByName(context, "Mace")
                },
                new Item.Weapon.Weapon
                {
                    ItemCode = "weapon_flangedmace_r",
                    Name = "Flanged Mace",
                    Value = 24,
                    MinDamage = 33,
                    MaxDamage = 34,
                    ItemRarity = FindItemRarityByName(context, "Rare"),
                    WeaponType = FindWeaponTypeByName(context, "Mace")
                },
                new Item.Weapon.Weapon
                {
                    ItemCode = "weapon_flangedmace_e",
                    Name = "Flanged Mace",
                    Value = 48,
                    MinDamage = 35,
                    MaxDamage = 36,
                    ItemRarity = FindItemRarityByName(context, "Epic"),
                    WeaponType = FindWeaponTypeByName(context, "Mace")
                },
                new Item.Weapon.Weapon
                {
                    ItemCode = "weapon_flangedmace_l",
                    Name = "Flanged Mace",
                    Value = 120,
                    MinDamage = 39,
                    MaxDamage = 40,
                    ItemRarity = FindItemRarityByName(context, "Legendary"),
                    WeaponType = FindWeaponTypeByName(context, "Mace")
                },
                // flachion
                new Item.Weapon.Weapon
                {
                    ItemCode = "weapon_flachion_c",
                    Name = "Falchion",
                    Value = 8,
                    MinDamage = 26,
                    MaxDamage = 28,
                    ItemRarity = FindItemRarityByName(context, "Common"),
                    WeaponType = FindWeaponTypeByName(context, "Sword")
                },
                new Item.Weapon.Weapon
                {
                    ItemCode = "weapon_flachion_u",
                    Name = "Falchion",
                    Value = 12,
                    MinDamage = 29,
                    MaxDamage = 31,
                    ItemRarity = FindItemRarityByName(context, "Uncommon"),
                    WeaponType = FindWeaponTypeByName(context, "Sword")
                },
                new Item.Weapon.Weapon
                {
                    ItemCode = "weapon_flachion_r",
                    Name = "Falchion",
                    Value = 24,
                    MinDamage = 32,
                    MaxDamage = 34,
                    ItemRarity = FindItemRarityByName(context, "Rare"),
                    WeaponType = FindWeaponTypeByName(context, "Sword")
                },
                new Item.Weapon.Weapon
                {
                    ItemCode = "weapon_flachion_e",
                    Name = "Falchion",
                    Value = 48,
                    MinDamage = 34,
                    MaxDamage = 35,
                    ItemRarity = FindItemRarityByName(context, "Epic"),
                    WeaponType = FindWeaponTypeByName(context, "Sword")
                },
                new Item.Weapon.Weapon
                {
                    ItemCode = "weapon_flachion_l",
                    Name = "Falchion",
                    Value = 120,
                    MinDamage = 36,
                    MaxDamage = 39,
                    ItemRarity = FindItemRarityByName(context, "Legendary"),
                    WeaponType = FindWeaponTypeByName(context, "Sword")
                },
                // longsword
                new Item.Weapon.Weapon
                {
                    ItemCode = "weapon_longsword_c", Name = "Longsword",
                    Value = 8, MinDamage = 37, MaxDamage = 38,
                    ItemRarity = FindItemRarityByName(context, "Common"),
                    WeaponType = FindWeaponTypeByName(context, "Longsword")
                },
                new Item.Weapon.Weapon
                {
                    ItemCode = "weapon_longsword_u", Name = "Longsword",
                    Value = 12, MinDamage = 39, MaxDamage = 41,
                    ItemRarity = FindItemRarityByName(context, "Uncommon"),
                    WeaponType = FindWeaponTypeByName(context, "Longsword")
                },
                new Item.Weapon.Weapon
                {
                    ItemCode = "weapon_longsword_r", Name = "Longsword",
                    Value = 24, MinDamage = 40, MaxDamage = 55,
                    ItemRarity = FindItemRarityByName(context, "Rare"),
                    WeaponType = FindWeaponTypeByName(context, "Longsword")
                },
                new Item.Weapon.Weapon
                {
                    ItemCode = "weapon_longsword_e", Name = "Longsword",
                    Value = 48, MinDamage = 56, MaxDamage = 61,
                    ItemRarity = FindItemRarityByName(context, "Epic"),
                    WeaponType = FindWeaponTypeByName(context, "Longsword")
                },
                new Item.Weapon.Weapon
                {
                    ItemCode = "weapon_longsword_l", Name = "Longsword",
                    Value = 120, MinDamage = 60, MaxDamage = 65,
                    ItemRarity = FindItemRarityByName(context, "Legendary"),
                    WeaponType = FindWeaponTypeByName(context, "Longsword")
                },
                // zweihander
                new Item.Weapon.Weapon
                {
                    ItemCode = "weapon_zweihander_c", Name = "Zweihander",
                    Value = 8, MinDamage = 40, MaxDamage = 42,
                    ItemRarity = FindItemRarityByName(context, "Common"),
                    WeaponType = FindWeaponTypeByName(context, "Longsword")
                },
                new Item.Weapon.Weapon
                {
                    ItemCode = "weapon_zweihander_u", Name = "Zweihander",
                    Value = 12, MinDamage = 43, MaxDamage = 45,
                    ItemRarity = FindItemRarityByName(context, "Uncommon"),
                    WeaponType = FindWeaponTypeByName(context, "Longsword")
                },
                new Item.Weapon.Weapon
                {
                    ItemCode = "weapon_zweihander_r", Name = "Zweihander",
                    Value = 24, MinDamage = 45, MaxDamage = 50,
                    ItemRarity = FindItemRarityByName(context, "Rare"),
                    WeaponType = FindWeaponTypeByName(context, "Longsword")
                },
                new Item.Weapon.Weapon
                {
                    ItemCode = "weapon_zweihander_e", Name = "Zweihander",
                    Value = 48, MinDamage = 50, MaxDamage = 60,
                    ItemRarity = FindItemRarityByName(context, "Epic"),
                    WeaponType = FindWeaponTypeByName(context, "Longsword")
                },
                new Item.Weapon.Weapon
                {
                    ItemCode = "weapon_zweihander_l", Name = "Zweihander",
                    Value = 120, MinDamage = 61, MaxDamage = 67,
                    ItemRarity = FindItemRarityByName(context, "Legendary"),
                    WeaponType = FindWeaponTypeByName(context, "Longsword")
                }
                );

            context.SaveChanges();
        }

        /// <summary>
        /// Tworzenie danych o typach bronii
        /// </summary>
        private static void InitWeaponType(RPG_Wiki_WebAppContext context)
        {
            // Sprawdzenie czy są 
            if (context.WeaponType.Any())
            {
                return;   // DB has been seeded
            }

            context.WeaponType.AddRange(
                new Item.Weapon.WeaponType { Name = "Longsword" },
                new Item.Weapon.WeaponType { Name = "Sword" },
                new Item.Weapon.WeaponType { Name = "Mace" },
                new Item.Weapon.WeaponType { Name = "Bow" },
                new Item.Weapon.WeaponType { Name = "Shield" }
                );

            context.SaveChanges();
        }

        /// <summary>
        /// Znajdowanie typu broni
        /// </summary>
        private static WeaponType FindWeaponTypeByName(RPG_Wiki_WebAppContext context, string weaponTypeName)
        {
            var weaponType = context.WeaponType.FirstOrDefault(type => type.Name == weaponTypeName);
            return weaponType;
        }

        /// <summary>
        /// Tworzenie danych o tierach przedmiotów
        /// </summary>
        private static void InitItemRarity(RPG_Wiki_WebAppContext context)
        {
            // Sprawdzenie czy są 
            if (context.ItemRarity.Any())
            {
                return;   // DB has been seeded
            }

            context.ItemRarity.AddRange(
                new Item.ItemRarity { Name = "Common" },
                new Item.ItemRarity { Name = "Uncommon" },
                new Item.ItemRarity { Name = "Epic" },
                new Item.ItemRarity { Name = "Rare" },
                new Item.ItemRarity { Name = "Legendary" },
                new Item.ItemRarity { Name = "Unique" }
            );

            context.SaveChanges();
        }

        /// <summary>
        /// Znajdowanie rzadkosci przedmiotu
        /// </summary>
        private static ItemRarity FindItemRarityByName(RPG_Wiki_WebAppContext context, string itemRarityName)
        {
            var itemRarity = context.ItemRarity.FirstOrDefault(rarity => rarity.Name == itemRarityName);
            return itemRarity;
        }
    }
}
