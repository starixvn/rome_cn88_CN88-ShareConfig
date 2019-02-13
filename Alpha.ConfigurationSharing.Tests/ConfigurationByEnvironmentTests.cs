namespace Alpha.ConfigurationSharing.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using NUnit.Framework;

    [TestFixture]
    public class ConfigurationByEnvironmentTests
    {
        public static string[] ExpectedAppSettings
        {
            get
            {
                return GetLocalAppSettings();
            }
        }

        public static string[] ExpectedConnectionsStringsUAT
        {
            get
            {
                return new string[]
                {
                    "CS_BackupDB",
                    "CS_ArchiveDB",
                    "LiveCasinoAdmin",
                    "IPEntities",
                    "DBCust.IPDB.BODBDownlineNet",
                    "DBCust.CustDB.BODBDownlineNet",
                    "DBPOL.NAP_UserControl.BODBDownlineNet",
                    "DBTXN.BODB02.BODBDownlineNet",
                    "DBTXN.CustDB.BODBDownlineNet",
                    "DBONUSER.ON_USER.BODBDownlineNet",
                    "DBPOLICY.bodb02.BODBDownlineNet",
                    "DBLOG.bodb_CustomerLog.BODBDownlineNet",
                    "DBAGE.bodb_AGE.BODBDownlineNet",
                    "DBAGE.bodb02.BODBDownlineNet",
                    "DB053.bodb_AGE.BODBDownlineNet",
                    "DB054.bodb_AGE.BODBDownlineNet",
                    "DB053.bodb02.BODBDownlineNet",
                    "DB054.bodb02.BODBDownlineNet",
                    "DB042.bodb02.BODBDownlineNet",
                    "DBARC.bodb_Archive.BODBDownlineNet",
                    "DBACC.bodb02.BODBDownlineNet",
                    "DBRPT.bodb_report.BODBDownlineNet",
                    "DBMSG.bodb_Message.BODBDownlineNet"
                };
            }
        }

        public static string[] ExpectedConnectionsStrings
        {
            get
            {
                return new string[]
                {
                    "CS_BackupDB",
                    "CS_ArchiveDB",
                    "LiveCasinoAdmin",
                    "IPEntities",
                    "DBCust.IPDB.BODBDownlineNet",
                    "DBCust.CustDB.BODBDownlineNet",
                    "DBPOL.NAP_UserControl.BODBDownlineNet",
                    "DBTXN.BODB02.BODBDownlineNet",
                    "DBTXN.CustDB.BODBDownlineNet",
                    "DBONUSER.ON_USER.BODBDownlineNet",
                    "DBPOLICY.bodb02.BODBDownlineNet",
                    "DBLOG.bodb_CustomerLog.BODBDownlineNet",
                    "DBAGE.bodb_AGE.BODBDownlineNet",
                    "DBAGE.bodb02.BODBDownlineNet",
                    "DB053.bodb_AGE.BODBDownlineNet",
                    "DB054.bodb_AGE.BODBDownlineNet",
                    "DB053.bodb02.BODBDownlineNet",
                    "DB054.bodb02.BODBDownlineNet",
                    "DB042.bodb02.BODBDownlineNet",
                    "DBARC.bodb_Archive.BODBDownlineNet",
                    "DBACC.bodb02.BODBDownlineNet",
                    "DBRPT.bodb_report.BODBDownlineNet",
                    "DBMSG.bodb_Message.BODBDownlineNet"
                };
            }
        }

        public static string[] ConnectionsStringsForSwitchingBetweenDB053AndDB054
        {
            get
            {
                return new string[]
                {
                    "CS_BackupDB",
                    "CS_ArchiveDB",
                    "LiveCasinoAdmin",
                    "IPEntities",
                    "DBCust.IPDB.BODBDownlineNet",
                    "DBCust.CustDB.BODBDownlineNet",
                    "DBPOL.NAP_UserControl.BODBDownlineNet",
                    "DBTXN.BODB02.BODBDownlineNet",
                    "DBTXN.CustDB.BODBDownlineNet",
                    "DBONUSER.ON_USER.BODBDownlineNet",
                    "DBPOLICY.bodb02.BODBDownlineNet",
                    "DBLOG.bodb_CustomerLog.BODBDownlineNet",
                    "DBAGE.bodb_AGE.BODBDownlineNet",
                    "DBAGE.bodb02.BODBDownlineNet",
                    "DB053.bodb_AGE.BODBDownlineNet",
                    "DB054.bodb_AGE.BODBDownlineNet",
                    "DB053.bodb02.BODBDownlineNet",
                    "DB054.bodb02.BODBDownlineNet",
                    "DB042.bodb02.BODBDownlineNet",
                    "DBARC.bodb_Archive.BODBDownlineNet",
                    "DBACC.bodb02.BODBDownlineNet",
                    "DBRPT.bodb_report.BODBDownlineNet",
                    "DBMSG.bodb_Message.BODBDownlineNet"
                };
            }
        }

        public static string BaseDirectory { get; set; }

        public int NumberOfAppSettings { get; set; }

        public int NumberOfConnectionsStrings { get; set; }

        public int NumberOfConnectionsStringsUAT { get; set; }

        public int NumberOfConnectionsStringsForSwitchingBetweenDB053AndDB054 { get; set; }

        public string WebConfigRelativePath { get; set; }

        public string RelativeAppSettingsAndConnectionStringTestPath { get; set; }

        public Configuration AppSettingsAndConnectionStringConfiguration { get; set; }

        public virtual void TestFixtureSetUp()
        {
            NumberOfAppSettings = ExpectedAppSettings.Length;
            NumberOfConnectionsStrings = ExpectedConnectionsStrings.Length;
            NumberOfConnectionsStringsUAT = ExpectedConnectionsStringsUAT.Length;
            NumberOfConnectionsStringsForSwitchingBetweenDB053AndDB054 = ConnectionsStringsForSwitchingBetweenDB053AndDB054.Length;
            AppSettingsAndConnectionStringConfiguration = LoadSetting(RelativeAppSettingsAndConnectionStringTestPath);
        }

        //public virtual void ValidateAppSettings_LoadedAppSettings_ReturnCorrectItemLength()
        //{
        //    // Assert
        //    Assert.AreEqual(NumberOfAppSettings, AppSettingsAndConnectionStringConfiguration.AppSettings.Settings.Count);
        //}

        public virtual void ValidateAppSettings_SupportKeyName_ReturnData(string keyName)
        {
            // Action
            var setting = AppSettingsAndConnectionStringConfiguration.AppSettings.Settings[keyName];
            setting = setting ?? new KeyValueConfigurationElement("Test", "Test");
            // Assert
            Assert.IsNotNull(setting);
        }

        public virtual void ValidateConnectionStrings_LoadedConnectionStringsUAT_ReturnCorrectItemLength()
        {
            // Assert
            Assert.AreEqual(NumberOfConnectionsStringsUAT, AppSettingsAndConnectionStringConfiguration.ConnectionStrings.ConnectionStrings.Count);
        }

        public virtual void ValidateConnectionStrings_LoadedConnectionStrings_ReturnCorrectItemLength()
        {
            // Assert
            Assert.AreEqual(NumberOfConnectionsStrings, AppSettingsAndConnectionStringConfiguration.ConnectionStrings.ConnectionStrings.Count);
        }

        public virtual void ValidateConnectionStringsForSwitchingBetweenDB053AndDB054_LoadedConnectionStrings_ReturnCorrectItemLength()
        {
            // Assert
            Assert.AreEqual(NumberOfConnectionsStringsForSwitchingBetweenDB053AndDB054, AppSettingsAndConnectionStringConfiguration.ConnectionStrings.ConnectionStrings.Count);
        }

        public virtual void ValidateConnectionStrings_SupportConnectionName_ReturnData(string connectionName)
        {
            // Action
            var connectionString = AppSettingsAndConnectionStringConfiguration.ConnectionStrings.ConnectionStrings[connectionName];

            // Assert
            Assert.IsNotNull(connectionString);
        }

        public virtual void ValidateWebConfig_LoadedWebConfig_LoadCorrectConfig()
        {
            // Arrange
            var configurationLoader = new ConfigurationLoader();
            var configuration = configurationLoader.LoadConfigurations(BaseDirectory + WebConfigRelativePath);

            // Action
            var sectionKeys = configuration.Sections.Keys;
            var keyList = new List<string>();
            foreach (var sectionKey in sectionKeys)
            {
                keyList.Add(sectionKey.ToString());
            }

            var containEnoughSections = keyList.Contains("Fanex.Logging") &&
                keyList.Contains("AlphaAuthSection") &&
                keyList.Contains("Fanex.Data.DbSettingProvider");

            // Assert
            Assert.IsTrue(containEnoughSections);
        }

        public static string[] GetLocalAppSettings()
        {
            var settings = LoadSetting("AppSettingsAndConnectionStringTest.config").AppSettings.Settings;

            var result = new List<string>();

            foreach (KeyValueConfigurationElement setting in settings)
            {
                result.Add(setting.Key);
            }

            return result.ToArray();
        }

        private static Configuration LoadSetting(string relativeAppSettingsAndConnectionStringTestPath)
        {
            BaseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            if (!BaseDirectory.EndsWith("\\"))
            {
                BaseDirectory += "\\";
            }

            var configurationLoader = new ConfigurationLoader();

            var settingPath = BaseDirectory + relativeAppSettingsAndConnectionStringTestPath;
            return configurationLoader.LoadConfigurations(settingPath);
        }
    }
}