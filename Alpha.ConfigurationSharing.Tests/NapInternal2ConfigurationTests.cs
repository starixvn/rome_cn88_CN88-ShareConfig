namespace Alpha.ConfigurationSharing.Tests
{
    using NUnit.Framework;

    [TestFixture]
    public class NapInternal2ConfigurationTests : ConfigurationByEnvironmentTests
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
            RelativeAppSettingsAndConnectionStringTestPath = "AppSettingsAndConnectionStringTest.napinternal2.config";
            WebConfigRelativePath = "Web.napinternal2.config";
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
            base.ValidateConnectionStringsForSwitchingBetweenDB053AndDB054_LoadedConnectionStrings_ReturnCorrectItemLength();
        }

        [TestCase("CS_BackupDB")]
        [TestCase("CS_ArchiveDB")]
        [TestCase("LiveCasinoAdmin")]
        [TestCase("IPEntities")]
        [TestCase("DBCust.IPDB.BODBDownlineNet")]
        [TestCase("DBCust.CustDB.BODBDownlineNetView")]
        [TestCase("DBTXN.BODB02.BODBDownlineNetView")]
        [TestCase("DBTXN.CustDB.BODBDownlineNetView")]
        [TestCase("DBONUSER.ON_USER.BODBDownlineNetView")]
        [TestCase("DBPOLICY.bodb02.BODBDownlineNetView")]
        [TestCase("DBLOG.bodb_CustomerLog.BODBDownlineNetView")]
        [TestCase("DBAGE.bodb_AGE.BODBDownlineNetView")]
        [TestCase("DBAGE.bodb02.BODBDownlineNetView")]
        [TestCase("DB054.bodb02.BODBDownlineNetView")]
        [TestCase("DBARC.bodb_Archive.BODBDownlineNetView")]
        [TestCase("DBACC.bodb02.BODBDownlineNetView")]
        [TestCase("DBRPT.bodb_report.BODBDownlineNetView")]
        [TestCase("DBMSG.bodb_Message.BODBDownlineNetView")]
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
