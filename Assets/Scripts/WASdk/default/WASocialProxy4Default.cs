using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WASdkAPI
{
    class WASocialProxy4Default : WASocialProxy
    {
        public override void appInvite(string platform, string appLinkUrl, string previewImageUrl, WACallback<WAResult> callback)
        {
            throw new NotImplementedException();
        }

        public override void createInviteRecord(string platform, string requestId, List<string> recipients, WACallback<WAResult> callback)
        {
            throw new NotImplementedException();
        }

        public override void displayAchievement(string platform)
        {
            throw new NotImplementedException();
        }

        public override int displayAchievement(string platform, WACallback<WAResult> callback)
        {
            throw new NotImplementedException();
        }

        public override void fbDeleteRequest(string requestId, WACallback<WAResult> callback)
        {
            throw new NotImplementedException();
        }

        public override void fbQueryAskForGiftRequests(WACallback<WAFBGameRequestResult> callback)
        {
            throw new NotImplementedException();
        }

        public override void fbQueryReceivedGifts(WACallback<WAFBGameRequestResult> callback)
        {
            throw new NotImplementedException();
        }

        public override void getCurrentAppLinkedGroup(string platform, string extInfo, WACallback<WAGroupResult> callback)
        {
            throw new NotImplementedException();
        }

        public override void getCurrentUserGroup(string platform, string extInfo, WACallback<WAGroupResult> callback)
        {
            throw new NotImplementedException();
        }

        public override void getGroupByGid(string platform, string[] gids, string extInfo, WACallback<WAGroupResult> callback)
        {
            throw new NotImplementedException();
        }

        public override void increaseAchievement(string platform, string id, int numSteps, WACallback<WAUpdateAchievementResult> callback)
        {
            throw new NotImplementedException();
        }

        public override void inviteEventReward(string platform, string eventName, WACallback<WAResult> callback)
        {
            throw new NotImplementedException();
        }

        public override void inviteInstallReward(string platform, WACallback<WAResult> callback)
        {
            throw new NotImplementedException();
        }

        public override void isCurrentUserGroupMember(string platform, string groupId, string extInfo, WACallback<bool> callback)
        {
            throw new NotImplementedException();
        }

        public override bool isGameSignedIn(string platform)
        {
            throw new NotImplementedException();
        }

        public override void joinGroup(string platform, string groupId, string extInfo, WACallback<WAResult> callback)
        {
            throw new NotImplementedException();
        }

        public override void loadAchievements(string platform, bool forceReload, WACallback<WALoadAchievementResult> callback)
        {
            throw new NotImplementedException();
        }

        public override void openGroupPage(string platform, string groupUri, string extInfo)
        {
            throw new NotImplementedException();
        }

        public override void queryFBGraphObjects(string objectType, WACallback<WAFBGraphObjectResult> callback)
        {
            throw new NotImplementedException();
        }

        public override void queryFriends(string platform, WACallback<WAFriendsResult> callback)
        {
            throw new NotImplementedException();
        }

        public override void queryInvitableFriends(string platform, long duration, WACallback<WAFriendsResult> callback)
        {
            throw new NotImplementedException();
        }

        public override void revealAchievement(string platform, string id, WACallback<WAUpdateAchievementResult> callback)
        {
            throw new NotImplementedException();
        }

        public override void sendRequest(string platform, string requestType, string title, string message, string objectId, List<string> receiptIds, WACallback<WARequestSendResult> callback, string extInfo)
        {
            throw new NotImplementedException();
        }

        public override void setStepsAchievement(string platform, string id, int numSteps, WACallback<WAUpdateAchievementResult> callback)
        {
            throw new NotImplementedException();
        }

        public override void share(string platform, WAShareContent shareContent, bool shareWithApi, string extInfo, WACallback<WAShareResult> callback)
        {
            throw new NotImplementedException();
        }

        public override void signInGame(string platform, WACallback<WAPlayer> callback)
        {
            throw new NotImplementedException();
        }

        public override void signOutGame(string platform)
        {
            throw new NotImplementedException();
        }

        public override void unlockAchievement(string platform, string id, WACallback<WAUpdateAchievementResult> callback)
        {
            throw new NotImplementedException();
        }
    }
}
