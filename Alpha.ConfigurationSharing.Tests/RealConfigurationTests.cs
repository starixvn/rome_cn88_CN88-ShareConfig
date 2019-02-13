namespace Alpha.ConfigurationSharing.Tests
{
    using NUnit.Framework;

    [TestFixture]
    public class RealConfigurationTests : ConfigurationByEnvironmentTests
    {
        // We must use this property because NUnit can't load ExpectedAppSettings from base class
        public static string[] ExpectedAppSettingsChild
        {
            get
            {
                return ExpectedAppSettings;
            }
        }

        [OneTimeSetUp]
        public override void TestFixtureSetUp()
        {
            RelativeAppSettingsAndConnectionStringTestPath = "AppSettingsAndConnectionStringTest.config";
            WebConfigRelativePath = "Web.config";
            base.TestFixtureSetUp();
        }

        //[Test]
        //public override void ValidateAppSettings_LoadedAppSettings_ReturnCorrectItemLength()
        //{
        //    base.ValidateAppSettings_LoadedAppSettings_ReturnCorrectItemLength();
        //}

        [Test, TestCaseSource("ExpectedAppSettingsChild")]
        public override void ValidateAppSettings_SupportKeyName_ReturnData(string keyName)
        {
            base.ValidateAppSettings_SupportKeyName_ReturnData(keyName);
        }

        [Test]
        public override void ValidateConnectionStrings_LoadedConnectionStrings_ReturnCorrectItemLength()
        {
            base.ValidateConnectionStrings_LoadedConnectionStrings_ReturnCorrectItemLength();
        }

        [TestCase("CS_BackupDB")]
        [TestCase("CS_ArchiveDB")]
        [TestCase("LiveCasinoAdmin")]
        [TestCase("IPEntities")]
        [TestCase("DBCust.IPDB.BODBDownlineNet")]
        [TestCase("DBCust.CustDB.BODBDownlineNet")]
        [TestCase("DBTXN.BODB02.BODBDownlineNet")]
        [TestCase("DBTXN.CustDB.BODBDownlineNet")]
        [TestCase("DBONUSER.ON_USER.BODBDownlineNet")]
        [TestCase("DBPOLICY.bodb02.BODBDownlineNet")]
        [TestCase("DBLOG.bodb_CustomerLog.BODBDownlineNet")]
        [TestCase("DBAGE.bodb_AGE.BODBDownlineNet")]
        [TestCase("DBAGE.bodb02.BODBDownlineNet")]
        [TestCase("DB054.bodb02.BODBDownlineNet")]
        [TestCase("DBARC.bodb_Archive.BODBDownlineNet")]
        [TestCase("DBACC.bodb02.BODBDownlineNet")]
        [TestCase("DBRPT.bodb_report.BODBDownlineNet")]
        [TestCase("DBMSG.bodb_Message.BODBDownlineNet")]
        public override void ValidateConnectionStrings_SupportConnectionName_ReturnData(string connectionName)
        {
            base.ValidateConnectionStrings_SupportConnectionName_ReturnData(connectionName);
        }

        [Test]
        public override void ValidateWebConfig_LoadedWebConfig_LoadCorrectConfig()
        {
            base.ValidateWebConfig_LoadedWebConfig_LoadCorrectConfig();
        }
    }
}
