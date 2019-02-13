namespace Alpha.ConfigurationSharing.Tests
{
    using NUnit.Framework;

    [TestFixture]
    public class ProI1ConfigurationTests : ConfigurationByEnvironmentTests
    {
        // We must use this property because NUnit can't load ExpectedAppSettings from base class
        public static string[] ExpectedAppSettingsChild
        {
            get
            {
                return ExpectedAppSettings;
            }
        }

        // We must use this property because NUnit can't load ExpectedConnectionsStrings from base class
        public static string[] ExpectedConnectionsStringsChild
        {
            get
            {
                return ExpectedConnectionsStrings;
            }
        }

        [OneTimeSetUp]
        public override void TestFixtureSetUp()
        {
            RelativeAppSettingsAndConnectionStringTestPath = "AppSettingsAndConnectionStringTest.proi1.config";
            WebConfigRelativePath = "Web.proi1.config";
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

        [Test, TestCaseSource("ExpectedConnectionsStringsChild")]
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
