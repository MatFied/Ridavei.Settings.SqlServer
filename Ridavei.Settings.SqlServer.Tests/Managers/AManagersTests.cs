using NUnit.Framework;
using Shouldly;

namespace Ridavei.Settings.SqlServer.Tests.Managers
{
    [TestFixture]
    public abstract class AManagersTests
    {
        protected const string DictionaryName = "TestDictionary";

        protected SettingsBuilder Builder;

        protected abstract void SetManager();

        [SetUp]
        protected virtual void SetUp()
        {
            Builder = SettingsBuilder
                .CreateBuilder();
        }

        [TearDown]
        protected virtual void TearDown() { }

        [Test]
        public void CreateDbSettings__GetSettings()
        {
            Should.NotThrow(() =>
            {
                SetManager();
                var settings = Builder.GetOrCreateSettings(DictionaryName);
                settings.ShouldNotBeNull();
                settings.DictionaryName.ShouldBe(DictionaryName);
            });
        }


        [Test]
        public void TryGetDbSettingsObject__GetSettings()
        {
            Should.NotThrow(() =>
            {
                SetManager();
                var settings = Builder.GetSettings(DictionaryName);
                settings.ShouldNotBeNull();
                settings.DictionaryName.ShouldBe(DictionaryName);
            });
        }
    }
}
