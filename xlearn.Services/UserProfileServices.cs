using System;
using System.Collections.Generic;
using System.Linq;
using Dapper;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using xlearn.Models;
using xlearn.Repository;

namespace xlearn.Services {
    public class UserProfileServices : RepositoryBase<UserProfile> {
        public UserProfileServices(IConfiguration configuration) : base(configuration) { }

        public override IEnumerable<UserProfile> GetAll() {
            using(var db = new MySqlConnection(ConnectionString)) {
                return db.Query<UserProfile, User, Profile, UserProfile>(
                    @"  SELECT
                            USERPROFILES.ID,
                            USERPROFILES.USERID,
                            USERPROFILES.PROFILEID,
                            USERPROFILES.CONFIGURATION,
                            USERS.ID,
                            USERS.NAME,
                            PROFILES.ID,
                            PROFILES.NAME
                        FROM
                            USERPROFILES
                        INNER JOIN USERS on
                            USERPROFILES.USERID = USERS.ID
                        INNER JOIN PROFILES on
                            PROFILES.ID = USERPROFILES.PROFILEID
                    ",
                    map: (UserProfile, User, Profiles) => {
                        UserProfile.User = User;
                        UserProfile.Profile = Profiles;
                        return UserProfile;
                    });
            }
        }

        public override UserProfile GetById(UInt64 userProfileId) {
            using(var db = new MySqlConnection(ConnectionString)) {
                return db.Query<UserProfile, User, Profile, UserProfile>(
                    @"  SELECT
                            USERPROFILES.ID,
                            USERPROFILES.USERID,
                            USERPROFILES.PROFILEID,
                            USERPROFILES.CONFIGURATION,
                            USERS.ID,
                            USERS.NAME,
                            PROFILES.ID,
                            PROFILES.NAME
                        FROM
                            USERPROFILES
                        INNER JOIN USERS on
                            USERPROFILES.USERID = USERS.ID
                        INNER JOIN PROFILES on
                            PROFILES.Id = USERPROFILES.PROFILEID
                        WHERE USERPROFILES.ID = @id
                    ",
                    map: (UserProfile, User, Profile) => {
                        UserProfile.User = User;
                        UserProfile.Profile = Profile;
                        return UserProfile;
                    },
                    new { @id = userProfileId }).FirstOrDefault();
            }
        }
    }
}
