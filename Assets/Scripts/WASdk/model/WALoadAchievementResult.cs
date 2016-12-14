using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WASdkAPI
{
    public class WALoadAchievementResult : WAResult
    {
        public List<WAAchievement> achievements
        {
            get;
            set;
        }

        public void addAchievement(WAAchievement achievement)
        {
            if(null == achievements)
            {
                achievements = new List<WAAchievement>();
            }
            achievements.Add(achievement);
        }
    }
}
