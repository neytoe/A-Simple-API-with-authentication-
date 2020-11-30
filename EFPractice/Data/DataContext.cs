using EFPractice.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EFPractice.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options): base (options)
        {

        }
        public DbSet<Character> Characters { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Weapon> Weapons { get; set; }
        public DbSet<Skill> Skills { get; set; }
        public DbSet<CharacterSkill> CharacterSkills { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder )
        {
            modelBuilder.Entity<CharacterSkill>()
                .HasKey(cs => new { cs.CharacterId, cs.SkillId });

            modelBuilder.Entity<User>()
                .Property(user => user.Role).HasDefaultValue("Player");

            modelBuilder.Entity<Skill>()
                .HasData(
                new Skill {  Id=1,Name="spongeBob", Damage= 30},
                new Skill { Id = 2, Name = "sponge", Damage = 40 },
                new Skill { Id = 3, Name = "Bob", Damage = 50 }
                );
        }
    }
}
