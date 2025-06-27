using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RPG_Wiki_WebApp.Models.Item;
using RPG_Wiki_WebApp.Models.Item.Weapon;
using RPG_Wiki_WebApp.Models.Item.Armor;
using RPG_Wiki_WebApp.Models.Item.Misc;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using RPG_Wiki_WebApp.Models;
using RPG_Wiki_WebApp.Models.Character;

namespace RPG_Wiki_WebApp.Data
{
    public class RPG_Wiki_WebAppContext : IdentityDbContext<ApplicationUser>
    {
        public RPG_Wiki_WebAppContext (DbContextOptions<RPG_Wiki_WebAppContext> options)
            : base(options)
        {
        }

        public DbSet<RPG_Wiki_WebApp.Models.Item.Item> Item { get; set; } = default!;
        public DbSet<RPG_Wiki_WebApp.Models.Item.ItemRarity> ItemRarity { get; set; } = default!;
        public DbSet<RPG_Wiki_WebApp.Models.Item.Weapon.Weapon> Weapon { get; set; } = default!;
        public DbSet<RPG_Wiki_WebApp.Models.Item.Weapon.WeaponType> WeaponType { get; set; } = default!;
        public DbSet<RPG_Wiki_WebApp.Models.Item.Armor.Armor> Armor { get; set; } = default!;
        public DbSet<RPG_Wiki_WebApp.Models.Item.Armor.ArmorType> ArmorType { get; set; } = default!;
        public DbSet<RPG_Wiki_WebApp.Models.Item.Misc.Misc> Misc { get; set; } = default!;
        public DbSet<RPG_Wiki_WebApp.Models.Item.Misc.MiscType> MiscType { get; set; } = default!;
        public DbSet<RPG_Wiki_WebApp.Models.Character.Character> Character { get; set; } = default!;
        public DbSet<RPG_Wiki_WebApp.Models.Character.CharacterType> CharacterType { get; set; } = default!;
    }
}
