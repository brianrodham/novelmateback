using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using NovelMate.Models;

namespace NovelMate.Data
{
    public class DbInitializer
    {
        public static void Initialize(NovelMateContext context)
        {
            context.Database.EnsureCreated();

            if (context.StoryNodes.Any())
            {
                return; // Database has already been seeded
            }

            Guid newGuid = Guid.NewGuid();
            // Add dummy story nodes
            var storyNodes = new StoryNode[] {
                new StoryNode {UserId = newGuid, Name="Chapter 1", Content="Some RTF content 1 ", CreatedAt=DateTime.Now, ModifiedAt=DateTime.Now},
                new StoryNode {UserId = newGuid, Name="Chapter 2", Content="Some RTF content 2", CreatedAt=DateTime.Now, ModifiedAt=DateTime.Now},
                new StoryNode {UserId = newGuid, Name="Chapter 3", Content="Some RTF content 3", CreatedAt=DateTime.Now, ModifiedAt=DateTime.Now},
                new StoryNode {UserId = newGuid, Name="Chapter 4", Content="Some RTF content 4", CreatedAt=DateTime.Now, ModifiedAt=DateTime.Now},
                new StoryNode {UserId = newGuid, Name="Chapter 5", Content="Some RTF content 5", CreatedAt=DateTime.Now, ModifiedAt=DateTime.Now}
            };

            foreach (StoryNode n in storyNodes)
            {
                context.StoryNodes.Add(n);
            }
            context.SaveChanges();


        }
    }
}
